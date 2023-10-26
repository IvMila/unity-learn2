using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData _itemData;//ссылка на данныые
    [SerializeField] private int _stackSize;//текущий размер стека - сколько данных у нас есть?

    public InventoryItemData ItemData => _itemData;
    public int StackSize => _stackSize;

    public InventorySlot(InventoryItemData source, int amount)//конструктор для создания занятого слота инвентаря
    {
        _itemData = source;
        _stackSize = amount;
    }

    public InventorySlot()//конструктор для создания пустого слота
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        _itemData = null;
        _stackSize = -1;
    }

    public void UpdateInventorySlot(InventoryItemData itemData, int amount)
    {
        _itemData = itemData;
        _stackSize = amount;
    }
    public bool EnoughRoomLeftInStacks(int amountToAdd, out int amountRemaining)//достаточно ли места в стеке для суммы которую мы пытаемся добавить 
    {
        amountRemaining = ItemData.MaxStackSize - _stackSize;
        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (_itemData == null || _itemData != null && _stackSize + amountToAdd <= _itemData.MaxStackSize) return true;//если текущий размер стека +2(которые мы пытаемся добавить)
        //меньше или равно максимальному размеру стека, то в стеке осталось достаточно места
        else return false;
    }
    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        _stackSize -= amount;
    }

    public void AssignItem(InventorySlot inventorySlot)//назначает предмет в слот
    {
        if (_itemData == inventorySlot.ItemData) AddToStack(inventorySlot._stackSize);//слот содержит тот же элемент? если да - добавляем его 
        else
        {
            _itemData = inventorySlot._itemData;
            _stackSize = 0;
            AddToStack(inventorySlot._stackSize);
        }
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (_stackSize <= 1)//достаточно ли чтобы фактически разделить стек
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(_stackSize / 2);//получим половину стека
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(_itemData, halfStack);//создает копию этого слота с половиной размера стека
        return true;
    }
}
