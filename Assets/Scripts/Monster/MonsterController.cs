using System.Collections;
using System.Collections.Generic;
using Monster.AI;
using Monster.ScriptableObject;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private GameObject monsterPrefab;
    public MonsterConfig monsterConfig;
    private BaseAIController aiController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private Transform shadow;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        aiController = GetComponent<BaseAIController>();
        aiController.Init(monsterConfig, GetComponent<Rigidbody2D>(), player);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = monsterConfig.SpriteOverride;
        animator.runtimeAnimatorController = monsterConfig.AnimatorOverrideController.runtimeAnimatorController;
        transform.localScale *= 0.9f;
        
        if (bodyCollider is BoxCollider2D box)
        {
            box.size = spriteRenderer.bounds.size;
        }
        Vector2 spriteSize = spriteRenderer.bounds.size;
        
        float shadowWidth = spriteSize.x * 0.6f;
        float shadowHeight = spriteSize.y * 0.3f;

        shadow.localScale = new Vector3(shadowWidth, shadowHeight, 1f);
    }
}
