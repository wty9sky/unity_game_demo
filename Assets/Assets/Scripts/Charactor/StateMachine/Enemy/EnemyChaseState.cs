using UnityEngine;
// <summary>
// 追击状态
// </summary>

public class EnemyChaseState : IState
{
    private Enemy enemy;

    public EnemyChaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }


    public void OnEnter()
    {
        enemy.animator.Play("SkeletonWalk");
    }
    public void OnFixedUpdate()
    {
        enemy.Move();
    }

    public void OnUpdate()
    {
        if (enemy.isHurt)
        {
            enemy.ChangeState(EnemyStateType.Hurt);
        }
        enemy.GetPlayerTransform(); // 获取玩家位置
        enemy.AiFindPath(); // 寻路

        if (enemy.player != null)
        {
            // 判断路径点是否未考没有
            if (enemy.pathPointList == null && enemy.pathPointList.Count <= 0)
            {
                return;
            }
            if (enemy.distance <= enemy.attackDistance)
            { // 玩家在攻击范围内，切换攻击状态
                enemy.ChangeState(EnemyStateType.Attack);
            }
            else
            {
                // 移动到下一个路径点
                Vector2 direction = enemy.pathPointList[enemy.currentIndex] - enemy.transform.position;
                enemy.MovementInput = direction.normalized;
            }
        }
        else
        {
            // 范围外就不在追击，切换到Idle状态
            enemy.ChangeState(EnemyStateType.Idle);
        }
    }

    public void OnExit()
    {
    }
}
