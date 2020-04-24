using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : State
{
    public AirState(PlayerController player) : base(player) { }

    public override void OnStateEnter()
    {
        player.playerAnimator.applyRootMotion = false;
    }

    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space)) player.SetState(new JumpState(player));
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.canDash) player.SetState(new DashState(player));
        if (player.grounded) player.SetState(new IdleState(player));
    }

    public override void FixedTick()
    {
        if (!player.colliding)
        {
            Vector2 input = player.GetInput();

            var forward = Camera.main.transform.TransformDirection(Vector3.forward);
            var right = Camera.main.transform.TransformDirection(Vector3.right);
            forward.y = 0;

            Vector3 targetDirection = (input.x * right) + (input.y * forward);

            //player.playerAnimator.SetBool("moving", true);

            targetDirection.Normalize();
            float differenceRotation = Vector3.Angle(player.transform.forward, targetDirection);

            float dot = Vector3.Dot(player.transform.right, targetDirection);
            var leastTravelDirection = dot < 0 ? -1 : 1;

            player.playerRigidbody.velocity = targetDirection * player.airSpeed + player.playerRigidbody.velocity.y * Vector3.up;
            player.transform.Rotate(player.transform.up, differenceRotation * leastTravelDirection * player.turnSpeed * Time.deltaTime);
        }
    }

    public override void OnStateExit()
    {
        //if (player.grounded) player.jumpsLeft = player.jumpsMax;
        player.playerAnimator.applyRootMotion = true;
    }
}
