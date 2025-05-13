using System;
using Monster.ScriptableObject;
using UnityEngine;


namespace Monster
{
    [Serializable]
    public struct MonsterDropInfo
    {
        public GameObject monsterDropPrefab;
        public Vector2 MonsterDropPosition;
        public int MonsterDropQuantity;
    }
}