using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(ScrollRect))]
public abstract class SnapScrollView : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    [System.Serializable]
    public struct SmoothOption
    {
        public bool enabled;
        public float moveSpeed;
    }
    
    [System.Serializable]
    public struct ScaleOption
    {
        public bool enabled;
        public Vector3 min;
        public Vector3 max;
    }
    
    [System.Serializable]
    public struct RotationOption
    {
        public bool enabled;
        public bool isMirror;
        public Vector3 min;
        public Vector3 max;
    }
    
    public event UnityAction<PointerEventData> OnBeginDragEvent;
    public event UnityAction<PointerEventData> OnEndDragEvent;
    
    
    
    public int snapIndex;
    
    [Space(10f)]
    [Header("Component")]
    [SerializeField] protected ScrollRect scrollRect;
    [SerializeField] protected List<RectTransform> itemList;

    [Space(15f)] 
    [Header("Option")]
    [SerializeField] private SmoothOption smoothOption;
    [SerializeField] private ScaleOption scaleOption;
    [SerializeField] private RotationOption rotationOption;
 

    private Coroutine lerpCoroutine;


    protected abstract Vector2 GetSnappedContentPos();
    protected abstract float GetMaxScrollDistance();
    protected abstract float GetItemDisToViewPort(Vector2 itemCenter, Vector2 viewPortCenter);

  
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
        }
        
        OnBeginDragEvent?.Invoke(eventData);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        SetSnapIndex();

        var targetPos = GetSnappedContentPos(); 
        
        lerpCoroutine = StartCoroutine(SnapScrollRect(targetPos));
        
        OnEndDragEvent?.Invoke(eventData);
    }
    
    
    public void DirectUpdateItemList(int index)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
            
        snapIndex = index;
        
        for (int i = 0; i < itemList.Count; i++)
        {
            if (scaleOption.enabled)
            {
                Vector3 scale = i == snapIndex ? scaleOption.max :  scaleOption.min;
                
                itemList[i].localScale = scale;
            }

            if (rotationOption.enabled)
            {
                float dir = !rotationOption.isMirror ? 1 : i < snapIndex ? -1 : 1;
                
                Vector3 rotation = i == snapIndex ? rotationOption.max :  rotationOption.min;

                itemList[i].localRotation = Quaternion.Euler(rotation * dir);
            }
        }
        
        scrollRect.content.anchoredPosition = GetSnappedContentPos();
    }


    void SetSnapIndex()
    {
        Vector2 viewportCenter = scrollRect.viewport.TransformPoint(scrollRect.viewport.rect.center);

        float maxDis = GetMaxScrollDistance()/ 2;

        float maxItemDisRatio = 0;

        int tempSnapIndex = snapIndex;
        
        for (int i = 0; i < itemList.Count; i++)
        {
            var item = itemList[i];
            
            Vector2 itemCenter = item.TransformPoint(item.rect.center);

            float itemDis = GetItemDisToViewPort(itemCenter, viewportCenter);
            
            float itemDisNormalizedRatio = Mathf.Clamp01(1f -  Mathf.Abs(itemDis) / maxDis);

            if (itemDisNormalizedRatio > maxItemDisRatio)
            {
                maxItemDisRatio = itemDisNormalizedRatio;
                
                tempSnapIndex = i;
            }
        }

        snapIndex = tempSnapIndex;
    }
    

    
    public void UpdateItemList()
    {
        //TransformPoint = 로컬좌표를 기준 글로벌좌표로 (마치 pivot 0.5, 0.5)
        
        Vector2 viewportCenter = scrollRect.viewport.TransformPoint(scrollRect.viewport.rect.center);

        float maxDis = GetMaxScrollDistance()/ 2;

        for (int i = 0; i < itemList.Count; i++)
        {
            var item = itemList[i];
            
            Vector2 itemCenter = item.TransformPoint(item.rect.center);

            float itemDis = GetItemDisToViewPort(itemCenter, viewportCenter);
            
            float itemDisNormalizedRatio = Mathf.Clamp01(1f -  Mathf.Abs(itemDis) / maxDis);

            if (Mathf.Abs(itemDis) < maxDis)
            {
                if (scaleOption.enabled)
                {
                    Vector3 scale = Vector3.Lerp(scaleOption.min, scaleOption.max, itemDisNormalizedRatio);
                
                    item.localScale = scale;
                }

                if (rotationOption.enabled)
                {
                    float dir = !rotationOption.isMirror ? 1 : itemDis > 0 ? 1 : -1;
                
                    Vector3 rotation = Vector3.Lerp(rotationOption.min, rotationOption.max, itemDisNormalizedRatio);
                
                    item.localRotation = Quaternion.Euler(rotation * dir);
                }
            }
        }
    }


    IEnumerator SnapScrollRect(Vector2 targetPos)
    {
        if (smoothOption.enabled)
        {
            while ((scrollRect.content.anchoredPosition - targetPos).sqrMagnitude > 0.01f)
            {
                scrollRect.content.anchoredPosition = Vector2.Lerp(scrollRect.content.anchoredPosition, targetPos, Time.deltaTime * smoothOption.moveSpeed);
                yield return null;
            }
        }
        
        scrollRect.content.anchoredPosition = targetPos;
        
        lerpCoroutine = null;
    }
}
