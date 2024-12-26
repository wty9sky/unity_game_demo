using UnityEngine;
// <summary>
// 攻击状态
// </summary>

public class EnemyAttackState : IState
{
    private Enemy enemy;
    private AnimatorStateInfo info;

    public EnemyAttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        if (enemy.isAttack)
        {
            enemy.animator.Play("SkeletonAttack");
            enemy.isAttack = false;
            enemy.AttackColdown();
        }
    }
    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        if(enemy.isHurt){
            enemy.ChangeState(EnemyStateType.Hurt);
        }
        enemy.rb.linearVelocity = Vector2.zero;
        float x = enemy.player.position.x - enemy.transform.position.x;
        if (x > 0)
        {
            enemy.spriteRenderer.flipX = true;
        }
        else
        {
            enemy.spriteRenderer.flipX = false;
        }
        info = enemy.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1f)
        {
            enemy.ChangeState(EnemyStateType.Idle);
        }

    }

    public void OnExit()
    {
    }

}
