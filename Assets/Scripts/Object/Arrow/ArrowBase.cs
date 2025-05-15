using System.Collections;
using UnityEngine;

public class ArrowBase : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AnimationCurve disableCurve;
    
    private bool isDisable;

    private int reflectCount;

    private float power;
    private float speed;
    private float range;

    private Vector3 moveDirection;
    private Vector3 startPosition;


    public void Init(float _power, float _speed, float _range, int _reflectCount, Vector3 _dir)
    {
        power = _power;
        speed = _speed;
        range = _range;
        reflectCount = _reflectCount;

        moveDirection = _dir.normalized;
        startPosition = transform.position;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x)*Mathf.Rad2Deg-90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        StartCoroutine(CheckArrowdistance());
    }

    
    //조건 변경, 리플렉트처리 정확하게 해당 몬스터를 검사하도록 변경하였습니다.
    //현재 맵구조에 따라 임시로 몬스터 피격시에만 도탄으로 변경했습니다.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "Player")
        {
            if (other.TryGetComponent(out MonsterController monster))
            {
                monster.TakeDamage(power);
            
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                
                if (reflectCount > 0)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.2f);
                    
                    if (hit.collider != null)
                    {
                        moveDirection = Vector3.Reflect(transform.up, hit.normal);

                        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
                    
                        transform.rotation = Quaternion.Euler(0, 0, angle);
                    
                        reflectCount--;
                    }
                }
                else
                {
                    isDisable = true;
                
                    ProgressTweener disableTweener = new(this);

                    disableTweener.Play(
                        (ratio) => spriteRenderer.color = new Color(1, 1, 1, 1 - ratio),
                        0.33f,
                        () =>
                        {
                            Destroy(gameObject);
                        });
                }
            }
        }
      
        
        /*if (((1 << other.gameObject.layer) & (1 << 7)) != 0 && other.CompareTag("M_hitbox"))
        {
            MonsterController monsterControll = other.GetComponentInParent<MonsterController>();
            if (monsterControll != null)
            {
                monsterControll.TakeDamage(power);

                if (reflectCount > 0)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.2f);
                    
                    if (hit.collider)
                    {
                        moveDirection = Vector3.Reflect(transform.up, hit.normal);

                        reflectCount--;

                        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
                        transform.rotation = Quaternion.Euler(0, 0, angle);
                    }
                }
                else
                {
                    isDisable = true;

                    ProgressTweener disableTweener = new(this);

                    disableTweener.Play(
                        (ratio) => spriteRenderer.color = new Color(1, 1, 1, 1 - ratio),
                        0.33f,
                        () =>
                        {
                            Destroy(gameObject);
                        });
                
                    Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                }
            }
        }*/
    }


    private void Update()
    {
        if (!isDisable)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }
 

    private IEnumerator CheckArrowdistance()
    {
        while (true)
        {
            if ((startPosition - transform.position).sqrMagnitude >= range * range)
            {
                Destroy(gameObject);
                yield break;
            }
            
            yield return new WaitForSeconds(0.3f);
        }
    }
}
