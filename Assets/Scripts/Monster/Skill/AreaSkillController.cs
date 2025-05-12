using System.Collections;
using UnityEngine;

public class AreaSkillController : MonoBehaviour
{
    private float _damage;
    private float _range;
    private float _duration;
    private MonsterConfig _ownerConfig;
    private CircleCollider2D _trigger;

    public void Init(float damage, float range, float duration, MonsterConfig owner)
    {
        _damage      = damage;
        _range       = range;
        _duration    = duration;
        _ownerConfig = owner;

        // Collider 세팅
        _trigger = GetComponentInChildren<CircleCollider2D>();
        _trigger.isTrigger = true;
        _trigger.radius    = _range;

        // 시작하고 일정 시간 뒤에 제거
        StartCoroutine(Lifecycle());
    }

    private IEnumerator Lifecycle()
    {
        float elapsed = 0f;
        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                //플레이어 공격 함수 추가       
            }
                
        }
    }
}