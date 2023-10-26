using System.Collections.Generic;
using UnityEngine;
public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private MouseItemData _mouseInventoryItem;

    protected InventorySystem _inventorySystem;
    public InventorySystem InventorySystem => _inventorySystem;

    protected Dictionary<InventorySlot_UI, InventorySlot> _slotDictionary;//соединяет слоты пользов.интерфейса с системными слотами в инвентаре
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => _slotDictionary;

    public abstract void AssingSlot(InventorySystem inventoryToDisplay);
    [SerializeField] private HealthPickUp _healthPickUp;
    [SerializeField] private AnimationOpeningBox _animationOpeningBox;
    [SerializeField] private SimpleShoot _playerShoot;
    private UIBehavior _uIBehavior;
    protected virtual void Start()
    {
        _uIBehavior = UIBehavior.InstanceUIBehavior;
    }

    protected virtual void UpdateSlot(InventorySlot updateSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updateSlot) //значение слота - слот инвентаря "под капотом"
            {
                slot.Key.UpdateUISlot(updateSlot);//слот ключа - представление интерфейса значения
            }
        }

        if (updateSlot.ItemData.ID == 2 && updateSlot.StackSize >= 4)
        {
            _uIBehavior.ScreenWin();
        }
    }

    public void ClickedSlot(InventorySlot_UI clickedUISlot)
    {
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool isXPressed = Input.GetKey(KeyCode.X);
        //если ли у слота, по которому мы щелкнули даные предмета

        if (clickedUISlot.AssignedInventorySlot.ItemData != null &&
        _mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                _mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            if (clickedUISlot.AssignedInventorySlot.ItemData.ID == 1 && isXPressed)
            {
                _healthPickUp.AddHealth();
                clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
                clickedUISlot.UpdateUISlot();

                if (clickedUISlot.AssignedInventorySlot.StackSize <= 0) clickedUISlot.ClearSlot();

                return;
            }
            if (clickedUISlot.AssignedInventorySlot.ItemData.ID == 0 && isXPressed)
            {
                _animationOpeningBox.OpeningBox();
                //_uIBehavior.DestroyBoxLetter_Key();
                clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
                clickedUISlot.UpdateUISlot();

                if (clickedUISlot.AssignedInventorySlot.StackSize <= 0) clickedUISlot.ClearSlot();
                return;
            }
            if (clickedUISlot.AssignedInventorySlot.ItemData.ID == 3 && isXPressed)
            {
                _playerShoot.AddBullet();
                clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
                clickedUISlot.UpdateUISlot();

                if (clickedUISlot.AssignedInventorySlot.StackSize <= 0) clickedUISlot.ClearSlot();
                return;
            }
            else
            {
                _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
        }

        //в щелкнутом слоте нет предмета и у мыши есть предмет - поместите предмет мыши в пустой слот
        if (clickedUISlot.AssignedInventorySlot.ItemData == null &&
            _mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            _mouseInventoryItem.ClearSlot();
        }
        //есть ли предмет в обоих слотах
        if (clickedUISlot.AssignedInventorySlot.ItemData != null &&
            _mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == _mouseInventoryItem.AssignedInventorySlot.ItemData;
            //является ли этот предмет одним и тем же и достаточно ли места в стеке
            if (isSameItem && clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(_mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();
                _mouseInventoryItem.ClearSlot();
                return;
            }
            else if (isSameItem &&
                !clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStacks(_mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
            {
                if (leftInStack < 1) SwapSlots(clickedUISlot);//стек заполнен, поэтому поменяйте местами элементы
                else //слот не максимальный, так что берите то, что нужно из инвентаря мыши
                {
                    int remainingOnMouse = _mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(_mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
                    _mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }
            else if (!isSameItem)//если это не тот самый предмет 
            {
                SwapSlots(clickedUISlot);
                return;
            }
        }
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        //клонируем слот инвентаря
        var clonedSlot = new InventorySlot(_mouseInventoryItem.AssignedInventorySlot.ItemData,
            _mouseInventoryItem.AssignedInventorySlot.StackSize);
        _mouseInventoryItem.ClearSlot();

        _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();

        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}
