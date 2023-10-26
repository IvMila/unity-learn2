using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DinamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI _slotPrefab;
    protected override void Start()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += RefreshDinamicInventory;
        base.Start();
    }

    private void OnDestroy()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= RefreshDinamicInventory;
    }

    public void RefreshDinamicInventory(InventorySystem invToDisplay)
    {
        _inventorySystem = invToDisplay;
    }

    public override void AssingSlot(InventorySystem inventoryToDisplay)
    {
        ClearSlots();

        _slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (inventoryToDisplay == null) return;

        for(int i = 0; i<inventoryToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(_slotPrefab, transform);
            _slotDictionary.Add(uiSlot, inventoryToDisplay.InventorySlots[i]);
            uiSlot.Init(inventoryToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

    private void ClearSlots()
    {
        foreach(var item in transform.Cast<Transform>())//этот трансформ получит все дочерние элементы нашего преобразования
        {
            Destroy(item.gameObject);
        }

        if (_slotDictionary != null) _slotDictionary.Clear();
    }
}
