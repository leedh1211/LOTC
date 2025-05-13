using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizePage : MonoBehaviour
{
    [SerializeField] private Transform itemsParent;
    [SerializeField] private CustomizePageSlot slotPrefab;
    [SerializeField] private CustomizeDataTable customizeDataTable;
    private List<CustomizePageSlot> customizePageSlots;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < customizeDataTable.GetCustomizeDataCount();i++)
        {
            var slot = Instantiate<CustomizePageSlot>(slotPrefab, itemsParent);
            if(customizeDataTable.TryGetCustomizeData(i + 1,out var data))
            {
                slot.Init(data);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
