using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{

    protected D_ChaseState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool isChaseTimeOver;

    protected bool performCloseRangeAction;

    public ChaseState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData) : base(entity, stateMachine, animBoolName) 
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(stateData.ChaseSpeed);
        entity.Chase = true;

        isChaseTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();

        entity.Chase = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        // entity.SetVelocity(stateData.ChaseSpeed);

        if (Time.time >= startTime + stateData.ChaseTime) {
            // isChaseTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

}
