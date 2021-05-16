using UnityEngine;
using UnityEngine.Events;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private StorageObject storageObject;

    [SerializeField]
    private UnityEvent inventoryUpdated;

    private Storage storage;
    public Storage Inventory => storage;

    private void Awake() {
        storage = new Storage(storageObject);
    }

    private void Start() {
        inventoryUpdated.Invoke();
        storage.StorageUpdated += () => inventoryUpdated.Invoke();
    }
    
    public void DropItemStack(int inventorySlot) {
        StorageItem storageItem = storage.RemoveItem(inventorySlot);
        if (storageItem != null) {
            Vector3 playerPosition = gameObject.transform.position;
            GameObject go = Instantiate(storageItem.Item.DroppedItemPrefab, playerPosition, Quaternion.identity);
            go.GetComponent<DroppedItemController>().Initialize(storageItem.Item, storageItem.Amount, UtilityMethods.GetRandomDirection(), true);
        }
    }

    public void DropItem(int inventorySlot, int amount) {
        StorageItem storageItem = storage.RemoveItem(inventorySlot, amount);
        if (storageItem != null) {
            Vector3 playerPosition = gameObject.transform.position;
            GameObject go = Instantiate(storageItem.Item.DroppedItemPrefab, playerPosition, Quaternion.identity);
            go.GetComponent<DroppedItemController>().Initialize(storageItem.Item, amount, UtilityMethods.GetRandomDirection(), true);
        }
    }
}
