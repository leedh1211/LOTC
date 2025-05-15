using System.Collections.Generic;
using Data;

[System.Serializable]
public class SaveData
{
    public int Gold { get; set; }
    public int SelectedStage { get; set; }
    public int ClearedStage { get; set; }
    public int CurrentCustomizeItem { get; set; }
    public string OwnedCustomizeItem { get; set; }
    public PlayerpermanentStat PlayerStat { get; set; }
    public List<AchievementStatus> AchievementStatus { get; set; } = new List<AchievementStatus>();
    public SaveData()
    {
        Gold = 0;
        SelectedStage = 0;
        ClearedStage = 0;
        CurrentCustomizeItem = 0;
        OwnedCustomizeItem = "1";
        PlayerStat = new PlayerpermanentStat();
        AchievementStatus = new List<AchievementStatus>();
    }
    public SaveData(int gold,int currentCustomizeItem, string ownedCustomizeItem, int selectedStage, int clearedStage, PlayerpermanentStat stat, List<AchievementStatus> achievementStatus)
    {
        Gold = gold;
        CurrentCustomizeItem = currentCustomizeItem;
        OwnedCustomizeItem = ownedCustomizeItem;
        SelectedStage = selectedStage;
        ClearedStage = clearedStage;
        PlayerStat = stat;
        AchievementStatus = achievementStatus;
    }
}
    
