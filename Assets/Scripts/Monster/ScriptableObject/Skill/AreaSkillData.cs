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

        public override void Excute(MonsterConfig monsterConfig, Transform self, Transform target)
        {
            // 1) 투사체 생성
            GameObject proj = Instantiate(
                affectProjectilePrefab,
                self.position,
                Quaternion.identity
            );

            // 2) 컨트롤러 붙이고 초기화
            var projCtrl = proj.GetComponent<AreaProjectileController>();
            if (projCtrl == null)
                projCtrl = proj.AddComponent<AreaProjectileController>();

            // 대미지 계산
            float damage = monsterConfig.monsterStatData.attackPower * DamageRatio;
            // 발사 방향 (스킬 시전 시점 플레이어 위치 기준)
            Vector2 dir = (target.position - self.position).normalized;

            projCtrl.Init(
                direction: dir,
                travelDistance: skillRange,
                speed: ProjectileSpeed,
                damage: damage,
                areaRange: AffectRange,
                areaDuration: Duration,
                areaPrefab: affectPrefab,
                ownerConfig: monsterConfig
            );
        }
    }
}