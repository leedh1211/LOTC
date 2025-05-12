using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector2 moveDirection;
    private PlayerVisual playerVisual;
    
    [SerializeField] Vector2VariableSO joystickPos;//WeaponAttack


    public void Init(PlayerVisual visual)
    {
        playerVisual = visual;
    }

    private void FixedUpdate()
    {
        bool isMoved = joystickPos.RuntimeValue != Vector2.zero;
        
        
        if (isMoved)
        {
            playerVisual.FlipSpriteRenderer(joystickPos.RuntimeValue.x < 0);
        
            //Todo - 추후 스탯에서 속도 받아오도록
            transform.Translate(joystickPos.RuntimeValue * 3f * Time.deltaTime);
        }
        
        playerVisual.SetAnimation(isMoved);
    }
}
