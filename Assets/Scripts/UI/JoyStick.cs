using UnityEngine;

public class JoyStick : MonoBehaviour
{
    [SerializeField] private RectTransform lever;
    private RectTransform rectTransform;
    Vector2 inputPos;
    [SerializeField,Range(0,200f)] private float leverRange = 50f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(Vector2 position)
    {
        var test = position;
        var dirVec = (test - inputPos).normalized;
        float magnitude = (test - inputPos).magnitude;

        var inputVector = magnitude < leverRange ? dirVec * magnitude : dirVec * leverRange;
        InputHandler.Instance.OnMove(dirVec);
        lever.anchoredPosition = inputVector;
    }

    public void OnPointerDown(Vector2 position)
    {
        inputPos = position;
        lever.anchoredPosition = Vector2.zero;
        InputHandler.Instance.OnMove(Vector2.zero);
    }

    public void OnPointerUp()
    {
        lever.anchoredPosition = Vector2.zero;
        InputHandler.Instance.OnMove(Vector2.zero);
    }

}
