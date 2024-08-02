using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE2_LookForPlayerState : LookForPlayerState
{
    private MiniSkeletonArcher enemy;

    public  UE2_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, MiniSkeletonArcher enemy) : base(entity, stateMachine, animBoolName, stateData) 
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

        if (isPlayerInMinAgroRange) {
            stateMachine.ChangeState(enemy.playerDetectedState);
        } else if (isAllTurnsTimeDone) {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
