using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PlayerStateType
{
    Idle,
    Move,
    Dodge,
    Attack,
    Hurt,
    Dead
}
public class Player : Charactor
{
    public static Player Instance { get; private set; }


    [Header("获取玩家输入")]
    public PlayerInput playerInput;

    [Header("移动")]
    public Vector2 inputDirection;
    public float moveSpeed;
    public float attackSpeed = 1f; // 攻击时玩家速度
    private float currentSpeed; // 当前移动速度

    [Header("攻击")]
    public bool isAttack;
    public float attackDamage;
    public Vector2 AttackSize = new Vector2(1f, 1f);
    public float offsetX = 1f;
    public float offsetY = 1f;
    private Vector2 AttackAreaPos;

    public LayerMask enemyLayer;

    public LayerMask destructiveLayer;


    [Header("闪避")]
    public bool isDodge = false;
    public float dodgeForce; // 闪避推力
    public float dodgeDuration = 0f; // 闪避持续时间
    public float dodgeCooldown = 2f; // 闪避冷却时间
    public bool isDodgeCoolDown = false; // 是否处于闪避冷却状态

    [Header("受伤")]
    public bool isDead;
    public bool isHurt;


    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    private IState currentState; //当前状态

    // 字典 Dictionary<状态类型, 状态>
    private Dictionary<PlayerStateType, IState> states = new Dictionary<PlayerStateType, IState>();


    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        states.Add(PlayerStateType.Idle, new PlayerIdleState(this));
        states.Add(PlayerStateType.Attack, new PlayerAttackState(this));
        states.Add(PlayerStateType.Dead, new PlayerDeadState(this));
        states.Add(PlayerStateType.Hurt, new PlayerHurtState(this));
        states.Add(PlayerStateType.Move, new PlayerMoveState(this));
        states.Add(PlayerStateType.Dodge, new PlayerDodgeState(this));

        ChangeState(PlayerStateType.Idle);

        playerInput.EnableInput();

    }

    protected override void OnEnable()
    {
        playerInput.onMove += Move;
        playerInput.onStopMove += StopMove;
        playerInput.onAttack += Attack;
        playerInput.onDodge += Dodge;
        base.OnEnable();
    }

    private void OnDisable()
    {
        playerInput.onMove -= Move;
        playerInput.onAttack -= Attack;
        playerInput.onDodge -= Dodge;
        playerInput.onStopMove -= StopMove;
    }

    public void ChangeState(PlayerStateType stateType)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[stateType];
        currentState.OnEnter();
    }

    private void Update()
    {
        currentState.OnUpdate();
    }
    private void FixedUpdate()
    {
        currentState.OnFixedUpdate();
    }

    #region 移动
    public void Move(Vector2 moveInput)
    {
        inputDirection = moveInput;
    }

    public void Move()
    {
        currentSpeed = isAttack ? attackSpeed : moveSpeed;
        rb.linearVelocity = inputDirection * currentSpeed;
        if (inputDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void StopMove()
    {
        inputDirection = Vector2.zero;

    }
    #endregion

    #region 翻滚
    public void Dodge()
    {
        if (!isDodge && !isDodgeCoolDown)
        {
            isDodge = true;
        }
    }

    public void DodgeCoolDown()
    {
        StartCoroutine(nameof(DodgeOnCoolDownCoroutine));
    }

    public IEnumerator DodgeOnCoolDownCoroutine()
    {
        yield return new WaitForSeconds(dodgeCooldown);
        isDodgeCoolDown = false;
    }

    #endregion

    #region 受伤

    public void PlayerHurt()
    {
        TimeController.Instance.SetBulletTime();
        isHurt = true;
    }

    #endregion


    #region 死亡

    public void PlayerDead()
    {
        isDead = true;
        playerInput.DisableAllInput();
        ChangeState(PlayerStateType.Dead);
        UIManager.Instance.ShowGameOverPanel();
    }

    #endregion

    #region 攻击

    public void Attack()
    {
        if (!isDodge)
        {
            animator.SetTrigger("AttackTrigger");
            isAttack = true;
        }

    }
    void AttackAnimEvent(float isAttack)
    {
        AttackAreaPos = transform.position;

        offsetX = spriteRenderer.flipX ? -Mathf.Abs(offsetX) : Mathf.Abs(offsetX);
        AttackAreaPos.x += offsetX;
        AttackAreaPos.y += offsetY;

        Collider2D[] EnemyHitColliders = Physics2D.OverlapBoxAll(AttackAreaPos, AttackSize, 0f, enemyLayer);
        Collider2D[] DestructiveHitColliders = Physics2D.OverlapBoxAll(AttackAreaPos, AttackSize, 0f, destructiveLayer);

        // 判断是否为敌人
        foreach (Collider2D hitCollider in EnemyHitColliders)
        {
            hitCollider.GetComponent<Charactor>().TakeDamage(attackDamage * isAttack);
        }
        // 判断是否为可破坏体
        foreach (Collider2D hitCollider in DestructiveHitColliders)
        {
            hitCollider.GetComponent<Destructible>().DestroyObject();
        }


    }

    #endregion

    public void UIUpdateHealthSlider()
    {
        UIManager.Instance.UpdateHpSlider(currentHealth,maxHealth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackAreaPos, AttackSize);
    }

}
