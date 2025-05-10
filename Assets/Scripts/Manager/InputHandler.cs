using UnityEngine;

public class InputHandler : MonoSingleton<InputHandler>
{
    [SerializeField] private JoyStick joystick;
    public Vector2 MoveInput { get; private set; }
    private CustomizeController customizeController;
    private void Start()
    {
        customizeController = GameObject.Find("Player").GetComponent<CustomizeController>();
    }
    private void Update()
    {
        //전투 씬에서만 사용되도록, UI 클릭시에는 적용되지 않도록
        if(Input.GetMouseButtonDown(0))
        {
            joystick.gameObject.SetActive(true);
            Vector3 vec = Input.mousePosition;
            joystick.OnPointerDown(vec);
            joystick.transform.position = vec;
        }
        else if(Input.GetMouseButton(0))
        {
            Vector3 vec = Input.mousePosition;
            joystick.OnDrag(vec);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            joystick.OnPointerUp();
            joystick.gameObject.SetActive(false);
        }
        //테스트 용도
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            customizeController.SetCustomize("Angel");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            customizeController.SetCustomize("Ninja");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            customizeController.BuyCustomizeItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            customizeController.BuyCustomizeItem(2);
        }
    }
    public void OnMove(Vector2 vector2)
    {
        MoveInput = vector2;
    }
}
