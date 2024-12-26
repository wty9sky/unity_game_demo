using UnityEngine;
// <summary>
// 追击状态
// </summary>

public class PlayerDodgeState : IState
{
    private Player player;

    private float dodgeTimer = 0f;

    public PlayerDodgeState(Player player)
    {
        this.player = player;
    }


    public void OnEnter()
    {
    }
    public void OnFixedUpdate()
    {
        player.Move();
        Dodge();
    }

    public void OnUpdate()
    {
        player.animator.SetBool("isDodge", player.isDodge);

        if (player.isHurt)
        {
            player.ChangeState(PlayerStateType.Hurt);
        }

        if (player.isDodge == false)
        {
            player.ChangeState(PlayerStateType.Idle);
        }
    }

    public void OnExit()
    {
    }


    void Dodge()
    {
        if (!player.isDodgeCoolDown)
        {
            if (dodgeTimer <= player.dodgeDuration)
            {
                // 施加推力
                player.rb.AddForce(player.inputDirection * player.dodgeForce, ForceMode2D.Impulse);
                dodgeTimer += Time.fixedDeltaTime;
            }
            else
            {
                // 完成闪避
                player.isDodge = false;
                player.isDodgeCoolDown = true;
                player.DodgeCoolDown();
                dodgeTimer = 0f;
            }
        }
    }
}
