using System;
using System.Collections;
using System.Collections.Generic;
using Monster.Skill;
using Unity.VisualScripting;
using UnityEngine;

namespace Monster.AI
{
    public class BaseAIController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private MonsterConfig _monsterConfig;
        private Rigidbody2D _monsterRigid;
        private List<float> skillCooldownTimerList = new List<float>();
        private MonsterSkillController _monsterSkillController;
        private bool _isInitialized;
        private bool isUsingSkill;

        public void Awake()
        {
            _isInitialized = false;
            isUsingSkill = false;
        }
        public virtual void Start()
        {
            _monsterSkillController = GetComponent<MonsterSkillController>();
        }
        
        public virtual void Update()
        {
            for (int i = 0; i < skillCooldownTimerList.Count; i++)
            {
                if (skillCooldownTimerList[i] <= 0)
                {
                    isUsingSkill = true; // 스킬 사용 중
                    _monsterSkillController.UseSkill(_monsterConfig, _monsterConfig.skillData[i], player.transform);

                    skillCooldownTimerList[i] = _monsterConfig.skillData[i].Cooldown;

                    StartCoroutine(EndSkillAfter(_monsterConfig.skillData[i].AfterDelay)); //후딜레이 처리
                }
                else
                {
                    skillCooldownTimerList[i] -= Time.deltaTime;
                }
            }
        }

        public virtual void FixedUpdate()
        {
            if (!_isInitialized || isUsingSkill)
                return;

            if (Vector2.Distance(player.transform.position, this.transform.position) > _monsterConfig.monsterStatData.attackRange)
            {
                Move();
            }
        }

        public virtual void Move()
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            Vector2 nextPosition = (direction * _monsterConfig.monsterStatData.moveSpeed * Time.fixedDeltaTime) + (Vector2)transform.position;
            _monsterRigid.MovePosition(nextPosition);
        }
        
        private IEnumerator EndSkillAfter(float duration)
        {
            yield return new WaitForSeconds(duration);
            isUsingSkill = false;
        }

        public virtual void ActiveSkill()
        {
            
        }

        public void Init(MonsterConfig config, Rigidbody2D rigid, GameObject player)
        {
            _monsterConfig = config;
            _monsterRigid = rigid;
            foreach (var monsterSkillData in _monsterConfig.skillData)
            {
                skillCooldownTimerList.Add(monsterSkillData.Cooldown);
            }
            this.player = player;
            _isInitialized = true;
        }
            
    }
}