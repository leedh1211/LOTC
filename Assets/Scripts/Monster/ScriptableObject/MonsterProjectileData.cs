using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Monster.ScriptableObject;
using UnityEngine;

namespace Monster.ScriptableObject
{
    [CreateAssetMenu(fileName = "MonsterProjectileData", menuName = "Scriptable Objects/MonsterProjectileData")]
    public class MonsterProjectileData : UnityEngine.ScriptableObject
    {
        public float size;
        [CanBeNull] public GameObject projectile;
        public float projectileSpeed;
        public float projectileDuration;
    }
}