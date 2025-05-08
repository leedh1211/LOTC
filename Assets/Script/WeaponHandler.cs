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
    public int projectileCount = 1; //화살갯수
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
        Transform target = GetNearestEnemy(transform.position, attackRange, targetlayer);

        float baseAngle = firePoint.eulerAngles.z;
        if (target != null)
        {
            Vector3 dir = (target.position - firePoint.position).normalized; //각도를 계산해야하기 때문에 
            baseAngle = Mathf.Atan2(dir.y, dir.x)* Mathf.Rad2Deg;
            Debug.Log($"[WeaponHandler] 타겟 방향 벡터: {dir}, 회전 각도: {baseAngle}");
 
        }

        //화살각도
        float angleOffset = (projectileCount - 1) * spreadAngle * 0.5f;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = -angleOffset + spreadAngle * i; //퍼져야하니까
            Quaternion rot = Quaternion.Euler(0, 0, baseAngle + angle);
            GameObject arrow = Instantiate(ArrowPrefabs, firePoint.position, rot); //화살생성

            Vector3 rotatedDir = rot * Vector3.right;
            arrow.GetComponent<ArrowBase>()?.Init(power, speed, attackRange, rotatedDir); //없으면 등록 
        }
        
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 원의 색상 지정
        Gizmos.DrawWireSphere(transform.position, attackRange); // 공격 범위 시각화
    }

}
