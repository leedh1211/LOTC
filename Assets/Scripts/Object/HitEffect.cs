using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private AnimationCurve easeInCurve;

    private void Start()
    {
        spriteRenderer.color = Color.white;
        
        ProgressTweener tweener = new(this);
        
        tweener.Play(
                (ratio) => spriteRenderer.color = new Color( spriteRenderer.color.r,  spriteRenderer.color.g,  spriteRenderer.color.b,  1* (1 - ratio)), 
                0.33f,
                () => Destroy(gameObject))
            .SetCurve(easeInCurve);
    }
}
