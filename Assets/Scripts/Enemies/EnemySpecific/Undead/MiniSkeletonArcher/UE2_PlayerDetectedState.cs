using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE2_PlayerDetectedState : PlayerDetectedState
{
    private MiniSkeletonArcher enemy;

    public UE2_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, MiniSkeletonArcher enemy) : base(entity, stateMachine, animBoolName, stateData) 
    {
        this.enemy = enemy;
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

        if (performCloseRangeAction) {
            stateMachine.ChangeState(enemy.meleeAttackState);
        } else if (isPlayerInMaxAgroRange) {
            stateMachine.ChangeState(enemy.chaseState);
        } else if (!isPlayerInMaxAgroRange) {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
