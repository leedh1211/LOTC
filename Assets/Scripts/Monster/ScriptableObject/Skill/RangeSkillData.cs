using System.Collections;
using Monster.Skill;
using UnityEngine;

namespace Monster.ScriptableObject.Skill
{
    [CreateAssetMenu(fileName = "RangeSkillData", menuName = "MonsterSkill/rangeSkillData")]
    public class rangeSkillData : BaseMonsterSkillData
    {
        public GameObject projectilePrefab;
        public float DamageRatio;
        public float projectileSpeed;
        public float projectileQuantity;
        public float projectileAngle;
        public float projectileDelay;
        public bool isRandom;
        
        public override IEnumerator Excute(MonsterConfig monsterConfig , Transform self, Transform target)
        {
            Vector2 baseDirection = (target.position - self.position).normalized;
            float damage = monsterConfig.monsterStatData.attackPower * DamageRatio;

            for (int i = 0; i < projectileQuantity; i++)
            {
                Vector2 dir;
                if (isRandom)
                { // Random한 방향 사출
                    float randAngle = Random.Range(-projectileAngle / 2f, projectileAngle / 2f);
                    dir = Quaternion.Euler(0, 0, randAngle) * baseDirection;
                }
                else
                { // 일관된 방향 사출
                    float startAngle = -projectileAngle / 2f;
                    float step = projectileQuantity > 1 ? projectileAngle / (projectileQuantity - 1) : 0f;
                    float angle = startAngle + step * i;
                    dir = Quaternion.Euler(0, 0, angle) * baseDirection;
                }
                GameObject projectile = Instantiate(projectilePrefab, self.position, Quaternion.identity);
                ProjectileController controller = projectile.GetComponent<ProjectileController>();
                controller.Init(dir, projectileSpeed, damage);

                if (projectileDelay > 0f) // 발사체 딜레이
                    yield return new WaitForSeconds(projectileDelay);
            }

            if (projectileDelay <= 0f)
                yield return null;
        }
    }
}