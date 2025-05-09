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

    private Coroutine buttonCoroutine;
    private Transform targetButtonTransform;


    public void OnButton(Transform target)
    {
        if (buttonCoroutine != null)
        {
            targetButtonTransform.localScale = Vector3.one;
            
            StopCoroutine(buttonCoroutine);
        }

        targetButtonTransform = target;
        
        targetButtonTransform.SetAsLastSibling();
        
        buttonCoroutine = StartCoroutine(ScaleCorouitne(target));
    }

    IEnumerator ScaleCorouitne(Transform target)
    {
        float time = 0;

        Vector3 localScale = target.localScale;

        while (time < duration)
        {
            time += Time.deltaTime;

            float curveRatio = time / duration;

            target.localScale = Vector3.Lerp(localScale, targetScale, onButtonCurve.Evaluate(curveRatio));

            yield return null;
        }

        target.localScale = targetScale;
    }
}
