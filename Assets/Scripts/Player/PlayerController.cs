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
            throw new System.Exception("PlayerController�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }
        moveDirection = inputHandler.MoveInput;
        if (moveDirection == Vector2.zero)
        {
            playerVisual.SetMoving(false);
            isMoveing = false;
            return;
        }
       

        //Todo - ���� ���ȿ��� �ӵ� �޾ƿ�����
        transform.Translate(moveDirection * 3f * Time.deltaTime);
        playerVisual.SetDirection(moveDirection);
        playerVisual.SetMoving(true);
        isMoveing = true;
    }
}