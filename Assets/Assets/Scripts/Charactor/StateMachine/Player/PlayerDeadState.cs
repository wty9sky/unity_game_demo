using UnityEngine;
// <summary>
// 死亡状态
// </summary>

public class PlayerDeadState : IState
{
    private Player player;

    public PlayerDeadState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {
        player.animator.SetBool("isDead", player.isDead);
    }

    public void OnExit()
    {

    }
}
