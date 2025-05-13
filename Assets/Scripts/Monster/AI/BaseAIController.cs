using System.Collections;
using System.Collections.Generic;
using Monster.Skill;
using UnityEngine;

namespace Monster.AI
{
    public class BaseAIController : MonoBehaviour
    {
        private GameObject _player;
        private MonsterConfig _monsterConfig;
        private Rigidbody2D _monsterRigid;
        private MonsterSkillController _monsterSkillController;

        private GridManager _gridManager;
        private bool _isInitialized;
        private bool _isFollowingPath;
        
        private Collider2D _monsterCollider2D;
        

        private void Start()
        {
            _gridManager = FindObjectOfType<GridManager>();
            _isInitialized = true;
        }
        
        public void Init(MonsterConfig config, Rigidbody2D rigid, GameObject playerObj)
        {
            _monsterConfig = config;
            _monsterRigid = rigid;
            _player = playerObj;
            _monsterSkillController = GetComponent<MonsterSkillController>();
            _monsterSkillController.Init(_monsterConfig, _player.transform);
            if (!TryGetComponent(out _monsterCollider2D))
            {
                Debug.LogError("monsterCollider2D not found", this);
            }
        }

        private void Update()
        {
            _monsterSkillController.Tick();
        }

        private void FixedUpdate()
        {
            if (!_isInitialized || _monsterSkillController.IsUsingSkill() || _isFollowingPath)
                return;
            
            int size = GetMonsterTileSize(_monsterCollider2D);
            
            var escapeDir = FindNearestWalkableDirection(transform.position, size);
            if (escapeDir.HasValue)
            {
                Move(escapeDir.Value);
                return;
            }


            if (Vector2.Distance(_player.transform.position, transform.position) > _monsterConfig.monsterStatData.attackRange)
            {
                Vector2 dir = (_player.transform.position - transform.position).normalized;
                float range = _monsterConfig.monsterStatData.attackRange;
                int mask = 1 << LayerMask.NameToLayer("Obstacle");

                if (_monsterCollider2D is BoxCollider2D)
                {
                    BoxCollider2D boxCollider = (BoxCollider2D)_monsterCollider2D;
                    Vector2 boxSize = boxCollider.size * transform.localScale;
                    int mobSize = GetMonsterTileSize(boxCollider);
                    RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0f, dir, range, mask);

                    if (hit.collider != null && !_monsterConfig.isFly)
                    {
                        StartCoroutine(FollowPathCoroutine(mobSize));
                    }
                    else
                    {
                        Move(dir);
                    }
                }
            }
        }

        private void Move(Vector2 direction)
        {
            var next = (Vector2)transform.position + direction * (_monsterConfig.monsterStatData.moveSpeed * Time.fixedDeltaTime);
            _monsterRigid.MovePosition(next);
        }

        private IEnumerator FollowPathCoroutine(int size)
        {
            _isFollowingPath = true;

            Node start = _gridManager.GetNodeFromWorld(transform.position);
            Node goal = _gridManager.GetNodeFromWorld(_player.transform.position);
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

                    if (Vector2.Distance(transform.position, _player.transform.position) <= attackRange)
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

        private int GetMonsterTileSize(Collider2D targetCollider)
        {
            float tileSize = 1f;
            Vector2 size = targetCollider.bounds.size;
            int tileWidth = Mathf.CeilToInt(size.x / tileSize);
            int tileHeight = Mathf.CeilToInt(size.y / tileSize);
            return Mathf.Max(tileWidth, tileHeight);
        }
        
        private Vector2? FindNearestWalkableDirection(Vector2 fromPosition, int size)
        {
            if (_gridManager == null)
            {
                Debug.LogError("GridManager is null!", this);
                return null;
            }
            Node fromNode = _gridManager.GetNodeFromWorld(fromPosition);
            if (fromNode == null)
            {
                Debug.LogWarning($"No node found for position {fromPosition}", this);
                return null;
            }
            
            if (fromNode != null && _gridManager.IsWalkableForSize(fromNode.pos, size))
            {
                return null;
            }

            Vector2Int origin = _gridManager.WorldToGrid(fromPosition);
            Vector2Int[] directions = new Vector2Int[]
            {
                new(0, 1), new(1, 0), new(0, -1), new(-1, 0),
                new(1, 1), new(1, -1), new(-1, -1), new(-1, 1)
            };

            float minDistance = float.MaxValue;
            Vector2Int bestTarget = origin;

            foreach (var dir in directions)
            {
                Vector2Int check = origin + dir;
                if (_gridManager.IsWalkableForSize(check, size))
                {
                    float dist = (check - origin).sqrMagnitude;
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        bestTarget = check;
                    }
                }
            }

            if (minDistance < float.MaxValue)
            {
                Vector2 bestWorld = _gridManager.GridToWorld(bestTarget);
                return (bestWorld - fromPosition).normalized;
            }

            return null;
        }
    }
}