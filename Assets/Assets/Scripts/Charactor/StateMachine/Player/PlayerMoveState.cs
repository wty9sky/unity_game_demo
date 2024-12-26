using UnityEngine;
// <summary>
// 攻击状态
// </summary>

public class PlayerMoveState : IState
{
    private Player player;

    public PlayerMoveState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        // player.animator.Play("Walk");
    }
    public void OnFixedUpdate()
    {
        player.Move();
    }

    public void OnUpdate()
    {
        player.animator.SetFloat("Speed", player.rb.linearVelocity.magnitude);

        if (player.isHurt)
        {
            player.ChangeState(PlayerStateType.Hurt);
        }

        if (player.rb.linearVelocity.magnitude < 0.01f)
        {
            player.ChangeState(PlayerStateType.Idle);
        }

        if (player.isDodge)
        {
            player.ChangeState(PlayerStateType.Dodge);
        }

        if (player.isAttack)
        {
            player.ChangeState(PlayerStateType.Attack);
        }
    }

    public void OnExit()
    {
    }

}
