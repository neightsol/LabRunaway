using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public bool jumped;

    public JumpState(PlayerController player) : base(player) { }

    public override void OnStateEnter()
    {
        player.playerAnimator.applyRootMotion = false;
        jumped = false;
    }

    public override void Tick()
    {
        if (jumped) player.SetState(new AirState(player));
    }

    public override void FixedTick()
    {
        if (player.jumpsLeft > 0)
        {
            player.playerRigidbody.velocity = new Vector3(player.playerRigidbody.velocity.x, 0f, player.playerRigidbody.velocity.z);
            player.playerRigidbody.AddForce(player.jumpForce * player.transform.up, ForceMode.Impulse);
            player.jumpsLeft--;
        }
        jumped = true;
    }
}
