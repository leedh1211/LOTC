using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection;
    private InputHandler inputHandler;
    private PlayerVisual playerVisual;
    private bool isInitialized = false;
    public bool isMoveing = false; //WeaponAttack

    public void Init(PlayerVisual visual)
    {
        inputHandler = InputHandler.Instance;
        playerVisual = visual;
        isInitialized = true;
    }

    public void FixedUpdate()
    {
        if (!isInitialized)
        {
            throw new System.Exception("PlayerController가 초기화되지 않았습니다.");
        }
        moveDirection = inputHandler.MoveInput;
        if (moveDirection == Vector2.zero)
        {
            playerVisual.SetMoving(false);
            isMoveing = false;
            return;
        }
       

        //Todo - 추후 스탯에서 속도 받아오도록
        transform.Translate(moveDirection * 3f * Time.deltaTime);
        playerVisual.SetDirection(moveDirection);
        playerVisual.SetMoving(true);
        isMoveing = true;
    }
}
