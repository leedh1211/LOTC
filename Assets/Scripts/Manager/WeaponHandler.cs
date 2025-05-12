using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("AttackInfo")]
    [SerializeField]
    [Range(0.3f,2.0f)]private float delay = 2.0f;
    public float Delay { get => delay;set=> delay = value; }

    private float curTime = 0.0f;
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

    [Header("Player")]
    [SerializeField]
    private Player player;
    

    /// <summary>
    /// /////////////////
    /// </summary>

    [SerializeField] private Vector2VariableSO joystickPos;
    public void Attack()
    {
        //�� Ž��
       
        MonsterController nearest = player.GetNearestEnemy();

        if (nearest == null)
        {
            Debug.Log("타겟이 없습니다");
            return;
        }
        
        Vector3 targetPos = (nearest.transform.position - firePoint.position).normalized;

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


    private void DelayAttack()
    {
        
        if(curTime >=delay)
        {
            Attack();
            curTime = 0.0f;
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

        if (joystickPos.RuntimeValue == Vector2.zero)
        { 
            DelayAttack();
        }

        //delay Test
        if(Input.GetKeyDown(KeyCode.M))
        {
            delay -=0.1f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); 
    }

}
