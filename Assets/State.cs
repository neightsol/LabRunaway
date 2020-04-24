using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerController player;

    public State(PlayerController player)
    {
        this.player = player;
    }

    public abstract void Tick();
    public virtual void FixedTick() { }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
}
