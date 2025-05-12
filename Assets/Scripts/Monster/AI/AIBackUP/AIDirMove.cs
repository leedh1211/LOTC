using System.Collections;
using System.Collections.Generic;
using Monster.ScriptableObject;
using Monster.Skill;
using UnityEngine;

namespace Monster.AI_justDirMove
{
    public class AIDirMove : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float lookAheadDistance = 0.5f;
        [SerializeField, Range(0f,1f)] private float slideStrength = 0.8f;

        private MonsterConfig _monsterConfig;
        private Rigidbody2D   _monsterRigid;
        private bool          _isInitialized;
        private bool          _isUsingSkill;
        private MonsterSkillController _monsterSkillController;
        private List<float>   skillCooldownTimerList = new();

        private void Awake()
        {
            _isInitialized = false;
            _isUsingSkill  = false;
        }

        private void Start()
        {
            _monsterSkillController = GetComponent<MonsterSkillController>();
        }

        private void Update()
        {
            // 스킬 쿨다운 관리 (기존 로직)
            for (int i = 0; i < skillCooldownTimerList.Count; i++)
            {
                if (skillCooldownTimerList[i] <= 0f)
                {
                    _isUsingSkill = true;
                    _monsterSkillController.UseSkill(
                        _monsterConfig,
                        _monsterConfig.skillData[i],
                        player.transform);

                    skillCooldownTimerList[i] = _monsterConfig.skillData[i].Cooldown;
                    StartCoroutine(EndSkillAfter(_monsterConfig.skillData[i].AfterDelay));
                }
                else
                {
                    skillCooldownTimerList[i] -= Time.deltaTime;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_isInitialized || _isUsingSkill) return;

            Vector2 pos   = transform.position;
            Vector2 toPlayer = (player.transform.position - (Vector3)pos).normalized;
            
            int obstacleMask = 1 << LayerMask.NameToLayer("Obstacle");
            RaycastHit2D hit = Physics2D.Raycast(pos, toPlayer, lookAheadDistance, obstacleMask);

            Vector2 finalDir = toPlayer;
            if (hit.collider != null)
            {
                Vector2 normal  = hit.normal;
                Vector2 tangent = new Vector2(-normal.y, normal.x);

                finalDir = Vector2.Lerp(toPlayer, tangent, slideStrength).normalized;
            }

            Move(finalDir);
        }

        private void Move(Vector2 direction)
        {
            float speed = _monsterConfig.monsterStatData.moveSpeed;
            Vector2 next = (Vector2)transform.position + direction * speed * Time.fixedDeltaTime;
            _monsterRigid.MovePosition(next);
        }

        private IEnumerator EndSkillAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isUsingSkill = false;
        }

        public void Init(MonsterConfig config, Rigidbody2D rigid, GameObject playerObj)
        {
            _monsterConfig = config;
            _monsterRigid  = rigid;
            player         = playerObj;

            skillCooldownTimerList.Clear();
            foreach (var s in _monsterConfig.skillData)
                skillCooldownTimerList.Add(s.Cooldown);

            _isInitialized = true;
        }
    }
}
