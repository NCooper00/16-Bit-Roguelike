using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_JumpState : JumpState
{
    private Enemy1 enemy;

    public  E1_JumpState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_JumpState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData) 
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
