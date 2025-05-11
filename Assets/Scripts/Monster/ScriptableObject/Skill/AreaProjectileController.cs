using System.Collections;
using UnityEngine;

public class AreaProjectileController : MonoBehaviour
{
    private Vector2   _direction;
    private float     _travelDistance;
    private float     _speed;
    private float     _damage;
    private float     _areaRange;
    private float     _areaDuration;
    private GameObject _areaPrefab;
    private MonsterConfig _ownerConfig;
    private Vector2   _startPos;

    public void Init(
        Vector2   direction,
        float     travelDistance,
        float     speed,
        float     damage,
        float     areaRange,
        float     areaDuration,
        GameObject areaPrefab,
        MonsterConfig ownerConfig)
    {
        _direction      = direction;
        _travelDistance = travelDistance;
        _speed          = speed;
        _damage         = damage;
        _areaRange      = areaRange;
        _areaDuration   = areaDuration;
        _areaPrefab     = areaPrefab;
        _ownerConfig    = ownerConfig;

        _startPos = transform.position;
    }

    private void Update()
    {
        float step = _speed * Time.deltaTime;
        transform.Translate(_direction * step);
        
        if (Vector2.Distance(_startPos, transform.position) >= _travelDistance)
        {
            SpawnArea();
            Destroy(gameObject);
        }
    }

    private void SpawnArea()
    {
        GameObject area = Instantiate(
            _areaPrefab,
            transform.position,
            Quaternion.identity
        );

        var controller = area.GetComponent<AreaSkillController>();
        if (controller == null)
            controller = area.AddComponent<AreaSkillController>();

        controller.Init(
            damage:      _damage,
            range:       _areaRange,
            duration:    _areaDuration,
            owner:       _ownerConfig
        );
    }
}
