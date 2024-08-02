using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSkeletonArcher : Entity
{
    public UE2_IdleState idleState { get; private set; }
    public UE2_PlayerDetectedState playerDetectedState { get; private set; }
    public UE2_ChaseState chaseState { get; private set; }
    public UE2_MeleeAttackState meleeAttackState { get; private set; }
    public UE2_JumpState jumpState { get; private set; }
    public UE2_LookForPlayerState lookForPlayerState { get; private set; }
    public UE2_HitState hitState { get; private set; }
    public UE2_StunState stunState { get; private set; }
    public UE2_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
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

        idleState = new UE2_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new UE2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chaseState = new UE2_ChaseState(this, stateMachine, "chase", chaseStateData, this);
        meleeAttackState = new UE2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        jumpState = new UE2_JumpState(this, stateMachine, "jump", jumpStateData, this);
        lookForPlayerState = new UE2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        hitState = new UE2_HitState(this, stateMachine, "hit", hitStateData, this);
        stunState = new UE2_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new UE2_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(idleState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(attackPosition.position, meleeAttackStateData.attackRadius);
    }
}
