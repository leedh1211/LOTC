using System.Collections;
using Monster.Skill;
using UnityEngine;

namespace Monster.ScriptableObject.Skill
{
    [CreateAssetMenu(fileName = "JumpSkillData", menuName = "MonsterSkill/JumpSkill")]
    public class JumpSkillData : BaseMonsterSkillData
    {
        public float DamageRatio;
        public float Range;
        public float Duration;
        public float ImpactRadius;
        public bool IsCollide;

        public override IEnumerator Excute(MonsterConfig monsterConfig, Transform self, Transform target)
        {
            if (!self.TryGetComponent(out JumpSkillController controller))
            {
                controller = self.gameObject.AddComponent<JumpSkillController>();
            }

            Vector2 dir = (target.position - self.position).normalized;
            Vector2 jumpTargetPos = (Vector2)self.position + dir * Range;

            float damage = monsterConfig.monsterStatData.attackPower * DamageRatio;
            
            var shadow = self.Find("Shadow");
            if (shadow == null || !shadow.TryGetComponent(out SpriteRenderer shadowRenderer))
            {
                yield break;
            }
            bool isDone = false;

            controller.OnJumpFinished = () => isDone = true;
            
            controller.JumpInit(damage, target, Range, Duration, ImpactRadius, false, shadow, shadowRenderer);
            while (!isDone)
                yield return null;
        }
    }
}