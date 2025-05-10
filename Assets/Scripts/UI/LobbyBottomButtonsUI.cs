using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyBottomButtonsUI : MonoBehaviour
{
    [SerializeField] private AnimationCurve onButtonCurve;
    [SerializeField] private float duration;
    [SerializeField] private Vector3 targetScale;
    
    [Space(10f)]
    [SerializeField] private Button apperanceButton;
    [SerializeField] private Button statButton;

    private Transform targetButtonTransform;

    private TransformAnimationCoroutine testCorouitine;
    private TransformAnimationCoroutine testCorouitine2;

    private void Awake()
    {
        testCorouitine = new(this, statButton.transform);
        testCorouitine2 = new (this, apperanceButton.transform);
    }

    public void OnButton(Transform target)
    {
        if (target == statButton.transform)
        {
            testCorouitine.PlayScale(onButtonCurve, Vector2.one, new Vector2(1.2f, 1.2f), 0.33f);
            testCorouitine2.PlayScale(onButtonCurve, new Vector2(1.2f, 1.2f), Vector2.one, 0.33f);
        }
        else
        {
            testCorouitine2.PlayScale(onButtonCurve, Vector2.one, new Vector2(1.2f, 1.2f), 0.33f);
            testCorouitine.PlayScale(onButtonCurve, new Vector2(1.2f, 1.2f), Vector2.one, 0.33f);
        }
        

        targetButtonTransform = target;
        
        targetButtonTransform.SetAsLastSibling();
    }

}
