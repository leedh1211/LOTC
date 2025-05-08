using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("AttackInfo")]
    [SerializeField]
    private float delay = 1f;
    public float Delay { get => delay;set=> delay = value; }
    [SerializeField]
    private float power = 1f;
    public float Power { get => power; set => power = value; }
    [SerializeField]
    private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }
    [SerializeField]
    private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask targetlayer; //공격 가능 대상


    [Header("ArrowInfo")]
    [SerializeField]
    private GameObject ArrowPrefabs;
    public int projectileCount = 2; //화살갯수
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float spreadAngle = 15f;


    public Transform GetNearestEnemy(Vector3 formpos, float range, LayerMask enemyLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(formpos, range, enemyLayer);

        float minDist = float.MaxValue;
        Transform nearest = null;

        foreach (var hit in hits)
        {
            float dist = Vector3.Distance(formpos, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }



    public void Attack()
    {
        //적 탐색
        float baseAngle = 90f;

        // 화살 각도 범위 설정
        float angleOffset = (projectileCount - 1) * spreadAngle * 0.5f;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = -angleOffset + spreadAngle * i;
            Quaternion rot = Quaternion.Euler(0, 0, baseAngle + angle);
            GameObject arrow = Instantiate(ArrowPrefabs, firePoint.position, rot); // 화살 생성

            Vector3 rotatedDir = rot * Vector3.right; // 항상 위로 쏘되 퍼트림
            arrow.GetComponent<ArrowBase>()?.Init(power, speed, attackRange, rotatedDir);
        }

    }


    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
         //여기 적용시킬것
            
            

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 원의 색상 지정
        Gizmos.DrawWireSphere(transform.position, attackRange); // 공격 범위 시각화
    }

}
