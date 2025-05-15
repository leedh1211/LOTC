using System.Collections;
using Monster.Skill;
using UnityEngine;

namespace Monster.ScriptableObject.Skill
{
    [CreateAssetMenu(fileName = "AreaSkillData", menuName = "MonsterSkill/AreaSkillData")]
    public class AreaSkillData : BaseMonsterSkillData
    {
        public GameObject affectPrefab;             
        public GameObject affectProjectilePrefab;   
        public float ProjectileSpeed;         
        public float DamageRatio;
        public float skillRange;
        public float AffectRange;
        public float Duration;
        public int AreaQuantity;
        public float AreaAngle;

        public override IEnumerator Excute(MonsterConfig monsterConfig, Transform self, Transform target)
        {
            Debug.Log("스킬동작");
            GameObject proj = Instantiate(
                affectProjectilePrefab,
                self.position,
                Quaternion.identity
            );

            var projCtrl = proj.GetComponent<AreaProjectileController>();
            if (projCtrl == null)
                projCtrl = proj.AddComponent<AreaProjectileController>();

            float damage = monsterConfig.monsterStatData.attackPower * DamageRatio;

            // 플레이어 방향 벡터 및 거리
            Vector2 direction = (target.position - self.position).normalized;
            float distanceToTarget = Vector2.Distance(self.position, target.position);

            // 목표 지점 계산
            Vector2 targetPos;
            float travelDistance;

            if (distanceToTarget <= skillRange)
            {
                travelDistance = distanceToTarget;
            }
            else
            {
                travelDistance = skillRange;
            }

            projCtrl.Init(
                direction,
                travelDistance,
                ProjectileSpeed,
                damage,
                AffectRange,
                Duration,
                affectPrefab,
                monsterConfig
            );
            yield return null;
        }
    }
}