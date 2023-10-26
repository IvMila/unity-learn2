using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder _inventoryHolder;
    [SerializeField] private InventorySlot_UI[] _inventorySlot_UI;

    protected override void Start()
    {
        base.Start();

        if (_inventoryHolder != null)
        {
            _inventorySystem = _inventoryHolder.InventorySystem;
            _inventorySystem.OnInventorySlotChange += UpdateSlot;
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");

        AssingSlot(_inventorySystem);
    }

    public override void AssingSlot(InventorySystem inventoryToDisplay)
    {
        _slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (_inventorySlot_UI.Length != _inventorySystem.InventorySize) Debug.Log($"Inventory slots out of sync on {this.gameObject}");

        for (int i = 0; i < _inventorySystem.InventorySize; i++)
        {
            _slotDictionary.Add(_inventorySlot_UI[i], _inventorySystem.InventorySlots[i]);
            _inventorySlot_UI[i].Init(_inventorySystem.InventorySlots[i]);
        }
    }
}
