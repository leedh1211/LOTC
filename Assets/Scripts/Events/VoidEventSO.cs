using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "VoidEvent", menuName = "Scriptable Objects/Events/Void Event")]
public class VoidEventChannelSO : ScriptableObject
{
    public event UnityAction OnEventRaised;

    public void Raise() => OnEventRaised?.Invoke();
}