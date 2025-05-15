using System.Collections;
using UnityEngine;

public class AreaProjectileController : MonoBehaviour
{
    private float _damage;
    private float _areaRange;
    private float _areaDuration;
    private GameObject _areaPrefab;
    private MonsterConfig _ownerConfig;

    private Vector2 _startPos;
    private Vector2 _targetPos;
    private float _duration;

    private float _arcHeight = 1.5f;

    public void Init(Vector2 direction, float travelDistance, float speed, float damage, float areaRange,
        float areaDuration, GameObject areaPrefab, MonsterConfig ownerConfig)
    {
        _startPos = transform.position;
        _targetPos = _startPos + direction.normalized * travelDistance;
        _duration = travelDistance / speed;

        _damage = damage;
        _areaRange = areaRange;
        _areaDuration = areaDuration;
        _areaPrefab = areaPrefab;
        _ownerConfig = ownerConfig;
    }

    private void Start()
    {
        AreaSkillController area = SpawnArea();
        StartCoroutine(ParabolicMoveCoroutine(area));
    }

    private IEnumerator ParabolicMoveCoroutine(AreaSkillController area)
    {
        float elapsed = 0f;

        while (elapsed < _duration)
        {
            float t = elapsed / _duration;
            float heightFactor = Mathf.Sin(t * Mathf.PI);

            Vector2 flatPos = Vector2.Lerp(_startPos, _targetPos, t);
            transform.position = flatPos + Vector2.up * heightFactor * _arcHeight;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = _targetPos;
        Destroy(gameObject);
        DeSpawnArea(area);
    }

    private AreaSkillController SpawnArea()
    {
        GameObject area = Instantiate(_areaPrefab, _targetPos, Quaternion.identity);
        var controller = area.GetComponent<AreaSkillController>();
        if (controller == null)
            controller = area.AddComponent<AreaSkillController>();

        controller.Init(
            damage: _damage,
            range: _areaRange,
            duration: _areaDuration,
            owner: _ownerConfig
        );

        return controller;
    }

    private void DeSpawnArea(AreaSkillController areaController)
    {
        areaController.DoImpact();
        Destroy(areaController.gameObject, 0.1f);
    }
}
