using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IManagement
{
    private Animator _animator;
    private AnimatorClipInfo _animClipInfo;
    private Rigidbody _rigidbody;
    private Vector3 velocity;

    public float airSpeed = 10f;
    private float turnSpeed = 15f;

    public bool onFloor;

    public bool jump = false;
    public float jumpForce = 30;

    public bool dash = false;
    public float dashForce = 60;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.AddManaged(this);
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) jump = true;
        if (Input.GetKeyDown(KeyCode.LeftShift)) dash = true;
        _animator.SetFloat("fallVelocity", _rigidbody.velocity.y);
    }

    public void MFixedUpdate()
    {
        if (onFloor) PlayerMovement();
        else PlayerAirMovement();

        if (jump)
        {
            jump = !jump;
            PlayerJump();
        }

        if (dash)
        {
            dash = !dash;
            PlayerDash();
        }
    }

    public void MLateUpdate()
    {
        
    }

    public void MPaused(bool pause)
    {
        if (pause)
        {
            velocity = _rigidbody.velocity;
            _rigidbody.isKinematic = true;
            _animator.enabled = false;
        } else
        {
            _rigidbody.velocity = velocity;
            _rigidbody.isKinematic = false;
            _animator.enabled = true;
        }
    }

    private void PlayerMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Vector2 input = new Vector2(inputX, inputZ);

        var forward = Camera.main.transform.TransformDirection(Vector3.forward);
        var right = Camera.main.transform.TransformDirection(Vector3.right);
        forward.y = 0;

        Vector3 targetDirection = (inputX * right) + (inputZ * forward);

        if (input != Vector2.zero && targetDirection.magnitude > .1f)
        {
            _animator.SetBool("moving", true);

            targetDirection.Normalize();
            _animator.SetFloat("speed", targetDirection.magnitude);
            float differenceRotation = Vector3.Angle(transform.forward, targetDirection);


            float dot = Vector3.Dot(transform.right, targetDirection);
            var leastTravelDirection = dot < 0 ? -1 : 1;

            //GetComponent<Rigidbody>().velocity = targetDirection * airSpeed + GetComponent<Rigidbody>().velocity.y * Vector3.up;
            transform.Rotate(transform.up, differenceRotation * leastTravelDirection * turnSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("moving", false);
            _animator.SetFloat("speed", targetDirection.magnitude);
        }
    }

    private void PlayerAirMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Vector2 input = new Vector2(inputX, inputZ);

        var forward = Camera.main.transform.TransformDirection(Vector3.forward);
        var right = Camera.main.transform.TransformDirection(Vector3.right);
        forward.y = 0;

        Vector3 targetDirection = (inputX * right) + (inputZ * forward);

        if (input != Vector2.zero && targetDirection.magnitude > .1f)
        {
            targetDirection.Normalize();
            _animator.SetFloat("speed", targetDirection.magnitude);
            float differenceRotation = Vector3.Angle(transform.forward, targetDirection);

            float dot = Vector3.Dot(transform.right, targetDirection);
            var leastTravelDirection = dot < 0 ? -1 : 1;
            _rigidbody.AddForce(new Vector3(targetDirection.x, 0f, targetDirection.z), ForceMode.Acceleration);
            GetComponent<Rigidbody>().velocity = targetDirection * airSpeed + GetComponent<Rigidbody>().velocity.y * Vector3.up;
            transform.Rotate(transform.up, differenceRotation * leastTravelDirection * turnSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetFloat("speed", targetDirection.magnitude);
        }
    }

    private void PlayerJump()
    {
        _animator.SetTrigger("jump");
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(jumpForce * transform.up, ForceMode.Impulse);
    }

    private void PlayerDash()
    {
        _animator.SetTrigger("dash");
        _rigidbody.AddForce(dashForce * transform.forward, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8) onFloor = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8) onFloor = false;
    }
}
