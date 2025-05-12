using System;
using UnityEngine;


public abstract class GenericVariableSO<T> : ScriptableObject,ISerializationCallbackReceiver
{
    [SerializeField] T InitialValue;

    [NonSerialized]
    public T RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize() { } 
}
