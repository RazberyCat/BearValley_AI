using System;
using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;

    public Action EnterAction;
    public Action UpdateAction;
    public Action ExitAction;

    public void SetMachine(StateMachine machine)
    {
        stateMachine = machine;
    }

    public virtual void Enter() { EnterAction?.Invoke(); }
    public virtual void Update() { UpdateAction?.Invoke(); }
    public virtual void Exit() { ExitAction?.Invoke(); }

}
