using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerController player) : base(player) { }

    public override void OnStateEnter()
    {
        if (player.grounded) player.jumpsLeft = player.jumpsMax;
    }

    public override void Tick()
    {
        if (player.GetInput() != Vector2.zero) player.SetState(new RunState(player));
        if (Input.GetKeyDown(KeyCode.Space)) player.SetState(new JumpState(player));
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.canDash) player.SetState(new DashState(player));
        if (!player.grounded) player.SetState(new AirState(player));
    }
}
