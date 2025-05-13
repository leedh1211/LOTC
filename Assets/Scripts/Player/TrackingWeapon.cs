using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingWeapon : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Vector3 defaultLocalPos;
    [SerializeField] private Transform target;

    [SerializeField] float lenghtX;
    [SerializeField] float lenghtY; 

    void Update()
    {

        if (player.GetNearestEnemy() != null)
        {
            target = player.GetNearestEnemy().transform;
        }
        else
        {
            return;
        }


        if (target != null)
        {
            Vector2 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            float radian = angle * Mathf.Deg2Rad;

            float x = lenghtX * Mathf.Cos(radian);
            float y = lenghtY * Mathf.Sin(radian);

            transform.localPosition = defaultLocalPos + new Vector3(x, y);
        }
     
    }
}
