using UnityEngine;
// <summary>
// 受伤状态
// </summary>

public class PlayerHurtState : IState
{
    private Player player;

    private AnimatorStateInfo info;

    private float timer;

    public PlayerHurtState(Player player)
    {
        this.player = player;
    }


    public void OnEnter()
    {
        player.animator.SetTrigger("hurt");
    }

    public void OnFixedUpdate()
    {
        player.Move();
    }

    public void OnUpdate()
    {
        info = player.animator.GetCurrentAnimatorStateInfo(0);

        if(info.normalizedTime >= 0.95f){
            player.ChangeState(PlayerStateType.Idle); 
        }

    }

    public void OnExit()
    {
        player.isHurt = false;
    }
}
