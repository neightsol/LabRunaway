using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public float speed = 8f;

    private CharacterController _controller;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
    }

    private void playerMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        _animator.SetFloat("inputX", inputX);
        _animator.SetFloat("inputZ", inputZ);
        _animator.SetFloat("speed", _controller.velocity.magnitude);

        Vector3 direction = new Vector3(inputX, 0, inputZ).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;

        _controller.Move(movement);
    }
}
