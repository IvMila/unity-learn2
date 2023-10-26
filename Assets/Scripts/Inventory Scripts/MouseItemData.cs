using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }
    private void Update()
    {
        if(AssignedInventorySlot.ItemData != null)
        {
            transform.position = Input.mousePosition;
            if(Input.GetMouseButton(1))
            {
                ClearSlot();
            }
        }
    }

    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    public void UpdateMouseSlot(InventorySlot inventorySlot)
    {
        AssignedInventorySlot.AssignItem(inventorySlot);
        ItemSprite.sprite = inventorySlot.ItemData.Icon;
        ItemCount.text = inventorySlot.StackSize.ToString();
        ItemSprite.color = Color.white;
    }
}
