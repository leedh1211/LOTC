using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "monsterEvent", menuName = "Scriptable Objects/Events/monster Event")]
public class MonsterEventChannelSO : GenericEventChannelSO<MonsterController>
{
    public UnityAction<MonsterController> OnEventRaised;
    public void Raise(MonsterController monster)
    {
        OnEventRaised?.Invoke(monster);
    }
}
