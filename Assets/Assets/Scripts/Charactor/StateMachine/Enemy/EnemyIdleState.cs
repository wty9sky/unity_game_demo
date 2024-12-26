using UnityEngine;
// <summary>
// 空闲状态
// </summary>

public class EnemyIdleState : IState
{
    private Enemy enemy;

    private float Timer = 0f;

    public EnemyIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        enemy.animator.Play("Skelet onIdle");
        enemy.rb.linearVelocity = Vector2.zero; //待机的时候不要移动
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        if (enemy.isHurt)
        {
            enemy.ChangeState(EnemyStateType.Hurt);
        }
        enemy.GetPlayerTransform(); // 获取玩家位置
        if (enemy.player != null)
        {
            if (enemy.distance > enemy.attackDistance) // 玩家在攻击范围内，切换追击状态
            {
                enemy.ChangeState(EnemyStateType.Chase); // 切换状态
            }
            else if (enemy.distance <= enemy.attackDistance)
            {
                enemy.ChangeState(EnemyStateType.Attack); // 切换状态
            }
        }
        else
        {
            if (Timer <= enemy.IdleDuration)
            { //如果玩家为空，则等待一定时间切换到巡逻状态
                Timer += Time.deltaTime;
            }
            else
            {
                Timer = 0;
                enemy.ChangeState(EnemyStateType.Patrol);
            }
        }
    }

    public void OnExit()
    {

    }


}
