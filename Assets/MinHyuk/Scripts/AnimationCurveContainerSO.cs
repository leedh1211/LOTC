using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationCurveContainer", menuName = "Scriptable Objects/Animation Curve Container")]
public class AnimationCurveContainerSO : ScriptableObject
{
    [SerializeField] private AnimationCurve lerpCurve;

    public AnimationCurve LerpCurve => lerpCurve;
}
