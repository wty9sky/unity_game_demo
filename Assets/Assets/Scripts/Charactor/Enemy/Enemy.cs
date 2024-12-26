using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Pathfinding;

// 敌人状态枚举
public enum EnemyStateType
{
    Idle,
    Chase,
    // Walk,
    Attack,
    Dead,

    Hurt,
    Patrol,

}

public class Enemy : Charactor
{
    [Header("目标")]
    public Transform player;

    [Header("巡逻")]
    public float IdleDuration; //待机持续时间才切换为巡逻状态
    public Transform[] patrolPoints; //巡逻点数组;
    public int targetPointIndex = 0; //当前目标点索引

    [Header("移动追击")]
    public float currentSpeed = 0;
    public Vector2 MovementInput { get; set; }

    public float chaseDistance = 3f; //追击距离

    public float attackDistance = 0.8f; //攻击距离

    // [Header("寻路")]
    private Seeker seeker;
    [HideInInspector] public List<Vector3> pathPointList;  //用于遍历路径点 
    [HideInInspector] public int currentIndex = 0; //当前路径点索引
    private float pathGenerateInterval = 0.5f; //路径生成时间
    private float pathGenerateTimer = 0.5f; // 计时器


    [Header("攻击")]
    public float attackDamage; //攻击伤害 
    public bool isAttack = true;

    [HideInInspector] public float distance;
    public LayerMask playerLayer; //玩家图层 
    public float attackDuration = 2f; //攻击冷却时间


    [Header("受伤击退")]
    public bool isHurt;
    public bool isKnockback = true; //是否击退
    public float knockbackForce = 10f; //击退力度
    public float knockbackForceDuration = 0.1f; //击退时间


    public float hurtDuration = 0.5f; //受伤时间


    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Collider2D enemyCollider;

    private IState currentState; //当前状态

    // 字典 Dictionary<状态类型, 状态>
    private Dictionary<EnemyStateType, IState> states = new Dictionary<EnemyStateType, IState>();

    private PickupSpawner pickupSpawner; // 掉落道具

    private void Start()
    {
        EnemyManager.Instance.EnemyCount++;
    }

    private void OnDestroy()
    {
        EnemyManager.Instance.EnemyCount--;
    }

    private void Awake()
    {
        seeker = GetComponent<Seeker>(); // 寻路组件
        spriteRenderer = GetComponent<SpriteRenderer>();  // 精灵渲染器组件
        rb = GetComponent<Rigidbody2D>(); // 刚体组件
        enemyCollider = GetComponent<Collider2D>();  // 碰撞器组件
        animator = GetComponent<Animator>(); // 动画 控制器组件 
        pickupSpawner = GetComponent<PickupSpawner>(); // 掉落道具

        // 实例化敌人状态
        states.Add(EnemyStateType.Idle, new EnemyIdleState(this));
        states.Add(EnemyStateType.Chase, new EnemyChaseState(this));
        states.Add(EnemyStateType.Attack, new EnemyAttackState(this));
        states.Add(EnemyStateType.Dead, new EnemyDeadState(this));
        states.Add(EnemyStateType.Hurt, new EnemyHurtState(this));
        states.Add(EnemyStateType.Patrol, new EnemyPatrolState(this));
        // 切换状态
        ChangeState(EnemyStateType.Idle);
    }


    public void ChangeState(EnemyStateType stateType)
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

    // 判定玩家是否在追击范围内
    public void GetPlayerTransform()
    {
        Collider2D[] chaseColliders = Physics2D.OverlapCircleAll(transform.position, chaseDistance, playerLayer);
        if (chaseColliders.Length > 0)
        { // 当玩家在追击范围内
            player = chaseColliders[0].transform; // 获取玩家位置
            distance = Vector2.Distance(player.position, transform.position);
        }
        else
        {
            player = null;// 当玩家不在追击范围内
        }
    }



    #region 敌人近战攻击帧时间
    private void AttackAnimEvent()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackDistance, playerLayer);
        foreach (Collider2D hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Charactor>().TakeDamage(attackDamage);
        }
    }

    public void AttackColdown()
    {
        StartCoroutine(nameof(AttackCoolDownCoroutine));
    }

    // 攻击冷却协程
    private IEnumerator AttackCoolDownCoroutine()
    {
        yield return new WaitForSeconds(attackDuration);
        isAttack = true;
    }

    #endregion

    #region 受伤

    public void EnemyHurt()
    {
        isHurt = true;
    }

    #endregion


    #region 死亡

    public void EnemyDead()
    {
        ChangeState(EnemyStateType.Dead);
        // pickupSpawner.DropItems();
    }
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
        pickupSpawner.DropItems();
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    #region 移动
    public void Move()
    {
        if (MovementInput.magnitude > 0.1f && currentSpeed >= 0)
        {
            rb.linearVelocity = MovementInput * currentSpeed;
            if (MovementInput.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            if (MovementInput.x > 0)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    #endregion

    #region 寻路
    public void AiFindPath()
    {
        if (player == null) return;
        pathGenerateTimer += Time.deltaTime;
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            generatePath(player.position);
            pathGenerateTimer = 0f;
        }
        // 当路径点为空 或者 路径点数量为0 重新生成路径点
        if (pathPointList == null || pathPointList.Count <= 0 || pathPointList.Count <= currentIndex)
        {
            generatePath(player.position);
        }
        else if (currentIndex < pathPointList.Count)
        {
            if (Vector2.Distance(transform.position, pathPointList[currentIndex]) <= 0.1f)
            {
                currentIndex++;
                if (currentIndex >= pathPointList.Count)
                {
                    generatePath(player.position);
                }

            }
        }
    }



    // 获取路径点
    public void generatePath(Vector3 targetPos)
    {
        // 起点 终点 回调函数
        seeker.StartPath(transform.position, targetPos, (Path) =>
        {
            currentIndex = 0;
            pathPointList = Path.vectorPath;
        });
    }

    #endregion


}
