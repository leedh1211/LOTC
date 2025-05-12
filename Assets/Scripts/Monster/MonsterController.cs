using System;
using System.Collections;
using System.Collections.Generic;
using Monster;
using Monster.AI;
using Monster.ScriptableObject;
using UnityEngine;

public class MonsterController : MonoBehaviour
{

    private float maxHealth;
    private float currentHealth;
    private MonsterConfig monsterConfig;
    private BaseAIController aiController;
        
    private float knockBackTimer;
    private float knockBackCooldown;

    [SerializeField] private HPBarController hpBar;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private Transform shadow;
   

    [SerializeField] private MonsterEventChannelSO killedMonster;

    void Update()
    {
        if (knockBackTimer > 0f)
        {
            knockBackTimer -= Time.deltaTime;

            if (knockBackTimer <= 0f)
            {
                TakeDamage(10);
                knockBackTimer = 0f;
            }
        }
    }

    public void Init(MonsterConfig config, GameObject player)
    {
        monsterConfig = config;
        if (monsterConfig.isFly)
        {
            Physics2D.IgnoreLayerCollision(
                gameObject.layer,
                LayerMask.NameToLayer("Obstacle"),
                true
            );
        }
        maxHealth = config.monsterStatData.maxhealth;
        currentHealth = config.monsterStatData.maxhealth;
        knockBackTimer = config.monsterStatData.knockbackCooldown;
        knockBackCooldown = config.monsterStatData.knockbackCooldown;
        
        aiController = GetComponent<BaseAIController>();
        aiController.Init(monsterConfig, GetComponent<Rigidbody2D>(), player);
        
        spriteRenderer.sprite = monsterConfig.SpriteOverride;
        animator.runtimeAnimatorController = monsterConfig.AnimatorOverrideController;
        Vector2 spriteSize = spriteRenderer.bounds.size;
        float shadowWidth = spriteSize.x * 0.6f;
        float shadowHeight = spriteSize.y * 0.3f;
        shadow.localScale = new Vector3(shadowWidth, shadowHeight, 1f);
        
        if (monsterConfig.monsterType == MonsterType.boss)
        {
            transform.localScale *= 2.0f;
        }
        else
        {
            transform.localScale *= 0.9f;    
        }
    }
    
    public void TakeDamage(float Damage)
    {
        if (currentHealth < Damage)
        {
            currentHealth = 0;
            MobDeath();
        }
        else
        {
            currentHealth -= Damage;
            currentHealth = Mathf.Max(0, currentHealth);    
        }
        hpBar.SetFill(currentHealth / maxHealth);
        knockBackTimer = knockBackCooldown;
    }

    public void MobDeath()
    {
        killedMonster.Raise(this);
        Destroy(gameObject);
    }
}
