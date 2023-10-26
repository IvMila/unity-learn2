using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class ItemPickUp : MonoBehaviour
{
    private BoxCollider _myCollider;
    public InventoryItemData InventoryItemData;

    private void Awake()
    {
        _myCollider = GetComponent<BoxCollider>();
        _myCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(InventoryItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
