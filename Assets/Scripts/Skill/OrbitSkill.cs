using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OrbitSkill : MonoBehaviour
{
    [SerializeField]
    private List<Transform> orbitObjects = new List<Transform>();


    [SerializeField]
    private float radis;

    public Transform targetPos;

    [SerializeField]
    private float speed;

    private float angle;


    private void Update()
    {
        if (targetPos == null || orbitObjects.Count < 2)
        {
            return;
        }


        angle += speed * Time.deltaTime;

        for (int i = 0; i < orbitObjects.Count; i++)
        {

            float offset = i * 180f;
            float curAngle = angle + offset;
            float rad = curAngle * Mathf.Deg2Rad;

            float dx = targetPos.position.x + Mathf.Cos(rad) * radis;
            float dy = targetPos.position.y + Mathf.Sin(rad) * radis;




            orbitObjects[i].position = new Vector3(dx, dy, 0);


        }
    }

   


}
