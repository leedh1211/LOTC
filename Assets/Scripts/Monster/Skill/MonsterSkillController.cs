using System.Collections.Generic;
using Monster.ScriptableObject;
using Unity.VisualScripting;
using UnityEngine;

namespace Monster.Skill
{
    public class MonsterSkillController : MonoBehaviour
    {
        public void UseSkill(MonsterConfig MonsterConfig ,BaseMonsterSkillData monsterSkillData, Transform target)
        {
            monsterSkillData.Excute(MonsterConfig,transform, target);
        }
    }
}