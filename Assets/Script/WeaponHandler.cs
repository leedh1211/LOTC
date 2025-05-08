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

    public LayerMask targetlayer; //���� ���� ���


    [Header("ArrowInfo")]
    [SerializeField]
    private GameObject ArrowPrefabs;
    public int projectileCount = 1; //ȭ�찹��
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
        //�� Ž��
        Transform target = GetNearestEnemy(transform.position, attackRange, targetlayer);

        float baseAngle = firePoint.eulerAngles.z;
        if (target != null)
        {
            Vector3 dir = (target.position - firePoint.position).normalized; //������ ����ؾ��ϱ� ������ 
            baseAngle = Mathf.Atan2(dir.y, dir.x)* Mathf.Rad2Deg;
            Debug.Log($"[WeaponHandler] Ÿ�� ���� ����: {dir}, ȸ�� ����: {baseAngle}");
 
        }

        //ȭ�찢��
        float angleOffset = (projectileCount - 1) * spreadAngle * 0.5f;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = -angleOffset + spreadAngle * i; //�������ϴϱ�
            Quaternion rot = Quaternion.Euler(0, 0, baseAngle + angle);
            GameObject arrow = Instantiate(ArrowPrefabs, firePoint.position, rot); //ȭ�����

            Vector3 rotatedDir = rot * Vector3.right;
            arrow.GetComponent<ArrowBase>()?.Init(power, speed, attackRange, rotatedDir); //������ ��� 
        }
        
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ���� ���� ����
        Gizmos.DrawWireSphere(transform.position, attackRange); // ���� ���� �ð�ȭ
    }

}
