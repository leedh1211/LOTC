using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "AchievementSO", menuName = "Scriptable Objects/AchievementSO/AchievementSO")]
    public class AchievementSO : ScriptableObject
    {
        public int seq;
        public string title;
        public string description;
        public Sprite icon;
        public int[] levels; 
    }
}