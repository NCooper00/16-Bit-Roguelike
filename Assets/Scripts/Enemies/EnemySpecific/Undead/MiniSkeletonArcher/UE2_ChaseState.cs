using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE2_ChaseState : ChaseState
{
    private MiniSkeletonArcher enemy;

    public  UE2_ChaseState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData, MiniSkeletonArcher enemy) : base(entity, stateMachine, animBoolName, stateData) 
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

        if (performCloseRangeAction) {
            stateMachine.ChangeState(enemy.meleeAttackState);
        } else if (!isPlayerInMaxAgroRange) {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
