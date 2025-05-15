using System;
using System.Collections;
using UnityEngine;

namespace Monster.Skill
{
    public class JumpSkillController : MonoBehaviour
    {
        public Action OnJumpFinished;
        
        private float jumpDamage;
        private Vector2 jumpStartPos;
        private Vector2 jumpTargetPos;
        private float jumpDuration;
        private float impactRadius;

        private bool isJumping;
        private Collider2D col;
        private Transform shadowTransform;
        private SpriteRenderer shadowRenderer;
        private Vector3 originalScale;

        public void JumpInit(float damage, Transform player, float range, float duration, float radius, bool isFly, Transform shadow, SpriteRenderer shadowRen )
        {
            originalScale = transform.localScale;
            jumpDamage = damage;
            jumpStartPos = transform.position;
            jumpDuration = duration;
            impactRadius = radius;
            shadowTransform = shadow;
            shadowRenderer = shadowRen;

            col = GetComponent<Collider2D>();

            if (isFly)
                col.enabled = false;

            isJumping = true;

            Vector2 playerPos = player.position;
            Vector2 dir = (playerPos - jumpStartPos);
            float distanceToPlayer = dir.magnitude;

            if (distanceToPlayer > range)
            {
                jumpTargetPos = jumpStartPos + dir.normalized * range;
            }
            else
            {
                jumpTargetPos = playerPos;
            }

            // 점프 시작
            StartCoroutine(JumpCoroutine());
        }

        private IEnumerator JumpCoroutine()
        {
            float elapsed = 0f;
            Vector2 landingTarget = jumpTargetPos;
            float jumpHeight = 1.0f;

            Vector3 baseShadowScale = shadowTransform.localScale;
            Color baseShadowColor = shadowRenderer.color;

            while (elapsed < jumpDuration)
            {
                float t = elapsed / jumpDuration;
                float heightFactor = Mathf.Sin(t * Mathf.PI);

                Vector2 flatPos = Vector2.Lerp(jumpStartPos, landingTarget, t);
                transform.position = flatPos + Vector2.up * heightFactor * jumpHeight;

                shadowTransform.position = flatPos + Vector2.down * 0.7f;

                // 🔸 여기서 스케일을 원래대로 유지
                transform.localScale = originalScale;

                float shadowScaleFactor = Mathf.Lerp(1f, 0.5f, heightFactor);
                shadowTransform.localScale = baseShadowScale * shadowScaleFactor;

                Color shadowColor = baseShadowColor;
                shadowColor.a = Mathf.Lerp(baseShadowColor.a, 0.3f, heightFactor);
                shadowRenderer.color = shadowColor;

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = landingTarget;
            transform.localScale = originalScale;

            shadowTransform.localScale = baseShadowScale;
            shadowRenderer.color = baseShadowColor;

            isJumping = false;
            OnJumpFinished?.Invoke();
            col.enabled = true;

            DoImpact();
        }

        private void DoImpact()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(jumpTargetPos, impactRadius, LayerMask.GetMask("Player"));
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out Player player))
                {
                    player.TakeDamage(jumpDamage);
                }
            }
        }
    }
}