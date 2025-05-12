using System.Collections;
using Monster.Skill;
using UnityEngine;

namespace Monster.ScriptableObject.Skill
{
    [CreateAssetMenu(fileName = "DashSkillData", menuName = "MonsterSkill/DashSkill")]
    public class DashSkillData : BaseMonsterSkillData
    {
        public float DamageRatio;
        public float Range;
        public float Delay;
        public float Speed;
        
        public override IEnumerator Excute(MonsterConfig monsterConfig , Transform self, Transform target)
        {
            if (!self.TryGetComponent(out DashSkillController controller))
            {
                controller = self.gameObject.AddComponent<DashSkillController>();
            }
            Vector2 direction = (target.position - self.position).normalized;
            float damage = monsterConfig.monsterStatData.attackPower * DamageRatio;
            controller.DashInit(direction, damage, monsterConfig.SpriteOverride, Range, Speed, Delay, monsterConfig.isFly);
            yield return null;
        }
    }
}