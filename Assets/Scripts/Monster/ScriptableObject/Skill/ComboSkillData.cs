using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.ScriptableObject.Skill
{
    [CreateAssetMenu(menuName = "MonsterSkill/ComboSkill")]
    public class ComboSkillData : BaseMonsterSkillData
    {
        public List<BaseMonsterSkillData> chainedSkills;
        public float skillDelay;

        public override IEnumerator Excute(MonsterConfig config, Transform self, Transform target)
        {
            foreach (var skill in chainedSkills)
            {
                yield return skill.Excute(config, self, target);
                if (skillDelay != 0)
                {
                    yield return new WaitForSeconds(skillDelay);    
                }
            }
        }
    }
}