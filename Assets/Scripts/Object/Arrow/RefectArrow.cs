using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class RefectArrow : ArrowBase
{
    [SerializeField] private int maxBounce = 3;
    [SerializeField] private LayerMask reflectLayer;

    [SerializeField]
    private Transform tipPos;

    private int curBonce = 0;


    // 이제는 사용되지 않습니다.
    /*protected override void Update()
    {
        if (isDisable) return;
        
        float moveDistance = speed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(tipPos.position, transform.up, 0.2f, reflectLayer);
        
        if (hit.collider)
        {
            moveDirection = Vector3.Reflect(transform.up, hit.normal);

            curBonce++;

            if (curBonce > maxBounce)
            {
                Destroy(gameObject);
            }

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);

        }
        else
        {
            transform.position += transform.up * moveDistance;
        }
    }
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }*/
}
