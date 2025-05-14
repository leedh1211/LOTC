using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitFeedbackPannel : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] private Image image;
    
    [SerializeField] private AnimationCurve easeInCurve;

    private ProgressTweener tweener;
    
    private void Awake()
    {
        tweener = new(this);
    }

    private void OnEnable()
    {
        image.color = Color.white;
        
        tweener.Play(
                (ratio) => image.color = new Color( image.color.r,  image.color.g,  image.color.b,  1* (1 - ratio)), 
                0.33f,
                () => this.enabled = false)
            .SetCurve(easeInCurve);
    }
}
