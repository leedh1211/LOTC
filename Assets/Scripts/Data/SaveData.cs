[System.Serializable]
public class SaveData
{
    public int Gold { get; set; }
    public string OwnedCustomizeItem { get; set; }

    public SaveData()
    {
        Gold = 0;
        OwnedCustomizeItem = "";
    }
    public SaveData(int gold, string ownedCustomizeItem)
    {
        Gold = gold;
        OwnedCustomizeItem = ownedCustomizeItem;
    }
}
    
