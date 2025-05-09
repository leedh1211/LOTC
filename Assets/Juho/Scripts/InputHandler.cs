using UnityEngine;
using UnityEngine.EventSystems;

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
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            customizeController.SetCustomizeByName("Angel");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            customizeController.SetCustomizeByName("Ninja");
        }
    }
    public void OnMove(Vector2 vector2)
    {
        MoveInput = vector2;
    }
}
