using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public bool facingRight { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }

    [SerializeField]
    private Transform playerCheck;
  
    private GameObject PLAYER;

    private Transform playerPosition;

    public Transform attackPosition;

    private Vector2 velocityWorkspace;

    private float Speed;
    private float newVelocity;
    public float speedLerpDuration = 2f;

    public bool Chase = false;

    public virtual void Start() {

        facingRight = true;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStatemachine>();

        PLAYER = GameObject.Find("PLAYER");

        Speed = 0;

        playerPosition = PLAYER.GetComponent<Transform>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update() {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate() {
        stateMachine.currentState.PhysicsUpdate();
        if (Chase) {
            StartCoroutine(LerpSpeed(newVelocity));
            
            if (facingRight == false && playerPosition.position.x > rb.transform.position.x) {
                Flip();
            } else if (facingRight == true && playerPosition.position.x < rb.transform.position.x) {
                Flip();
            }

            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, Speed * Time.deltaTime);
        }
    }

    public virtual void SetVelocity(float speed) {
        newVelocity = speed;
    }

    public IEnumerator LerpSpeed(float speed) {
        float timeElapsed = 0;
        float startValue = Speed;
        while (timeElapsed < speedLerpDuration)
        {
            Speed = Mathf.Lerp(startValue, speed, timeElapsed / speedLerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Speed = speed;
    }

    public virtual bool CheckPlayerInMinAgroRange() {
        return Physics2D.OverlapCircle(playerCheck.position, entityData.minAgroDistance, entityData.playerHitDetector);
    }

    public virtual bool CheckPlayerInMaxAgroRange() {
        return Physics2D.OverlapCircle(playerCheck.position, entityData.maxAgroDistance, entityData.playerHitDetector);
    }

    public virtual bool CheckPlayerInCloseRangeAction() {
        return Physics2D.OverlapCircle(playerCheck.position, entityData.closeRangeActionDistance, entityData.playerHitDetector);
    }

    public virtual void ChasePlayer() {
        if (facingRight == false && playerPosition.position.x > transform.position.x) {
            Flip();
        }else if (facingRight == true && playerPosition.position.x < transform.position.x) {
            Flip();
        }

        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, Speed * Time.deltaTime);
    }

    public virtual void Flip() {
        facingRight = !facingRight;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos() {
        Gizmos.DrawWireSphere(playerCheck.position, entityData.minAgroDistance);
        Gizmos.DrawWireSphere(playerCheck.position, entityData.maxAgroDistance);

        Gizmos.DrawWireSphere(playerCheck.position, entityData.closeRangeActionDistance);
    }
}
