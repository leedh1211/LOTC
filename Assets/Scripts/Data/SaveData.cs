[System.Serializable]
public class SaveData
{
    public int Gold { get; set; }
    public int SelectedStage { get; set; }
    public int ClearedStage { get; set; }
    public int CurrentCustomizeItem { get; set; }
    public string OwnedCustomizeItem { get; set; }
    public PlayerStat PlayerStat { get; set; }
    public SaveData()
    {
        Gold = 0;
        SelectedStage = 0;
        ClearedStage = 0;
        CurrentCustomizeItem = 0;
        OwnedCustomizeItem = "";
        PlayerStat = new PlayerStat();
    }
    public SaveData(int gold,int currentCustomizeItem, string ownedCustomizeItem, int selectedStage, int clearedStage, PlayerStat stat)
    {
        Gold = gold;
        CurrentCustomizeItem = currentCustomizeItem;
        OwnedCustomizeItem = ownedCustomizeItem;
        SelectedStage = selectedStage;
        ClearedStage = clearedStage;
        PlayerStat = stat;
    }
}
    
