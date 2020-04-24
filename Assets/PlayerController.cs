using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private State _currentState;
    public string currentState;

    public Animator playerAnimator;
    public Rigidbody playerRigidbody;

    public int jumpsMax = 2;
    public int jumpsLeft;
    public float jumpForce = 6f;

    public float turnSpeed = 15f;
    public float airSpeed = 4f;

    public bool canDash = true;
    public float dashTimer = 0.5f;
    public float dashForce = 6f;

    public bool grounded = true;
    public bool colliding = true;

    public float distToGround;

    void Start()
    {
        distToGround = GetComponent<CapsuleCollider>().bounds.min.y;
        SetState(new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        grounded = IsGrounded();
        _currentState.Tick();
    }

    void FixedUpdate()
    {
        _currentState.FixedTick();
    }

    public void SetState(State state)
    {
        if (_currentState != null) _currentState.OnStateExit();

        _currentState = state;
        currentState = _currentState.GetType().Name;

        if (_currentState != null) _currentState.OnStateEnter();
    }

    public Vector2 GetInput()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputZ = Input.GetAxis("Vertical");

        return new Vector2(inputX, inputZ);
    }

    public bool IsGrounded()
    {
        Debug.DrawRay(transform.position, new Vector3(0f, -10, 0f), Color.red);
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    public void DashCooldownTrigger()
    {
        canDash = false;
        StartCoroutine("DashCooldown");
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashTimer);
        canDash = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        colliding = true;
        if (collision.gameObject.layer == 8) jumpsLeft = jumpsMax;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }
}
