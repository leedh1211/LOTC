using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimationCoroutine
{
    private Transform target;

    private float scaleRemainDuration;

    private Vector3 startScale;
    private Vector3 startPosition;
    private Vector3 startRotaion;

    private Coroutine scaleCoroutine;
    

    private MonoBehaviour runner;


    public TransformAnimationCoroutine(MonoBehaviour runner, Transform target)
    {
        this.runner = runner;
        this.target = target;
    }

    public void PlayScale(AnimationCurve curve, Vector3 targetScale, float duration)
    {
        if (scaleCoroutine != null)
        {
            runner.StopCoroutine(scaleCoroutine);

            duration -= scaleRemainDuration;
        }

        runner.StartCoroutine(ScaleCorouitne(curve, targetScale, duration));
    }

    IEnumerator ScaleCorouitne(AnimationCurve curve, Vector3 targetScale,  float duration)
    {
        float time = 0;

        startScale = target.localScale;

        while (time <= duration)
        {
            time += Time.deltaTime;

            float timeRatio = time / duration;

            scaleRemainDuration = duration - time;

            target.localScale = Vector3.Lerp(startScale, targetScale, curve.Evaluate(timeRatio));
            
            yield return null;
        }

        target.localScale = targetScale;
    }
}
