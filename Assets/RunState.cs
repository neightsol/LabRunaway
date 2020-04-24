using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public RunState(PlayerController player) : base(player) { }
    public Vector2 movement;

    public override void OnStateEnter()
    {
        player.playerAnimator.SetBool("moving", true);
        if (player.grounded) player.jumpsLeft = player.jumpsMax;
    }

    public override void Tick()
    {
        if (player.GetInput() == Vector2.zero) player.SetState(new IdleState(player));
        if (Input.GetKeyDown(KeyCode.Space)) player.SetState(new JumpState(player));
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.canDash) player.SetState(new DashState(player));
        if (!player.grounded) player.SetState(new AirState(player));
        movement = player.GetInput();
    }

    public override void FixedTick()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            var forward = Camera.main.transform.TransformDirection(Vector3.forward);
            var right = Camera.main.transform.TransformDirection(Vector3.right);
            forward.y = 0;

            Vector3 targetDirection = (movement.x * right) + (movement.y * forward);

            player.playerAnimator.SetBool("moving", true);

            targetDirection.Normalize();
            float differenceRotation = Vector3.Angle(player.transform.forward, targetDirection);

            float dot = Vector3.Dot(player.transform.right, targetDirection);
            var leastTravelDirection = dot < 0 ? -1 : 1;

            Vector3 velocity = player.transform.forward * this.movement.y * player.airSpeed + player.transform.right * this.movement.x * player.airSpeed;
            player.playerRigidbody.velocity = new Vector3(velocity.x, player.playerRigidbody.velocity.y, velocity.z);
        } else
        {
            var forward = Camera.main.transform.TransformDirection(Vector3.forward);
            var right = Camera.main.transform.TransformDirection(Vector3.right);
            forward.y = 0;
            
            Vector3 targetDirection = (movement.x * right) + (movement.y * forward);

            player.playerAnimator.SetBool("moving", true);

            targetDirection.Normalize();
            float differenceRotation = Vector3.Angle(player.transform.forward, targetDirection);

            float dot = Vector3.Dot(player.transform.right, targetDirection);
            var leastTravelDirection = dot < 0 ? -1 : 1;

            //GetComponent<Rigidbody>().velocity = targetDirection * airSpeed + GetComponent<Rigidbody>().velocity.y * Vector3.up;
            player.transform.Rotate(player.transform.up, differenceRotation * leastTravelDirection * player.turnSpeed * Time.deltaTime);
        }
    }

    public override void OnStateExit()
    {
        player.playerAnimator.SetBool("moving", false);
    }
}
