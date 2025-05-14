using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingWeapon : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private Vector3 defaultLocalPos;
    [SerializeField] private AnimationCurve pullAnimation;

    [Space(10f)] 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite pullSprite;
    
    [Space(10f)]
    [SerializeField] private Vector2VariableSO joystickPos;

    [SerializeField] float lenghtX;
    [SerializeField] float lenghtY;
    
    [SerializeField] float pullDist;

    private bool isPulling;
    
    private Transform target;

    private Vector3 dirToTarget;

    private Sprite currentWeaponSprite;
    
    void Update()
    {
        if (player.GetNearestEnemy() != null)
        {
            target = player.GetNearestEnemy().transform;
        }

        bool tempPulling = isPulling;

        currentWeaponSprite = baseSprite;

        if (joystickPos.RuntimeValue == Vector2.zero)
        {
            if (target != null)
            {
                dirToTarget = (target.position - player.transform.position).normalized;
                
                player.PlayerVisual.FlipSpriteRenderer(dirToTarget.x < 0);

                RotaionWeapon(dirToTarget);
            
                float pullRatio = weaponHandler.PullRatio;
                
             
                if (pullRatio > 0.15f)
                {
                    isPulling = true;
                    
                    PullAnimation(pullRatio);
                }
                else
                {
                    isPulling = false;
                }
            }
        }
        else
        {
            isPulling = false;
            
            RotaionWeapon(joystickPos.RuntimeValue);
        }

        if (tempPulling != isPulling)
        {
            player.Animator.SetBool("isPull", isPulling);
        }
        
        spriteRenderer.sprite = currentWeaponSprite;
    }

  
    void RotaionWeapon(Vector2 targetDir)
    {
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        float radian = angle * Mathf.Deg2Rad;

        float x = lenghtX * Mathf.Cos(radian);
        float y = lenghtY * Mathf.Sin(radian);

        transform.localPosition = defaultLocalPos + new Vector3(x, y);
    }
    

    void PullAnimation(float pullRatio)
    {
        float pullingDist = pullAnimation.Evaluate(pullRatio) * pullDist;

        Vector3 dirToPlayer = dirToTarget * -1;
        
        transform.localPosition += pullingDist * dirToPlayer;
        
        currentWeaponSprite = pullSprite;
        
    }
}
