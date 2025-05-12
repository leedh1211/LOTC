using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] private int goldAmount;

    [Space(10f)]
    [SerializeField] private float yoyoDist;
    [SerializeField] private float yoyoTime;
    
    [Space(5f)]
    [SerializeField] private float chaseTime;
    
    [Space(10f)]
    [SerializeField] private AnimationCurve easeInCurve;
    [SerializeField] private AnimationCurve easeOutCurve;
    
    [Space(10f)]
    [Header("Events")]
    [SerializeField] private TransformEventChannelSO rooting;
    [SerializeField] private IntegerEventChannelSO rootedGold;


    private ProgressTweener chaseTweener;
    
    private void Awake()
    {
        chaseTweener = new(this);
    }

    private void OnEnable()
    {
        rooting.OnEventRaised += ChasedTarget;
    }

    private void OnDisable()
    {
        rooting.OnEventRaised -= ChasedTarget;
    }

    void ChasedTarget(Transform target)
    {
        Vector3 yoyoStartPos = transform.position;
        
        Vector3 yoyoDir = (yoyoStartPos - target.position).normalized;

        Vector3 yoyoPos = yoyoStartPos + yoyoDir * yoyoDist;
        

        chaseTweener.Play(
            (ratio) => transform.position = Vector3.Lerp(yoyoStartPos, yoyoPos, ratio),
            yoyoTime,
            () =>
            {
                Vector3 yoyoEndPos = transform.position;
                
                chaseTweener.Play(
                    (ratio) => transform.position = Vector3.Lerp(yoyoEndPos, target.position, ratio),
                    chaseTime,
                    () =>
                    {
                        rootedGold.Raise(goldAmount);
                        
                        gameObject.SetActive(false);
                    }).SetCurve(easeInCurve);
                
            }).SetCurve(easeOutCurve);
    }
}
