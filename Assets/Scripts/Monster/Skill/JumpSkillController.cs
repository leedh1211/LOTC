using System.Collections;
using UnityEngine;

namespace Monster.Skill
{
    public class JumpSkillController : MonoBehaviour
    {
        private float jumpDamage;
        private Vector2 jumpStartPos;
        private Vector2 jumpTargetPos;
        private float jumpDuration;
        private float impactRadius;

        private bool isJumping;
        private Collider2D col;
        private Transform shadowTransform;
        private SpriteRenderer shadowRenderer;

        public void JumpInit(float damage, Transform player, float range, float duration, float radius, bool isFly, Transform shadow, SpriteRenderer shadowRen )
        {
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
                float heightFactor = Mathf.Sin(t * Mathf.PI); // 0→1→0

                Vector2 flatPos = Vector2.Lerp(jumpStartPos, landingTarget, t);

                transform.position = flatPos + Vector2.up * heightFactor * jumpHeight;

                shadowTransform.position = flatPos + Vector2.down * 0.7f; //그림자위치 잡기

                float scale = Mathf.Lerp(1f, 1.2f, heightFactor);
                transform.localScale = new Vector3(scale, scale, 1);

                float shadowScaleFactor = Mathf.Lerp(1f, 0.5f, heightFactor);
                shadowTransform.localScale = baseShadowScale * shadowScaleFactor;

                Color shadowColor = baseShadowColor;
                shadowColor.a = Mathf.Lerp(baseShadowColor.a, 0.3f, heightFactor);
                shadowRenderer.color = shadowColor;

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = landingTarget;
            transform.localScale = Vector3.one;

            shadowTransform.localScale = baseShadowScale;
            shadowRenderer.color = baseShadowColor;

            isJumping = false;
            col.enabled = true;

            DoImpact();
        }

        private void DoImpact()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(jumpTargetPos, impactRadius, LayerMask.GetMask("Player"));
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out PlayerController player))
                {
                    // 플레이어 공격 함수 추가 필요
                }
            }
        }
    }
}