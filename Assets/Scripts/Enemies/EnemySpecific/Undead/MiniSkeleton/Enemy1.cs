using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChaseState chaseState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_JumpState jumpState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
    public E1_HitState hitState { get; private set; }
    public E1_StunState stunState { get; private set; }
    public E1_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChaseState chaseStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_JumpState jumpStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_HitState hitStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;


    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chaseState = new E1_ChaseState(this, stateMachine, "chase", chaseStateData, this);
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "attack", meleeAttackPosition, meleeAttackStateData, this);
        jumpState = new E1_JumpState(this, stateMachine, "jump", jumpStateData, this);
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        hitState = new E1_HitState(this, stateMachine, "hit", hitStateData, this);
        stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(idleState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(attackPosition.position, meleeAttackStateData.attackRadius);
    }
}
