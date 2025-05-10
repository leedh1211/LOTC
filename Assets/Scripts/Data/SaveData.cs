[System.Serializable]
public class SaveData
{
    private int gold;
    private string ownedCustomizeItem;

    public int Gold => gold;
    public string OwnedCustomizeItem => ownedCustomizeItem;
    public SaveData()
    {

    }
    public SaveData(int gold, string ownedCustomizeItem)
    {
        this.gold = gold;
        this.ownedCustomizeItem = ownedCustomizeItem;
    }
}
    
