using UnityEngine;
// <summary>
// 攻击状态
// </summary>

public class PlayerAttackState : IState
{
    private Player player;
    // private AnimatorStateInfo info;

    public PlayerAttackState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        player.animator.SetBool("isAttack", player.isAttack);
    }
    public void OnFixedUpdate()
    {
        player.Move();
        // Attack();
    }

    public void OnUpdate()
    {

        if (player.isHurt)
        {
            player.ChangeState(PlayerStateType.Hurt);
        }

        if (player.isAttack == false)
        {
            player.ChangeState(PlayerStateType.Idle);
        }
    }

    public void OnExit()
    {
    }

}
