[System.Serializable]
public class SaveData
{
    public int Gold { get; set; }
    public int SelectedStage { get; set; }
    public int ClearedStage { get; set; }
    public string OwnedCustomizeItem { get; set; }

    public SaveData()
    {
        Gold = 0;
        SelectedStage = 0;
        ClearedStage = 0;
        OwnedCustomizeItem = "";
    }
    public SaveData(int gold, string ownedCustomizeItem, int selectedStage, int clearedStage)
    {
        Gold = gold;
        OwnedCustomizeItem = ownedCustomizeItem;
        SelectedStage = selectedStage;
        ClearedStage = clearedStage;
    }
}
    
