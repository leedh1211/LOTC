using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
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
    public int projectileCount = 2; //ȭ�찹��
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float spreadAngle = 15f;

    [Header("MonsterList")]
    
    public List<Transform> monsterList;


    public Transform GetNearestEnemy()
    {
        float Nearest = Mathf.Infinity;
        float dis;
        Transform NearestMonster = null;
        

        for(int i = 0 ; i <monsterList.Count;i++)
        {
       
           // dis = Vector3.Distance(this.transform.position,pos.position); // Change CurDistance

            dis = (transform.position - monsterList[i].position).sqrMagnitude;

            if(dis<Nearest)
            {
                NearestMonster = monsterList[i];
                Nearest = dis;

            }


        }
        return NearestMonster;
    }





    public void Attack()
    {
        //�� Ž��
       
        Transform nearest = GetNearestEnemy();
        
        Vector3 targetPos = (nearest.position - firePoint.position).normalized;

        float baseAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;

     
        // ȭ�� ���� ���� ����
        float angleOffset = (projectileCount - 1) * spreadAngle * 0.5f;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = baseAngle - angleOffset + spreadAngle * i;
            //0, 0, baseAngle + angle
            Quaternion rot = Quaternion.Euler(0,0,angle);
            GameObject arrow = Instantiate(ArrowPrefabs, firePoint.position, rot); // ȭ�� ����

            Vector3 rotatedDir = rot * Vector3.right; // �׻� ���� ��� ��Ʈ��
            arrow.GetComponent<ArrowBase>()?.Init(power, speed, attackRange, rotatedDir);
        }

    }


    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            GetNearestEnemy();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ���� ���� ����
        Gizmos.DrawWireSphere(transform.position, attackRange); // ���� ���� �ð�ȭ
    }

}
