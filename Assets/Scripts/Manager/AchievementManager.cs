using System;
using System.Collections.Generic;
using Data;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class AchievementManager : Singleton<AchievementManager>
    {
        [SerializeField]
        private List<AchievementSO> achievements;
        [SerializeField] private GameObject ToastHUDPrefab;
        private Dictionary<int, AchievementStatus> statusDict = new();
        
        private AchievementUI achievementUI;
        
        // public void Start()
        // {
        //     if (achievementUI == null)
        //     {
        //         GameObject HUD = Instantiate(ToastHUDPrefab);
        //         achievementUI = HUD.GetComponentInChildren<AchievementUI>();
        //     }
        // }

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
                CompleteAchievements(so,status);
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
                CompleteAchievements(so,status);
            }

        }


        public void CompleteAchievements(AchievementSO achievement, AchievementStatus status)
        {
            Debug.Log("[업적 완수]"+achievement.title+"\n"+achievement.description);
            if (achievementUI == null)
            {
                GameObject HUD = Instantiate(ToastHUDPrefab);
                achievementUI = HUD.GetComponentInChildren<AchievementUI>();    
            }
            achievementUI.ShowAchievement(achievement, status);
        }
        
        public List<AchievementStatus> GetStatusesForSave()
        {
            return new List<AchievementStatus>(statusDict.Values);
        }
    }
}