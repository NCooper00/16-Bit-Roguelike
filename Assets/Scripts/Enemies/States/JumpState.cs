using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    protected D_JumpState stateData;

    public JumpState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_JumpState stateData) : base(entity, stateMachine, animBoolName) 
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

        entity.SetVelocity(0f);
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
