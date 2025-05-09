using System;
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

        public void Awake()
        {
            _isInitialized = false;
        }
        public virtual void Start()
        {
            _monsterSkillController = GetComponent<MonsterSkillController>();
        }
        
        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
            if (!_isInitialized)
            {
                return;
            }
            if (Vector2.Distance(player.transform.position, this.transform.position) <= _monsterConfig.monsterStatData.attackRange) // 스킬쿨이 다 돌았고, 사거리안에 들어와있으면 스킬 시전
            {
                for (int i = 0; i < skillCooldownTimerList.Count; i++)
                {
                    if (skillCooldownTimerList[i] <= 0)
                    {
                        _monsterSkillController.UseSkill(i, player.transform);
                        skillCooldownTimerList[i] = _monsterConfig.skillData[i].Cooldown;
                    }
                    else
                    {
                        skillCooldownTimerList[i] -= Time.fixedDeltaTime;
                    }
                }
            }
            else
            {
                Move();
            }
            
        }

        public virtual void Move()
        {
            Vector2 direction = player.transform.position - this.transform.position;
            Vector2 nextPosition = (direction * _monsterConfig.monsterStatData.moveSpeed*Time.fixedDeltaTime) + (Vector2)this.transform.position;
            _monsterRigid.MovePosition(nextPosition);
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