using System;
using System.Collections.Generic;
using Data;
using UI;
using UnityEngine;

namespace Manager
{
    public class AchievementManager : Singleton<AchievementManager>
    {
        [SerializeField]
        private List<AchievementSO> achievements;
        private Dictionary<int, AchievementStatus> statusDict = new();
        
        [SerializeField] private AchievementUI achievementUI;
        
        public void Init(List<AchievementStatus> loadedStatuses)
        {
            foreach (var so in achievements)
            {
                var loaded = loadedStatuses.Find(s => s.seq == so.seq);
                if (loaded == null)
                {
                    statusDict[so.seq] = new AchievementStatus { seq = so.seq, currentLevel = 0, currentProgress = 0 };
                }
                else
                {
                    statusDict[so.seq] = loaded;
                }
            }
        }

        public void AddProgress(int seq, int amount)
        {
            if (!statusDict.ContainsKey(seq)) return;

            var status = statusDict[seq];
            var so = achievements.Find(a => a.seq == seq);
            if (so == null) return;
            
            status.currentProgress += amount;
            Debug.Log("[업적 관련 작업 실행] seq : "+seq+"amount : "+amount);
            
            if (status.currentLevel >= so.levels.Length) return;
            
            int required = so.levels[status.currentLevel];
            if (status.currentProgress >= required)
            {
                status.currentLevel++;
                CompleteAchievements(so);
            }
        }
        
        public void ChangeProgress(int seq, int amount)
        {
            if (!statusDict.ContainsKey(seq)) return;

            var status = statusDict[seq];
            var so = achievements.Find(a => a.seq == seq);
            if (so == null) return;
            
            status.currentProgress = amount;
            Debug.Log("[업적 관련 작업 실행] seq : "+seq+"amount : "+amount);
            
            if (status.currentLevel >= so.levels.Length) return;
            
            int required = so.levels[status.currentLevel];
            if (status.currentProgress >= required)
            {
                status.currentLevel++;
                CompleteAchievements(so);
            }

        }


        public void CompleteAchievements(AchievementSO achievement)
        {
            Debug.Log("[업적 완수]"+achievement.title+"\n"+achievement.description);
            achievementUI.ShowAchievement(achievement);
        }
        
        public List<AchievementStatus> GetStatusesForSave()
        {
            return new List<AchievementStatus>(statusDict.Values);
        }
    }
}