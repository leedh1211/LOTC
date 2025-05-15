using System.Collections;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public float PullRatio
    {
        get => curTime / Delay;
    }

    public float Delay { get => Mathf.Clamp(delay - permanentStat.RuntimeValue.Delay * 0.1f,0.3f,2.0f); set => delay = value; }
    public float Power { get => power + permanentStat.RuntimeValue.Power * 10; set => power = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public int ShotCount { get => shotCount; set => shotCount = value; }
    public int ReflectCount { get => reflectCount; set => reflectCount = value; }
    public int ProjectileCount  { get => projectileCount; set => projectileCount = value; }

    
    public LayerMask targetlayer;
    

    [Header("AttackInfo")]
    [SerializeField][Range(0.3f, 2.0f)] private float delay = 2.0f; //clamp()로해야 제대로 된다. 
    [SerializeField] private float power = 1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float attackRange = 10f;



    [Header("ArrowInfo")]

    [SerializeField] private int projectileCount = 2;
    [SerializeField] private ArrowBase ArrowPrefabs;
    [SerializeField] private float spreadAngle = 15f;
    [SerializeField] private int shotCount = 1;
    [SerializeField] private int reflectCount = 0;
    
    //[SerializeField] private bool isReflect = true; 오직 reflectCount 로 동작하도록 하였습니다.
    //[SerializeField] private bool isSequnce = false; 오직 shotCount 로 동작하도록 하였습니다.
    //[SerializeField] private GameObject ReflectPrfabs;

    //[SerializeField] private Transform firePoint; 중복되는 변수




    [Header("Player")]
    [SerializeField] private Player player;
    [SerializeField] private PlayerStatVariableSO permanentStat;
    [SerializeField] private Vector2VariableSO joystickPos;
    
    
    private bool isRunningAttack;
    private float curTime = 0.0f;


    //Attack 은 코루틴으로 통합되었습니다.
    
    /*public void Attack()
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

            Vector3 rotatedDir = rot * Vector3.right; 

            GameObject arrow;
            
            if (isReflect == true)
            {
                arrow = Instantiate(ReflectPrfabs, firePoint.position, rot);
            }
            else
            {
                arrow = Instantiate(ArrowPrefabs, firePoint.position, rot); 
            }

            arrow.GetComponent<ArrowBase>()?.Init(Power, speed, attackRange, rotatedDir);

        }
    }*/


    private IEnumerator Shot()
    {
        isRunningAttack = true;
        
        MonsterController nearest = player.GetNearestEnemy();

        if (nearest == null)
        {
            Debug.Log("타겟이 없습니다");
            yield break;
        }
        
        Vector3 targetPos = (nearest.transform.position - transform.position).normalized;

        float baseAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;

        float angleOffset = (projectileCount - 1) * spreadAngle * 0.5f;


        for (int i = 0; i < shotCount; i++)
        {
            yield return new WaitForSeconds(0.2f); //시간을 채크할때 -> 평소 느끼는 대로 

            for (int j = 0; j < projectileCount; j++)
            {
                float angle = baseAngle - angleOffset + spreadAngle * j;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                Vector3 rotatedDir = rot * Vector3.right; 

                ArrowBase arrow = Instantiate(ArrowPrefabs, transform.position, rot); 
            
                arrow.Init(Power, speed, attackRange, reflectCount, rotatedDir);

                //이제 베이스 애로우만 사용!
                /*if (isReflect == true)
                {
                    arrow = Instantiate(ReflectPrfabs, firePoint.position, rot);
                }
                else
                {
                    arrow
                }*/
            }
        }
        
        curTime = 0.0f;
    }


    private void DelayAttack()
    {
        if (curTime >= Delay)
        {
            if (!isRunningAttack)
            {
                StartCoroutine(Shot());
            }
        }
        else
        {
            isRunningAttack = false;

            curTime += Time.deltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Shot());
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
