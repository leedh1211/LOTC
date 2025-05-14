using System;
using Monster.ScriptableObject.Skill;
using UnityEngine;

namespace Monster.Skill
{
    public class ProjectileController : MonoBehaviour
    {
        public float Damage => damage;
        
        Vector2 direction;
        private float damage;
        private float speed;
        private Rigidbody2D rigidbody2D;

        public void Init(Vector2 dir, float projectileSpeed, float projectileDamage)
        {
            direction = dir;
            speed = projectileSpeed;
            damage = projectileDamage;
            if (TryGetComponent(out Rigidbody2D body))
            {
                rigidbody2D = body;
            }
        }


        public void Start()
        {
        }

        public void Update()
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            bool isInView = viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;
            if (!isInView)
            {
                Destroy(gameObject);
            }
        }

        public void FixedUpdate()
        {
            rigidbody2D.velocity = direction.normalized * speed;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log(other.gameObject.name);
                Destroy(gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Enemy_projectile"))
            {
                return;
            }else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                Destroy(gameObject);
            }
            
        }
    }
}