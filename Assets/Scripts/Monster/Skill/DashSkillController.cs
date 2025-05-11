using System.Collections;
using UnityEngine;

namespace Monster.Skill
{
    public class DashSkillController : MonoBehaviour
    {
        private float damage;
        private float dashRange;
        private float dashSpeed;
        private Vector2 dashDirection;
        private Vector2 startPosition;

        private bool isDashing;

        private Rigidbody2D rb;
        
        public void DashInit( Vector2 dir, float damage,Sprite sprite, float range, float speed , float delay) 
        {
            this.damage = damage;
            dashDirection = dir.normalized;
            dashRange = range;
            dashSpeed = speed;
            startPosition = transform.position;
            isDashing = true;

            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D가 필요합니다.");
                return;
            }

            StartCoroutine(DashCoroutine());
        }
        
        private IEnumerator DashCoroutine()
        {
            rb.velocity = dashDirection * dashSpeed;

            while (Vector2.Distance(startPosition, transform.position) < dashRange)
            {
                yield return null;
            }

            EndDash();
        }
        
        private void EndDash()
        {
            isDashing = false;
            if (rb != null)
                rb.velocity = Vector2.zero;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isDashing) return;

            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (other.TryGetComponent(out PlayerController player))
                {
                    Debug.Log("체력깎기"+damage);
                }

                EndDash();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                EndDash();
            }
        }

    }
}