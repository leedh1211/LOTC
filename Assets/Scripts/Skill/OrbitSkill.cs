using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OrbitSkill : MonoBehaviour
{

    [SerializeField]
    private float radis;

    public Transform targetPos;

    [SerializeField]
    private float speed;

    private float angle;


    private void Update()
    {
        if (targetPos == null)
        {
            return;
        }


        angle += speed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;

        //Vector3 orbit = new Vector3(Mathf.Cos(rad) * xRange, Mathf.Sin(rad) * yRange, 0f);
        float dx = targetPos.position.x + Mathf.Cos(rad) * radis;
        float dy = targetPos.position.y + Mathf.Sin(rad) * radis;
       // float dz = targetPos.position.z;



        transform.position = new Vector3(dx, dy, 0);
        //      this.transform.RotateAround(targetPos.position, Vector3.left, speed * Time.deltaTime);

    }




}
