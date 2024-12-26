using UnityEngine;
// <summary>
// 死亡状态
// </summary>

public class EnemyDeadState : IState
{
    private Enemy enemy;

    public EnemyDeadState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        enemy.animator.Play("SkeletonDead");
        enemy.rb.linearVelocity = Vector2.zero; //待机的时候不要移动
        enemy.enemyCollider.enabled = false; //禁用碰撞体
    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
