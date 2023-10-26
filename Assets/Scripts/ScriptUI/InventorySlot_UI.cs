using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private InventorySlot _assignedInventorySlot;

    private Button _button;

    public InventorySlot AssignedInventorySlot => _assignedInventorySlot;

    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();

        _button = GetComponent<Button>();
        _button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot inventorySlot)
    {
        _assignedInventorySlot = inventorySlot;
        UpdateUISlot(inventorySlot);
    }

    public void UpdateUISlot(InventorySlot inventorySlot)
    {
        if (inventorySlot.ItemData != null)
        {
            _itemSprite.sprite = inventorySlot.ItemData.Icon;
            _itemSprite.color = Color.white;

            if (inventorySlot.StackSize > 1) _itemCount.text = inventorySlot.StackSize.ToString();
            else _itemCount.text = "";
        }
        else ClearSlot();
    }

    public void UpdateUISlot()
    {
        if(_assignedInventorySlot.ItemData!=null) UpdateUISlot(_assignedInventorySlot);
    }

    public void ClearSlot()
    {
        _assignedInventorySlot?.ClearSlot();
        _itemSprite.sprite = null;
        _itemSprite.color = Color.clear;
        _itemCount.text = "";
    }
    public void OnUISlotClick()
    {
        ParentDisplay?.ClickedSlot(this);
    }
}
