using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    [SerializeField] private RectTransform area;

    [SerializeField] private RectTransform lever;
    
    [SerializeField] private Vector2VariableSO joystickPos;

    
    private float leverRange;
    
    private float leverRatio;

    private Vector2 leverVector2;

    private void Awake()
    {
        area.gameObject.SetActive(false);

        area.anchorMax = new Vector2(0.5f, 0.5f);
        area.anchorMin = new Vector2(0.5f, 0.5f);
        
        lever.anchorMax = new Vector2(0.5f, 0.5f);
        lever.anchorMin = new Vector2(0.5f, 0.5f);

        leverRange = area.sizeDelta.x / 2;
    }

    private void Update()
    {
        joystickPos.RuntimeValue = leverVector2;
    }

    public void OnPointerDownEvent(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        lever.anchoredPosition = Vector2.zero;
        
        area.position =  pointerData.position;

        area.gameObject.SetActive(true);
    }
    
    public void OnDragEvent(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
    
        lever.position = pointerData.position;
        
        Vector2 inputDir =  lever.anchoredPosition - Vector2.zero;

        float leverDist = lever.anchoredPosition.magnitude;

        leverDist = leverDist < leverRange ? leverDist : leverRange;
        
        lever.anchoredPosition =  inputDir.normalized * leverDist;


        leverRatio = leverDist / leverRange;
        
        leverVector2 = inputDir.normalized * leverRatio;
        
    }

    public void OnPointerUpEvent(BaseEventData data)
    {
        lever.anchoredPosition = Vector2.zero;
        
        area.gameObject.SetActive(false);
        
        leverVector2 = Vector2.zero;
    }
}
