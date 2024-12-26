using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Charactor : MonoBehaviour
{
    [Header("属性")]
    public float maxHealth;
    
    public float currentHealth;

    [Header("UI")]
    public UnityEvent<float, float> OnHealthUpdate;

    [Header("无敌")]
    public bool invulnerable; // 是否无敌

    public float invulnerableDuration; // 无敌时间

    public UnityEvent OnHurt;
    public UnityEvent OnDead;


    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
        // OnHealthUpdate?.Invoke(currentHealth, maxHealth); //初始化
    }

    public void Start()
    {
        OnHealthUpdate?.Invoke(currentHealth, maxHealth); //初始化
    }


    public virtual void TakeDamage(float damage)
    {
        if (invulnerable) return;
        if (currentHealth - damage > 0f)
        {
            currentHealth -= damage;
            StartCoroutine(nameof(InvulnerableCoroutine)); // 启动无敌时间协程
            OnHurt?.Invoke();
        }
        else
        {
            Die();
        }
        GameManager.Instance.ShowText("-" + damage,transform.position,Color.red);
        OnHealthUpdate?.Invoke(currentHealth, maxHealth);
    }

    public virtual void RestoreHealth(float healAmount)
    {
        if (currentHealth == maxHealth) return;
        if (currentHealth + healAmount <= maxHealth)
        {
            currentHealth += healAmount;
        }
        else
        {
            currentHealth = maxHealth;
        }
        OnHealthUpdate?.Invoke(currentHealth, maxHealth);
    }

    void Die()
    {
        currentHealth = 0f;
        // Destroy(this.gameObject);
        // OnHealthUpdate?.Invoke(currentHealth, maxHealth);
        OnDead?.Invoke();
    }

    protected virtual IEnumerator InvulnerableCoroutine()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }


}
