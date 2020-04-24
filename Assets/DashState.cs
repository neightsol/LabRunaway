using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public bool dashed = false;

    public DashState(PlayerController player) : base(player) { }

    public override void OnStateEnter()
    {
        player.playerAnimator.SetTrigger("dash");
        player.playerAnimator.applyRootMotion = false;
        player.DashCooldownTrigger();
    }

    public override void Tick()
    {
        if (dashed)
        {
            player.SetState(new AirState(player));
        }
    }

    public override void FixedTick()
    {
        
        player.playerRigidbody.velocity = Vector3.zero;
        player.playerRigidbody.AddForce(player.dashForce * player.transform.forward, ForceMode.Impulse);
        dashed = true;
    }

    public override void OnStateExit()
    {
        player.playerAnimator.applyRootMotion = false;
    }
}
