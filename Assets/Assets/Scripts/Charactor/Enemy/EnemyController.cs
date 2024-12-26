using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    // [SerializeField] private float currentSpeed = 0;
    // public Vector2 MovementInput { get; set; }
    // [SerializeField] private bool isAttack = true;
    // [SerializeField] private float attackCoolDuration = 1;

    // private Rigidbody2D rb;
    // private SpriteRenderer sr;
    // private Animator anim;
    // private Collider2D enemyCollider;

    // private bool isDead;
    // private bool isHurt;

    // [Header("击退")]
    // [SerializeField] private bool isKnockBack = true;
    // [SerializeField] private float knockBackForce = 10f;

    // [SerializeField] private float knockBackDuration = 0.1f;



    // private void Awake()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     sr = GetComponent<SpriteRenderer>();
    //     anim = GetComponent<Animator>();
    //     enemyCollider = GetComponent<Collider2D>();
    // }

    // private void FixedUpdate()
    // {
    //     if(!isHurt && !isDead){
    //     Move();
    //     }
    //     SetAnimation();
    // }
    // void Move()
    // {
    //     if (MovementInput.magnitude > 0.1f && currentSpeed >= 0)
    //     {
    //         rb.linearVelocity = MovementInput * currentSpeed;
    //         if (MovementInput.x < 0)
    //         {
    //             sr.flipX = false;
    //         }
    //         if (MovementInput.x > 0)
    //         {
    //             sr.flipX = true;
    //         }
    //     }
    //     else
    //     {
    //         rb.linearVelocity = Vector2.zero;
    //     }
    // }
    // public void Attack()
    // {
    //     if (isAttack)
    //     {
    //         isAttack = false;
    //         StartCoroutine(nameof(AttackCoroutine));
    //     }
    // }

    // public void EnemyHurt(){
    //     isHurt = true;
    //     anim.SetTrigger("hurtTrigger");
    // }

    // public void EnemyKnockBack(Vector3 pos){
    //     if(!isKnockBack || isDead){
    //         return;
    //     }
    //     StartCoroutine(KnockBackCoroutine(pos));

    // }

    // IEnumerator KnockBackCoroutine(Vector3 pos)
    // {
    //     var direction = (transform.position - pos).normalized;
    //     rb.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
    //     yield return new WaitForSeconds(knockBackDuration);
    //     isHurt = false;
    // }

    // public void EnemyDead(){
    //     rb.linearVelocity = Vector2.zero;
    //     isDead = true;
    //     enemyCollider.enabled = false;
    // }


    // void SetAnimation()
    // {
    //     anim.SetBool("isWalk", MovementInput.magnitude > 0);
    //     anim.SetBool("isDead", isDead);
    // }

    // public void DestroyEnemy(){
    //     Destroy(this.gameObject);
    // }

    // IEnumerator AttackCoroutine()
    // {
    //     anim.SetTrigger("isAttack");

    //     yield return new WaitForSeconds(attackCoolDuration);
    //     isAttack = true;
    // }
}
