using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class HealthPickUp : MonoBehaviour
{
    private PlayerBehavior _playerBehavior;

    private int _addHealth = 2;

    public InventoryItemData InventoryItemData;
   
    private void Start()
    {
        _playerBehavior = PlayerBehavior.PlayerBehaviorInstance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var inventory = collision.gameObject.GetComponent<InventoryHolder>();
        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(InventoryItemData, 1))
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void AddHealth()
    {
        _playerBehavior.PlayerHealth += _addHealth;
    }
}
