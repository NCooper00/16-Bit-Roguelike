using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected bool performCloseRangeAction;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName) 
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.attackState = this;
        isAnimationFinished = false;

        entity.SetVelocity(0.15f);
        entity.Chase = true;
    }

    public override void Exit()
    {
        base.Exit();

        entity.Chase = false;
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

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMinAgroRange = entity.CheckPlayerInMaxAgroRange();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public virtual void TriggerAttack() 
    {
        
    }

    public virtual void FinishAttack() 
    {
        isAnimationFinished = true;
    }

}
