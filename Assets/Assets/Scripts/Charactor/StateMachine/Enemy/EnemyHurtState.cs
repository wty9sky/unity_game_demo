using UnityEngine;
// <summary>
// 受伤状态
// </summary>

public class EnemyHurtState : IState
{
    private Enemy enemy;
    private Vector2 direction;

    private float timer;

    public EnemyHurtState(Enemy enemy)
    {
        this.enemy = enemy;
    }


    public void OnEnter()
    {
        enemy.animator.Play("SkeletonHurt");
    }

    public void OnFixedUpdate()
    {
        if (timer <= enemy.knockbackForceDuration)
        {
            enemy.rb.AddForce(direction * enemy.knockbackForce, ForceMode2D.Impulse);
            timer += Time.fixedDeltaTime;
        }
        else
        {
            timer = 0;
            enemy.isHurt = false;
            // enemy.isKnockback = false;
            enemy.ChangeState(EnemyStateType.Idle);
        }
    }

    public void OnUpdate()
    {
        if (enemy.isKnockback)
        {
            if (enemy.player != null)
            {
                direction = (enemy.transform.position - enemy.player.position).normalized ;
            }
            else
            {
                Transform player = GameObject.FindWithTag("Player").transform;
                direction = (enemy.transform.position - player.position).normalized;
            }
        }

    }

    public void OnExit()
    {
        enemy.isHurt = false;
    }
}
