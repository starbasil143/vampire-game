using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public float enemyDefense = 1f;
    public Rigidbody2D rigidBody { get; set; }
    public Animator animator { get; set; }
    public bool isFacingRight { get; set; } = false;
    public bool IsInChaseRange { get; set; }
    public bool IsInAttackRange { get; set; }

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyIdleState IdleState { get; set; }

    #endregion


    #region Scriptable Object Variables

    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;


    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }

    #endregion
    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyDamager"))
        {
            Damage(collision.gameObject.GetComponent<EnemyDamagerScript>().damageAmount * enemyDefense);
        }
    }


    #region Health/Die Functions
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (damageAmount > 0)
        {
            animator.Play("Hurt", -1, 0f);
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    #region Movement Functions


    public void MoveEnemy(Vector2 velocity)
    {
        rigidBody.velocity = velocity;
        FaceCorrectDirection(velocity);
    }

    public void FaceCorrectDirection(Vector2 velocity)
    {
        if (isFacingRight && velocity.x < 0f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180f, transform.rotation.z));
            isFacingRight = !isFacingRight;
        }
        else if (!isFacingRight && velocity.x > 0f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0f, transform.rotation.z));
            isFacingRight = !isFacingRight;
        }
    }

    #endregion

    #region Animation Triggers
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }
    #endregion

    #region Distance Checks

    public void SetChaseRangeStatus(bool isInChaseRange)
    {
        IsInChaseRange = isInChaseRange;
    }

    public void SetAttackRangeStatus(bool isInAttackRange)
    {
        IsInAttackRange = isInAttackRange;
    }

    #endregion
}
