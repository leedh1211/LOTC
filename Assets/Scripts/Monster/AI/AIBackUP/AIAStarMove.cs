using System.Collections;
using System.Collections.Generic;
using Monster.ScriptableObject;
using Monster.Skill;
using UnityEngine;

namespace Monster.AI
{
    public class AIAStarMove : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        private MonsterConfig _monsterConfig;
        private Rigidbody2D _monsterRigid;
        private MonsterSkillController _monsterSkillController;
        private List<float> _skillCooldowns = new();

        private GridManager _gridManager;
        private bool _isInitialized;
        private bool _isUsingSkill;
        private bool _isFollowingPath;

        // 시각 디버그용
        private List<Vector2Int> _debugPath;

        private void Awake()
        {
            _isInitialized = false;
            _isUsingSkill = false;
            _isFollowingPath = false;
        }

        private void Start()
        {
            _gridManager = FindObjectOfType<GridManager>();
            _monsterSkillController = GetComponent<MonsterSkillController>();
            _isInitialized = true;
        }

        private void Update()
        {
            // 스킬 쿨다운 관리
            for (int i = 0; i < _skillCooldowns.Count; i++)
            {
                if (_skillCooldowns[i] <= 0f)
                {
                    _isUsingSkill = true;
                    _monsterSkillController.UseSkill(_monsterConfig, _monsterConfig.skillData[i], player.transform);
                    _skillCooldowns[i] = _monsterConfig.skillData[i].Cooldown;
                    StartCoroutine(EndSkillAfter(_monsterConfig.skillData[i].AfterDelay));
                }
                else
                {
                    _skillCooldowns[i] -= Time.deltaTime;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_isInitialized || _isUsingSkill || _isFollowingPath)
                return;

            if (Vector2.Distance(player.transform.position, this.transform.position) >
                _monsterConfig.monsterStatData.attackRange)
            {
                Vector2 dir = (player.transform.position - transform.position).normalized;
                float range = _monsterConfig.monsterStatData.attackRange;
                int mask = 1 << LayerMask.NameToLayer("Obstacle");

                // 장애물 검사
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, range, mask);
                if (hit.collider != null)
                {
                    StartCoroutine(FollowPathCoroutine());
                }
                else
                {
                    // 장애물 없으면 기본 이동
                    Move(dir);
                }
            }
        }

        private void Move(Vector2 direction)
        {
            var next = (Vector2)transform.position + direction * _monsterConfig.monsterStatData.moveSpeed * Time.fixedDeltaTime;
            _monsterRigid.MovePosition(next);
        }

        private IEnumerator EndSkillAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isUsingSkill = false;
        }

        private IEnumerator FollowPathCoroutine()
        {
            _isFollowingPath = true;

            // 1) A* 경로 계산
            Node start = _gridManager.GetNodeFromWorld(transform.position);
            Node goal  = _gridManager.GetNodeFromWorld(player.transform.position);
            var path = AStar.FindPath(start, goal, _gridManager.GetGrid());

            // 디버그용 시각화
            _debugPath = path;

            if (path == null || path.Count < 2)
            {
                _isFollowingPath = false;
                yield break;
            }
            
            float attackRange = _monsterConfig.monsterStatData.attackRange;
            for (int i = 1; i < path.Count; i++)
            {
                Vector2 waypoint = _gridManager.GridToWorld(path[i]);
                while (Vector2.Distance(transform.position, waypoint) > 0.1f)
                {
                    if (_isUsingSkill)
                    {
                        _isFollowingPath = false;
                        yield break;
                    }
                    
                    if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
                    {
                        _isFollowingPath = false;
                        yield break;
                    }
                    
                    Vector2 dir = (waypoint - (Vector2)transform.position).normalized;
                    Move(dir);

                    yield return new WaitForFixedUpdate();
                }
            }
            _isFollowingPath = false;
        }

        public void Init(MonsterConfig config, Rigidbody2D rigid, GameObject playerObj)
        {
            _monsterConfig = config;
            _monsterRigid = rigid;
            player = playerObj;
            _skillCooldowns.Clear();
            foreach (var s in _monsterConfig.skillData)
                _skillCooldowns.Add(s.Cooldown);
        }

        #region Debug Gizmos

        private void OnDrawGizmos()
        {
            if (_debugPath == null || _gridManager == null) return;
            Gizmos.color = Color.cyan;
            foreach (var g in _debugPath)
            {
                Vector2 w = _gridManager.GridToWorld(g);
                Gizmos.DrawWireCube(w, Vector3.one * _gridManager.cellSize * 0.9f);
            }
        }

        #endregion
    }
}