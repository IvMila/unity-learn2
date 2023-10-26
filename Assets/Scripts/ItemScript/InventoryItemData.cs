using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID;
    public string DisplayName;
    [TextArea(4, 4)] public string Discription;
    public Sprite Icon;
    public int MaxStackSize;
}
