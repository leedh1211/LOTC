using JetBrains.Annotations;
using Monster.ScriptableObject;
using UnityEngine;

namespace Monster.ScriptableObject
{
    [CreateAssetMenu(fileName = "MonsterProjectileData", menuName = "ScriptableObjects/MonsterProjectileData")]
    public class MonsterProjectileData : UnityEngine.ScriptableObject
    {
        public float size;
        [CanBeNull] public GameObject projectile;
        public float projectileSpeed;
        public float projectileDuration;
        public float projectileQuantity;
        public float projectileAngle;
    }
}