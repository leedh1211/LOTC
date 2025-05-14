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


    protected override void Update()
    {
        if (isDisable) return;
        
        // base.Update(); //�ι��� �������� �̻��Ѱſ���.. 
        float moveDistance = speed * Time.deltaTime;

        //�ڷ���Ʈ
        RaycastHit2D hit = Physics2D.Raycast(tipPos.position, transform.up, 0.2f, reflectLayer);
        if (hit.collider)
        {
            moveDirection = Vector3.Reflect(transform.up, hit.normal);//���⿡�� hitnormal.
                                                                      // transform.position = hit.point + hit.normal * 0.01f; ���⼭ �ڷ���Ʈ �ؼ� �浹�̻����. 

            curBonce++;
            //�ε��ƴ�.

            if (curBonce > maxBounce)
            {
                Destroy(gameObject);
                //���߿� Ǯ�� �Ҳ��� 
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

    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(tipPos.position, transform.up * 0.2f);
        if (moveDirection == Vector3.zero)
        {
            Debug.LogWarning("moveDirection�� zero�Դϴ�. Ray�� �׷����� �ʾƿ�.");
            return;
        }
    }

}
