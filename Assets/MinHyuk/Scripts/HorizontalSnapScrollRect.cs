using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalSnapScrollRect : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private HorizontalLayoutGroup layoutGroup;

    protected int currentSnapIndex;

    private RectTransform[] childItems;

    protected virtual void Awake()
    {
        float horizontalPadding = scrollRect.viewport.rect.size.x/2;

        layoutGroup.padding.left = (int)horizontalPadding;
        layoutGroup.padding.right = (int)horizontalPadding;

        
        var content = scrollRect.content;
        
        childItems = new RectTransform[content.childCount];
        
        for (int i = 0; i < content.childCount; i++)
        {
            childItems[i] = content.GetChild(i).GetComponent<RectTransform>();
        }
    }

    protected virtual void Start()
    {
        StartCoroutine(StartSnap());
    }
    
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (scrollRect.content.childCount > 0)
        {
            float tempDis = float.MaxValue;

            for (int i = 0; i < childItems.Length; i++)
            {
                float distance = Mathf.Abs(childItems[i].anchoredPosition.x + scrollRect.content.anchoredPosition.x);

                if (distance < tempDis)
                {
                    tempDis = distance;

                    SetSnapIndex(i);
                }
            }
            SetContentPos(currentSnapIndex);
        }
    }


    protected void SetSnapIndex(int index)
        => currentSnapIndex = index;
    

    protected virtual void SetContentPos(int index) => 
        scrollRect.content.anchoredPosition = new Vector2(-childItems[index].anchoredPosition.x, scrollRect.content.anchoredPosition.y);

    
    IEnumerator StartSnap()
    {
        if (scrollRect.content.childCount > 0)
        {
            if (layoutGroup.padding.horizontal != 0)
            {
                float previousPosX = childItems[0].anchoredPosition.x;

                yield return new WaitUntil(() => previousPosX != childItems[0].anchoredPosition.x + layoutGroup.padding.horizontal);
            }
            
            SetContentPos(currentSnapIndex);
        }
    }
}
