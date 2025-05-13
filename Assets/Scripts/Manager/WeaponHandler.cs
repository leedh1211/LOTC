using System.Collections;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public float PullRatio
    {
        get => curTime / Delay;
    }

    public float Delay { get => delay - permanentStat.RuntimeValue.Delay * 0.1f; set => delay = value; }
    public float Power { get => power + permanentStat.RuntimeValue.Power * 10; set => power = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public bool IsSequnce { get => isSequnce; set => isSequnce = value; }
    public bool IsReflect { get => isReflect; set => isReflect = value; }
    

    public LayerMask targetlayer;

    [Header("AttackInfo")]
    [SerializeField][Range(0.3f, 2.0f)] private float delay = 2.0f; //clamp()로해야 제대로 된다. 
    [SerializeField] private float power = 1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float attackRange = 10f;

    private float curTime = 0.0f;


    [Header("ArrowInfo")]
    public int projectileCount = 2; //arrow count SpreadCountup

    [SerializeField] private GameObject ArrowPrefabs;
    [SerializeField] private GameObject ReflectPrfabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float spreadAngle = 15f;
    [SerializeField] private int shotCount = 1;
    [SerializeField] private bool isSequnce = false;
    [SerializeField] private bool isReflect = true;



    [Header("Player")]
    [SerializeField] private Player player;
    [SerializeField] private PlayerStatVariableSO permanentStat;
    [SerializeField] private Vector2VariableSO joystickPos;

    public void Attack()
    {
        MonsterController nearest = player.GetNearestEnemy();

        if (nearest == null)
        {
            Debug.Log("타겟이 없습니다");
            return;
        }

        Vector3 targetPos = (nearest.transform.position - firePoint.position).normalized;

        float baseAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;

        float angleOffset = (projectileCount - 1) * spreadAngle * 0.5f;
        
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = baseAngle - angleOffset + spreadAngle * i;

            Quaternion rot = Quaternion.Euler(0, 0, angle);

            Vector3 rotatedDir = rot * Vector3.right; // �׻� ���� ��� ��Ʈ��

            GameObject arrow;
            
            if (isReflect == true)
            {


                arrow = Instantiate(ReflectPrfabs, firePoint.position, rot); // ȭ�� ����

            }
            else
            {
                arrow = Instantiate(ArrowPrefabs, firePoint.position, rot); // ȭ�� ����

            }

            arrow.GetComponent<ArrowBase>()?.Init(Power, speed, attackRange, rotatedDir);

        }
    }




    private IEnumerator SequnceShot()
    {
        MonsterController nearest = player.GetNearestEnemy();

        if (nearest == null)
        {
            Debug.Log("타겟이 없습니다");
            yield return new WaitForSeconds(0.5f);
            yield break;
        }

        for (int i = 0; i < shotCount; i++)
        {

            for (int j = 0; j < projectileCount; j++)
            {
                Vector3 targetPos = (nearest.transform.position - firePoint.position).normalized;

                float baseAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;

                float angleOffset = (projectileCount - 1) * spreadAngle * 0.5f;
                float angle = baseAngle - angleOffset + spreadAngle * j;

                Quaternion rot = Quaternion.Euler(0, 0, angle);
                GameObject arrow = Instantiate(ArrowPrefabs, firePoint.position, rot);

                Vector3 rotatedDir = rot * Vector3.right;
                arrow.GetComponent<ArrowBase>()?.Init(Power, speed, attackRange, rotatedDir);
            }

            yield return new WaitForSeconds(0.4f); //시간을 채크할때 -> 평소 느끼는 대로 
        }
    }



    private void DelayAttack()
    {
        if (curTime >= Delay)
        {
            if (isSequnce == false)
            {
                Attack();
            }
            else
            {
                StartCoroutine(SequnceShot());
            }
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
        else
        {
            curTime = 0;
        }

        //delay Test
        if (Input.GetKeyDown(KeyCode.M))
        {
            Delay -= 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isSequnce = true;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
