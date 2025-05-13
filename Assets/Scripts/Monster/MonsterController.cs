using System;
using System.Collections;
using System.Collections.Generic;
using Monster;
using Monster.AI;
using Monster.ScriptableObject;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public MonsterDropInfo monsterDropInfo;
    public GameObject monsterDropPrefab;
    
    private MonsterConfig monsterConfig;
    private float maxHealth;
    private float currentHealth;
    private BaseAIController aiController;
        
    private float knockBackTimer;
    private float knockBackCooldown;

    [SerializeField] private HPBarController hpBar;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform shadow;
    
    [SerializeField] private BoxCollider2D bodyCollider;
    [SerializeField] private BoxCollider2D hitBoxCollider;
   

    [SerializeField] private MonsterEventChannelSO killedMonster;
    
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

        bodyCollider.size = new Vector2(config.colliderSizeX, config.colliderSizeY);
        bodyCollider.offset = new Vector2(config.colliderOffX, config.colliderOffY);
        hitBoxCollider.size = new Vector2(config.colliderSizeX, config.colliderSizeY);
        hitBoxCollider.offset = new Vector2(config.colliderOffX, config.colliderOffY);
        
    }
    
    public void TakeDamage(float Damage)
    {
        Debug.Log("몬스터 피격 성공 Damage"+Damage);
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
        monsterDropInfo = new MonsterDropInfo
        {
            MonsterDropPosition = transform.position,
            MonsterDropQuantity = monsterConfig.goldQuantity,
            monsterDropPrefab = monsterDropPrefab
        };
        killedMonster.Raise(this);
        Destroy(gameObject);
    }
}
