using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    // public InputActions inputActions;
    // public Vector2 inputDirection;

    // private SpriteRenderer spriteRenderer;

    // [Header("移动速度")]
    // public float moveSpeed;

    // public float attackSpeed = 1f; // 攻击时玩家速度
    // private float currentSpeed; // 当前移动速度

    // private Animator animator;



    // [Header("攻击")]
    // public bool isAttack;

    // private Rigidbody2D rb;

    // [Header("闪避")]
    // public bool isDodge = false;
    // public float dodgeForce; // 闪避推力
    // public float dodgeDuration = 0f; // 闪避持续时间
    // public float dodgeTimer = 0f; // 闪避计时器

    // public bool isDead = false;

    // public float dodgeCooldown = 2f; // 闪避冷却时间
    // private bool isDodgeCoolDown = false; // 是否处于闪避冷却状态

    // private void Awake()
    // {
    //     inputActions = new InputActions();
    //     rb = GetComponent<Rigidbody2D>();
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    //     animator = GetComponent<Animator>();

    //     // 近战攻击
    //     inputActions.GamePlay.Attack.started += ctx =>
    //     {
    //         if (!isDodge)
    //         {
    //             animator.SetTrigger("AttackTrigger");
    //             isAttack = true;
    //         }
    //     };

    //     inputActions.GamePlay.Dodge.started += ctx =>
    //     {
    //         if (!isDodge && !isDodgeCoolDown)
    //         {
    //             isDodge = true;

    //         }
    //     };
    // }

    // private void OnEnable()
    // {
    //     inputActions.Enable();
    // }

    // private void OnDisable()
    // {
    //     inputActions.Disable();
    // }

    // private void Update()
    // {
    //     inputDirection = inputActions.GamePlay.Move.ReadValue<Vector2>();
    //     SetAnmiation();
    // }

    // void Dodge()
    // {
    //     if (isDodgeCoolDown)
    //     {
    //         dodgeTimer += Time.fixedDeltaTime;
    //         if (dodgeTimer >= dodgeCooldown)
    //         {
    //             isDodgeCoolDown = false;
    //             dodgeTimer = 0f;
    //         }
    //     }
    //     if (isDodge)
    //     {
    //         if (!isDodgeCoolDown)
    //         {
    //             if (dodgeTimer <= dodgeDuration)
    //             {
    //                 rb.AddForce(inputDirection * dodgeForce, ForceMode2D.Impulse);
    //                 dodgeTimer += Time.fixedDeltaTime;
    //             }
    //             else
    //             {
    //                 isDodge = false;
    //                 isDodgeCoolDown = true;
    //                 dodgeTimer = 0f;
    //             }
    //         }
    //     }
    // }

    // private void FixedUpdate()
    // {
    //     Move();
    //     Dodge();
    // }

    // public void PlayerHurt(){
    //     animator.SetTrigger("hurt");
    // }
    
    // public void PlayerDead(){
    //     isDead = true;
    //     SwitchActionMap(inputActions.UI);
    // }

    // void Move()
    // {
    //     currentSpeed = isAttack? attackSpeed : moveSpeed;
    //     rb.linearVelocity = inputDirection * currentSpeed;
    //     if (inputDirection.x > 0)
    //     {
    //         spriteRenderer.flipX = false;
    //     }
    //     else if (inputDirection.x < 0)
    //     {
    //         spriteRenderer.flipX = true;
    //     }
    // }

    // void SetAnmiation()
    // {
    //     animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    //     animator.SetBool("isAttack", isAttack);
    //     animator.SetBool("isDodge", isDodge);
    //     animator.SetBool("isDead",isDead);
    // }

    // void SwitchActionMap(InputActionMap actionMap){
    //     inputActions.Disable();
    //     actionMap.Enable();
    // }
}
