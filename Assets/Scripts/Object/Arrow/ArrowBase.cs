using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowBase : MonoBehaviour
{
    private WaitForSeconds ws = new WaitForSeconds(0.3f);
    protected float power;
    protected float speed;
    protected float range;

    protected Vector3 moveDirection;
    protected Vector3 startPosition;

    public virtual void Init(float _power, float _speed, float _range , Vector3 _dir)
    {
        power = _power;
        speed = _speed;
        range = _range;

        moveDirection = _dir.normalized;
        startPosition = transform.position;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x)*Mathf.Rad2Deg-90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log($"[ArrowBase] Init�� �̵� ����: {moveDirection}");

        StartCoroutine(CheckArrowdistance());
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & (1 << 7)) != 0 && other.CompareTag("M_hitbox"))
        {
            MonsterController monsterControll = other.GetComponentInParent<MonsterController>();
            if (monsterControll != null)
            {
                monsterControll.TakeDamage(power);
                Destroy(gameObject);
            }
        }
    }


    protected virtual void Update()
    {
        // moveDirection �������� �̵�
        transform.position += moveDirection * speed * Time.deltaTime;


      
     
    }


    //�ڷ�ƾ���� ��Ÿ� äũ

    protected IEnumerator CheckArrowdistance()
    {
        while (true)
        {
            if ((startPosition - transform.position).sqrMagnitude >= range * range)
            {
                Destroy(gameObject);
                yield break;
            }
            yield return ws;
            //
        }

    }
}
   //��Ʈ �����̶� ������ 
   //sqrMagnitude�� �������� ������ �Ǳ� ������ ������ ���� ������. 


//���� �����ϴ°� ���
//�������� rigidbody2d�� �ܼ��� �����ϴ� �뵵�� ����ϴ°� ����
//�ݻ�� �ݻ簢������ �����ؼ� ����ϴ°� �������ϴ�. 

//pc���� 150fps������ �������� äũ�� �ؾ��Ѵ�. �� �ܴ� ũ�� �������� �ʾƵ��ȴ�. 
//�������� �����ϴٰ� ��ũġ�� �κ��� Ȯ�� �ؾ��Ѵ�. ���� ����� GC�߻��ϴ��� Ȯ���ϰ� 
//Ctrl +7 ��ư ���� Ȯ������. 
//���Ӻ� �ִ�� �ϰ� �ϱ� 
