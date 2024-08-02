using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE2_DeadState : DeadState
{
    private MiniSkeletonArcher enemy;

    public  UE2_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, MiniSkeletonArcher enemy) : base(entity, stateMachine, animBoolName, stateData) 
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

        entity.SetVelocity(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // If isBeingSummoned, then change state to resurrecting state
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
