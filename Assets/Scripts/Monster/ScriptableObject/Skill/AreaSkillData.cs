using Monster.Skill;
using UnityEngine;

namespace Monster.ScriptableObject.Skill
{
    [CreateAssetMenu(fileName = "AreaSkillData", menuName = "MonsterSkill/AreaSkillData")]
    public class AreaSkillData : BaseMonsterSkillData
    {
        public GameObject affectPrefab;
        public float DamageRatio;
        public float Range;
        public float projectileSpeed;
        public float projectileQuantity;
        public float projectileAngle;
        
        public override void Excute(MonsterConfig monsterConfig , Transform self, Transform target)
        {
            Vector2 BaseDirection = (target.position - self.position).normalized;
            float startAngle = (projectileAngle / 2) * -1f;
            float stepAngle = projectileAngle / projectileQuantity;
            Vector2 startDir = Quaternion.Euler(0, 0, startAngle) * BaseDirection;
            float Damage = monsterConfig.monsterStatData.attackPower * DamageRatio;
            for (int i = 0; i < projectileQuantity; i++)
            {
                Vector2 dir = Quaternion.Euler(0, 0, stepAngle * i) * startDir;
                GameObject projectile = Instantiate(affectPrefab, self.position, Quaternion.identity);
                ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
                projectileController.Init(dir, projectileSpeed, Damage);   
            }
        }
    }
}