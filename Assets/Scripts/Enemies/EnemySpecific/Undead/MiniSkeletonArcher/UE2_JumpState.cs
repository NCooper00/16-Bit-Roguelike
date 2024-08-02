using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE2_JumpState : JumpState
{
    private MiniSkeletonArcher enemy;

    public  UE2_JumpState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_JumpState stateData, MiniSkeletonArcher enemy) : base(entity, stateMachine, animBoolName, stateData) 
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
