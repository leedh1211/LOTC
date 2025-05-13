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

        ResetColliderToSpriteSize(ref bodyCollider, spriteRenderer);
        ResetColliderToSpriteSize(ref hitBoxCollider, spriteRenderer);
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
        monsterDropInfo = new MonsterDropInfo
        {
            MonsterDropPosition = transform.position,
            MonsterDropQuantity = monsterConfig.goldQuantity,
            monsterDropPrefab = monsterDropPrefab
        };
        killedMonster.Raise(this);
        Destroy(gameObject);
    }
    
    private void ResetColliderToSpriteSize(ref BoxCollider2D boxCollider, SpriteRenderer spriteRenderer)
    {
        if (boxCollider == null || spriteRenderer == null || spriteRenderer.sprite == null) return;

        Sprite sprite = spriteRenderer.sprite;

        Rect textureRect = sprite.textureRect;
        float width = textureRect.width / sprite.pixelsPerUnit;
        float height = textureRect.height / sprite.pixelsPerUnit;

        Vector2 pivotNormalized = sprite.pivot / sprite.rect.size;
        float offsetX = (0.5f - pivotNormalized.x) * width;
        float offsetY = (0.5f - pivotNormalized.y) * height;
        
        Vector3 spriteLocalPosition = spriteRenderer.transform.localPosition;
        offsetX += spriteLocalPosition.x;
        offsetY += spriteLocalPosition.y;

        boxCollider.size = new Vector2(width, height);
        boxCollider.offset = new Vector2(offsetX, offsetY);
    }
    
    private void ResetColliderToNonTransparentArea(ref BoxCollider2D collider, SpriteRenderer renderer)
    {
        Texture2D tex = renderer.sprite.texture;
        Color[] pixels = tex.GetPixels();

        int width = tex.width;
        int height = tex.height;

        int minX = width, minY = height, maxX = 0, maxY = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = pixels[y * width + x];
                if (pixel.a > 0.01f)
                {
                    minX = Mathf.Min(minX, x);
                    maxX = Mathf.Max(maxX, x);
                    minY = Mathf.Min(minY, y);
                    maxY = Mathf.Max(maxY, y);
                }
            }
        }

        // 영역 계산
        int trimmedWidth = maxX - minX;
        int trimmedHeight = maxY - minY;

        float unitWidth = trimmedWidth / renderer.sprite.pixelsPerUnit;
        float unitHeight = trimmedHeight / renderer.sprite.pixelsPerUnit;

        float offsetX = ((minX + trimmedWidth / 2f) - renderer.sprite.pivot.x) / renderer.sprite.pixelsPerUnit;
        float offsetY = ((minY + trimmedHeight / 2f) - renderer.sprite.pivot.y) / renderer.sprite.pixelsPerUnit;

        collider.size = new Vector2(unitWidth, unitHeight);
        collider.offset = new Vector2(offsetX, offsetY);
    }
}
