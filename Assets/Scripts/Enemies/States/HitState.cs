using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    protected D_HitState stateData;

    public HitState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_HitState stateData) : base(entity, stateMachine, animBoolName) 
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
