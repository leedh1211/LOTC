using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SnapScroll : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
 
    [System.Serializable]
    public struct LerpMoveSetting
    {
        public bool enabled;
        public float moveSpeed;
    }
    
    [System.Serializable]
    public struct Vector3Setting
    {
        public bool enabled;
        
        public Vector3 min;
        public Vector3 max;
    }
    
    [System.Serializable]
    public struct RotaionSetting
    {
        public bool isMirror;
        public Vector3Setting Vector3Setting;
    }
    
    public event UnityAction<PointerEventData> OnBeginDragEvent;
    public event UnityAction<PointerEventData> OnEndDragEvent;

    
    
    public int centerIndex;

    [Space(10f)] 
    [Header("Setting")]
    
    public LerpMoveSetting lerpMove;
    public Vector3Setting transformScale;
    public RotaionSetting transformRotaion;

    
    [Space(15f)]
    [Header("Component")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] protected LayoutGroup layoutGroup;

    

    private bool isUpdatedLayout;
    
    protected RectTransform viewPort;
    protected RectTransform content;
    protected RectTransform[] childItems;
    
    private Coroutine lerpCoroutine;
    
    

    
    
    private void Awake()
    {
        content = scrollRect.content;
        
        childItems = new RectTransform[content.childCount];
        
        for (int i = 0; i < content.childCount; i++)
        {
            childItems[i] = content.GetChild(i).GetComponent<RectTransform>();
        }
        
        
        viewPort = scrollRect.viewport;
    }

    private void Start()
    {
        StartCoroutine(UpdateLayout());
    }

    private void OnEnable()
    {
        StartCoroutine(StartSnap());
    }
    
    
    protected abstract void SetLayoutPadding();
    protected abstract Vector2 GetMoveContentPos();
    protected abstract float GetMaxDisToViewPort(Vector2 viewPortCenter);
    protected abstract float GetItemDisToViewPort(Vector2 itemCenter, Vector2 viewPortCenter);



    public void OnBeginDrag(PointerEventData eventData)
    {
        if (lerpMove.enabled)
        {
            if (lerpCoroutine != null)
            {
                StopCoroutine(lerpCoroutine);
            }
        }
        
        OnBeginDragEvent?.Invoke(eventData);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (lerpMove.enabled)
        {
            lerpCoroutine = StartCoroutine(LerpMoveContent());
        }
        else
        {
            content.anchoredPosition = GetMoveContentPos();
        }
        
        OnEndDragEvent?.Invoke(eventData);
    }

    

    
    public void UpdateChildScales()
    {
        //content, viewport 는 pivot = 0, 1 이므로
        //TransformPoint = pivot 0.5, 0.5 기준 로컬좌표를 글로벌좌표로
        
        Vector2 viewportCenter = viewPort.TransformPoint(viewPort.rect.center);

        float maxDis = GetMaxDisToViewPort(viewportCenter);

        float maxItemDisRatio = 0;

        int tempCenterIndex = centerIndex;
        
        for (int i = 0; i < childItems.Length; i++)
        {
            var item = childItems[i];
            
            Vector2 itemCenter = item.TransformPoint(item.rect.center);

            float itemDis = GetItemDisToViewPort(itemCenter, viewportCenter);
            
            float itemDisNormalizedRatio = Mathf.Clamp01(1f -  Mathf.Abs(itemDis) / maxDis);

            if (itemDisNormalizedRatio > maxItemDisRatio)
            {
                maxItemDisRatio = itemDisNormalizedRatio;
                
                tempCenterIndex = i;
            }

            if (Mathf.Abs(itemDis) < maxDis)
            {
                if (transformScale.enabled)
                {
                    Vector3 scale = Vector3.Lerp(transformScale.min, transformScale.max, itemDisNormalizedRatio);
                
                    item.localScale = scale;
                }

                if (transformRotaion.Vector3Setting.enabled)
                {
                    float dir = !transformRotaion.isMirror ? 1 : itemDis > 0 ? 1 : -1;
                
                    Vector3 rotation = Vector3.Lerp(transformRotaion.Vector3Setting.min, transformRotaion.Vector3Setting.max, itemDisNormalizedRatio);
                
                    item.localRotation = Quaternion.Euler(rotation * dir);
                }
            }
        }
            
        
        if (Mathf.Abs(content.anchoredPosition.x) < content.rect.width)
        {
            centerIndex = tempCenterIndex;
        }
    }
    



    IEnumerator LerpMoveContent()
    {
        var targetPos = GetMoveContentPos();
        
        while ((content.anchoredPosition - targetPos).sqrMagnitude > 0.01f)
        {
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPos, Time.deltaTime * lerpMove.moveSpeed);
            yield return null;
        }
        
        content.anchoredPosition = targetPos;
        
        lerpCoroutine = null;
    }

    IEnumerator UpdateLayout()
    {
        SetLayoutPadding();
        
        yield return new WaitUntil(() => content.hasChanged);

        isUpdatedLayout = true;
    }

    IEnumerator StartSnap()
    {
        yield return new WaitUntil(() => isUpdatedLayout);
    
        content.anchoredPosition = GetMoveContentPos();
        
    }
}
