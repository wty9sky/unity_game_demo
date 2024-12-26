using UnityEngine;
// <summary>
// 空闲状态
// </summary>

public class PlayerIdleState : IState
{
    private Player player;

    public PlayerIdleState(Player player)
    {
        this.player = player;
    }
    public void OnEnter()
    {
        player.animator.Play("Idle");
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        player.animator.SetFloat("Speed", player.rb.linearVelocity.magnitude);
        if (player.isHurt)
        {
            player.ChangeState(PlayerStateType.Hurt);
        }

        if (player.inputDirection != Vector2.zero)
        {
            player.ChangeState(PlayerStateType.Move);
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
