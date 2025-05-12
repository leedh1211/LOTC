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

        public override void Excute(MonsterConfig monsterConfig, Transform self, Transform target)
        {
            if (!self.TryGetComponent(out JumpSkillController controller))
            {
                controller = self.gameObject.AddComponent<JumpSkillController>();
            }

            Vector2 dir = (target.position - self.position).normalized;
            Vector2 jumpTargetPos = (Vector2)self.position + dir * Range;

            float damage = monsterConfig.monsterStatData.attackPower * DamageRatio;
            
            var shadow = self.Find("Shadow"); // 이름이 "Shadow"인 자식 오브젝트라고 가정
            
            if (shadow == null || !shadow.TryGetComponent(out SpriteRenderer shadowRenderer))
            {
                return;
            }

            controller.JumpInit(damage, target, Range, Duration, ImpactRadius, false, shadow, shadowRenderer);
        }
    }
}