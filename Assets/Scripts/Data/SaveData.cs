[System.Serializable]
public class SaveData
{
    public int Gold { get; set; }
    public int SelectedStage { get; set; }
    public int ClearedStage { get; set; }
    public int CurrentCustomizeItem { get; set; }
    public string OwnedCustomizeItem { get; set; }

    public SaveData()
    {
        Gold = 0;
        SelectedStage = 0;
        ClearedStage = 0;
        CurrentCustomizeItem = 0;
        OwnedCustomizeItem = "";
    }
    public SaveData(int gold,int currentCustomizeItem, string ownedCustomizeItem, int selectedStage, int clearedStage)
    {
        Gold = gold;
        CurrentCustomizeItem = currentCustomizeItem;
        OwnedCustomizeItem = ownedCustomizeItem;
        SelectedStage = selectedStage;
        ClearedStage = clearedStage;
    }
}
    
