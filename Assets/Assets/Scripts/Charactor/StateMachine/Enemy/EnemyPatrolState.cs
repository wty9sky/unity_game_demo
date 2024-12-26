using UnityEngine;
// <summary>
// 敌人巡逻状态
// </summary>

public class EnemyPatrolState : IState
{
    private Enemy enemy;

    private Vector2 direction;

    private float stopTime = 0f;
    private float stopThreshold = 3f; // 停止时间阈值

    public EnemyPatrolState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        GeneratePartrolPoint(); //进入巡逻状态随机生成巡逻点
        enemy.animator.Play("Walk"); // 播放巡逻动画
    }
    public void OnFixedUpdate()
    {
        enemy.Move();
    }

    public void OnUpdate()
    {
        // 检测是否受伤
        if (enemy.isHurt)
        {
            enemy.ChangeState(EnemyStateType.Hurt);
        }

        // 在巡逻中发现玩家，切换到追击状态

        enemy.GetPlayerTransform();
        if (enemy.player != null)
        {
            enemy.ChangeState(EnemyStateType.Chase);
        }

        // 当路径点为空时，随机生成巡逻点
        if (enemy.pathPointList == null || enemy.pathPointList.Count <= 0)
        {
            GeneratePartrolPoint();
        }
        else
        {
            // 当敌人达到当前路径点时，递增索引currentIndex并进行路径计算
            if (Vector2.Distance(enemy.transform.position, enemy.pathPointList[enemy.currentIndex]) <= 0.1f)
            {
                enemy.currentIndex++;
                if (enemy.currentIndex >= enemy.pathPointList.Count)
                {
                    // enemy.currentIndex = 0;
                    enemy.ChangeState(EnemyStateType.Idle);
                }
                else
                {
                    direction = enemy.pathPointList[enemy.currentIndex] - enemy.transform.position;
                    enemy.MovementInput = direction.normalized;
                }
                // enemy.generatePath(enemy.pathPointList[enemy.currentIndex]);
            }
            else
            {
                // 相撞处理
                if (enemy.rb.linearVelocity.magnitude < enemy.currentSpeed && enemy.currentIndex < enemy.pathPointList.Count)
                {
                    if (enemy.rb.linearVelocity.magnitude == 0)
                    {
                        direction = enemy.pathPointList[enemy.currentIndex] - enemy.transform.position;
                        enemy.MovementInput = direction.normalized;
                    }
                    else
                    {
                        enemy.ChangeState(EnemyStateType.Idle);
                    }
                }
            }

            if(stopTime >= stopThreshold){
                enemy.ChangeState(EnemyStateType.Idle);
                stopTime = 0;
            }
        }

    }

    public void OnExit()
    {
    }

    public void GeneratePartrolPoint()
    {
        // 随机生成巡逻点索引

        // 排除当前索引
        while (true)
        {
            int i = Random.Range(0, enemy.patrolPoints.Length);

            if (enemy.targetPointIndex != i)
            {
                enemy.targetPointIndex = i;
                break; // 退出循环
            }
        }
        // 把巡逻点给生成路径点函数
        enemy.generatePath(enemy.patrolPoints[enemy.targetPointIndex].position);

    }
}
