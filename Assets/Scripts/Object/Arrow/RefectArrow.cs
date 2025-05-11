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

    

    private void Start()
    {
        //tipPos = <Transform>();
    }


    protected override void Update()
    {
       // base.Update(); //두번씩 움직여서 이상한거였다.. 
        float moveDistance = speed * Time.deltaTime;
       
        //텔레포트
        RaycastHit2D hit= Physics2D.Raycast(tipPos.position, transform.up, 0.2f, reflectLayer);
        if (hit.collider)
        {
            moveDirection = Vector3.Reflect(transform.up, hit.normal);//방향에서 hitnormal.
           // transform.position = hit.point + hit.normal * 0.01f; 여기서 텔레포트 해서 충돌이생긴다. 
           
            curBonce++;
            //부딪쳤다.

            if (curBonce > maxBounce)
            {
                Destroy(gameObject);
                //나중에 풀링 할꺼야 
            }

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);

        }
        else
        {
            transform.position += transform.up * moveDistance; 
        }


         
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(tipPos.position, transform.up*0.2f);
         if (moveDirection == Vector3.zero)
    {
        Debug.LogWarning("moveDirection이 zero입니다. Ray가 그려지지 않아요.");
        return;
    }
    }

}
