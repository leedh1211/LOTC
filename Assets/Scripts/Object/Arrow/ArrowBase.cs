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
        Debug.Log($"[ArrowBase] Init된 이동 방향: {moveDirection}");

        StartCoroutine(CheckArrowdistance());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & (1 << 7)) != 0)
        {
            MonsterController monsterControll = other.GetComponent<MonsterController>();
            if (monsterControll != null)
            {
                //monsterControll.TakeDamage(power);
                Destroy(gameObject);
            }
        }
    }


    protected virtual void Update()
    {
        // moveDirection 방향으로 이동
        transform.position += moveDirection * speed * Time.deltaTime;


      
     
    }


    //코루틴으로 사거리 채크

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
   //루트 연산이라서 느리다 
   //sqrMagnitude는 제곱값이 리턴이 되기 때문에 연산이 좀더 빠르다. 


//각도 변경하는거 고민
//물리엔진 rigidbody2d를 단순히 접촉하는 용도로 사용하는게 좋고
//반사는 반사각공식을 구현해서 사용하는게 좋을듯하다. 

//pc에서 150fps밑으로 떨어지면 채크를 해야한다. 그 외는 크게 생각하지 않아도된다. 
//프레임이 일정하다가 피크치는 부분을 확인 해야한다. 많이 생기면 GC발생하는지 확인하고 
//Ctrl +7 버튼 자주 확인하자. 
//게임뷰 최대로 하고 하기 
