using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE2_MeleeAttackState : MeleeAttackState
{
    private MiniSkeletonArcher enemy;

    public  UE2_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, MiniSkeletonArcher enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData) 
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

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished) {
            if (!isPlayerInMinAgroRange) {
                stateMachine.ChangeState(enemy.chaseState);
            } else if (!isPlayerInMaxAgroRange) {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
