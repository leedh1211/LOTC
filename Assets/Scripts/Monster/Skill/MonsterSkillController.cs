using System.Collections.Generic;
using Monster.ScriptableObject;
using Unity.VisualScripting;
using UnityEngine;

namespace Monster.Skill
{
    public class MonsterSkillController : MonoBehaviour
    {
        private List<MonsterSkillData> skills;
        private float cooldownTimer;

        public void Init(List<MonsterSkillData> skillList)
        {
            skills = skillList;
        }

        public void UseSkill(int index, Transform target)
        {
            if (cooldownTimer > 0f) return;
            var skill = skills[index];

            switch (skill.Type)
            {
                case MonsterSkillType.projectile:
                    LaunchProjectile(skill, target);
                    break;
                case MonsterSkillType.rush:
                    // 예: AI 이동 강제 제어
                    break;
                case MonsterSkillType.area:
                    CreateGroundEffect(skill, target.position);
                    break;
            }

            cooldownTimer = skill.Cooldown;
        }

        private void LaunchProjectile(MonsterSkillData skill, Transform target)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            var bullet = Instantiate(skill.projectile, transform.position, Quaternion.identity);
        }

        private void CreateGroundEffect(MonsterSkillData skill, Vector2 position)
        {
            Instantiate(skill.GroundEffectPrefab, position, Quaternion.identity);
        }

        private void Update()
        {
            if (cooldownTimer > 0)
                cooldownTimer -= Time.deltaTime;
        } 
    }
}