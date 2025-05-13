using System;
using System.Collections;
using System.Collections.Generic;
using Monster.ScriptableObject;
using Monster.Skill;
using UnityEngine;

namespace Monster.AI
{
    public class BaseAIController : MonoBehaviour
    {
        private GameObject player;
        private MonsterConfig _monsterConfig;
        private Rigidbody2D _monsterRigid;
        private MonsterSkillController _monsterSkillController;

        private GridManager _gridManager;
        private bool _isInitialized;
        private bool _isFollowingPath;

        private void Start()
        {
            _gridManager = FindObjectOfType<GridManager>();
            _isInitialized = true;
        }
        
        public void Init(MonsterConfig config, Rigidbody2D rigid, GameObject playerObj)
        {
            _monsterConfig = config;
            _monsterRigid = rigid;
            player = playerObj;
            _monsterSkillController = GetComponent<MonsterSkillController>();
            _monsterSkillController.Init(_monsterConfig, player.transform);
        }

        private void Update()
        {
            _monsterSkillController.Tick();
        }

        private void FixedUpdate()
        {
            if (!_isInitialized || _monsterSkillController.IsUsingSkill() || _isFollowingPath)
                return;

            if (Vector2.Distance(player.transform.position, transform.position) > _monsterConfig.monsterStatData.attackRange)
            {
                Vector2 dir = (player.transform.position - transform.position).normalized;
                float range = _monsterConfig.monsterStatData.attackRange;
                int mask = 1 << LayerMask.NameToLayer("Obstacle");

                BoxCollider2D collider = GetComponent<BoxCollider2D>();
                Vector2 boxSize = collider.size * transform.localScale;
                int mobSize = GetMonsterTileSize(collider);
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0f, dir, range, mask);

                if (hit.collider != null)
                {
                    StartCoroutine(FollowPathCoroutine(mobSize));
                }
                else
                {
                    Move(dir);
                }
            }
        }

        private void Move(Vector2 direction)
        {
            var next = (Vector2)transform.position + direction * _monsterConfig.monsterStatData.moveSpeed * Time.fixedDeltaTime;
            _monsterRigid.MovePosition(next);
        }

        private IEnumerator FollowPathCoroutine(int size)
        {
            _isFollowingPath = true;

            Node start = _gridManager.GetNodeFromWorld(transform.position);
            Node goal = _gridManager.GetNodeFromWorld(player.transform.position);
            List<Vector2Int> path = AStar.FindPath(start, goal, _gridManager.GetGrid(), pos => _gridManager.IsWalkableForSize(pos, size));
            
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
                    if (_monsterSkillController.IsUsingSkill())
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

        private int GetMonsterTileSize(Collider2D collider)
        {
            float tileSize = 1f;
            Vector2 size = collider.bounds.size;
            int tileWidth = Mathf.CeilToInt(size.x / tileSize);
            int tileHeight = Mathf.CeilToInt(size.y / tileSize);
            return Mathf.Max(tileWidth, tileHeight);
        }
    }
}