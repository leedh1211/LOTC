using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public SaveData CurrentSave { get; private set; }

    string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    [SerializeField] private CustomizeDataTable CustomizeDataTable;
    [SerializeField] private IntegerVariableSO Gold;
    [SerializeField] private IntegerVariableSO SelectedStage;
    [SerializeField] private IntegerVariableSO ClearedStage;
    [SerializeField] private IntegerVariableSO CurrentCustomItemId;
    [SerializeField] private BitArrayVariableSO OwnedCustomizeItem;
    protected override void Awake()
    {
        base.Awake();
        CurrentSave = Load();

        OwnedCustomizeItem.RuntimeValue = new BitArray(CustomizeDataTable.GetCustomizeDataCount());
        var bits = DeserializeOwnedItems(CurrentSave.OwnedCustomizeItem);
        for (int i = 0; i < bits.Length && i < OwnedCustomizeItem.RuntimeValue.Length; i++)
        {
            OwnedCustomizeItem.RuntimeValue[i] = bits[i];
        }
        CurrentCustomItemId.RuntimeValue = CurrentSave.CurrentCustomizeItem;
        Gold.RuntimeValue = CurrentSave.Gold;
        SelectedStage.RuntimeValue = CurrentSave.SelectedStage;
        ClearedStage.RuntimeValue = CurrentSave.ClearedStage;
        
    }
    public void Save()
    {
        SaveData data = new SaveData(
            gold: Gold.RuntimeValue,
            currentCustomizeItem: CurrentCustomItemId.RuntimeValue,
            ownedCustomizeItem: SerializeOwnedItems(OwnedCustomizeItem.RuntimeValue),
            selectedStage: SelectedStage.RuntimeValue,
            clearedStage: ClearedStage.RuntimeValue
            );

        string json = JsonConvert.SerializeObject(data, Formatting.Indented); 

        File.WriteAllText(SavePath, json);
    }
    public SaveData Load()
    {
        if (!File.Exists(SavePath))
            return new SaveData();

        string json = File.ReadAllText(SavePath);
        return JsonConvert.DeserializeObject<SaveData>(json);
    }
    public string SerializeOwnedItems(BitArray bits)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < bits.Length; i++)
        {
            sb.Append(bits[i] ? '1' : '0');
        }
        return sb.ToString();
    }

    public BitArray DeserializeOwnedItems(string str)
    {
        BitArray bits = new BitArray(str.Length);
        for (int i = 0; i < str.Length; i++)
        {
            bits[i] = str[i] == '1';
        }
        return bits;
    }
}