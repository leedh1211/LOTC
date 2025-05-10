using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimationCoroutine
{
    private Transform target;

    private MonoBehaviour runner;

    private Coroutine scaleCoroutine;
    

    public TransformAnimationCoroutine(MonoBehaviour runner, Transform target)
    {
        this.runner = runner;
        this.target = target;
    }

    public void PlayScale(AnimationCurve curve,  Vector3 startScale,  Vector3 targetScale, float duration)
    {
        if (scaleCoroutine != null)
        {
            runner.StopCoroutine(scaleCoroutine);
        }

        scaleCoroutine = runner.StartCoroutine(ScaleCorouitne(curve, startScale, targetScale, duration));
    }

    IEnumerator ScaleCorouitne(AnimationCurve curve, Vector3 startScale, Vector3 targetScale,  float duration)
    {
        float totalDistance = Vector3.Distance(startScale, targetScale);
        
        float currentDistance = Vector3.Distance(startScale, target.localScale);
        
        float timeRatio = (totalDistance > 0f) ? currentDistance / totalDistance : 0f;
        
        float time = timeRatio * duration;
        
        while (time <= duration)
        {
            time += Time.deltaTime;

            float cureveRatio = time / duration;

            target.localScale = Vector3.Lerp(startScale, targetScale, curve.Evaluate(cureveRatio));
            
            yield return null;
        }

        target.localScale = targetScale;
    }
}
