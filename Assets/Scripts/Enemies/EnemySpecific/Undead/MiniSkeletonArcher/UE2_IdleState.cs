using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE2_IdleState : IdleState
{
    private MiniSkeletonArcher enemy;

    public UE2_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, MiniSkeletonArcher enemy) : base(entity, stateMachine, animBoolName, stateData) {
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

        

        if (isPlayerInMinAgroRange) {
            stateMachine.ChangeState(enemy.playerDetectedState);
        } else if (isIdleTimeOver) {
            // stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
