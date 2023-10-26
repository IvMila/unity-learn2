using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> _inventorySlots;

    public List<InventorySlot> InventorySlots => _inventorySlots;

    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChange;

    public InventorySystem(int size)//конструктор который устанавливает кол-во слотов
    {
        _inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            _inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> inventorySlot))//проверить существует ли предмет в инвентаре 
        {
            foreach (var slot in inventorySlot)
            {
                if (slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChange?.Invoke(slot);
                    return true;
                }
            }
        }
        if (HasFreeSlot(out InventorySlot freeSlot))//получает первый доступный слот
        {
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))//достаточно ли места в этом слоте
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChange?.Invoke(freeSlot);
                return true;
            }
        }
        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> inventorySlot)//если ли в каких либо из наших слотов предмет для добавления? 
    {
        inventorySlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();//если они, получают список всех из них

        return inventorySlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);//получает первый доступный слот
        return freeSlot == null ? false : true;

    }
}
