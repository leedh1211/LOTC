using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowBase : MonoBehaviour
{
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
        Debug.Log($"[ArrowBase] Init된 이동 방향: {moveDirection}");
    }


    protected virtual void Update()
    {
        // moveDirection 방향으로 이동
        transform.position += moveDirection * speed * Time.deltaTime;

        if (Vector3.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }

    }
}
