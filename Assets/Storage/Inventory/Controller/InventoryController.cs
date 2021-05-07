using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private StorageObject storageObject;

    [SerializeField]
    private HotbarController hotbar;

    private StorageContainerController targetStorageContainerController = null;
    private Storage inventory;

    private int hotbarRow = 0;
    private int selectedHotbarSlot = 0;

    private void Awake() {
        inventory = new Storage(storageObject);
        InventoryUpdated();
    }

    public bool CanFitInInventory(ItemObject item, int amount) {
        return inventory.CanItemFit(item, amount);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == EntityTags.StorageContainer && targetStorageContainerController == null) {
            targetStorageContainerController = other.GetComponent<StorageContainerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (targetStorageContainerController != null && other.tag == EntityTags.StorageContainer) {
            StorageContainerController otherStorageContainerController = other.GetComponent<StorageContainerController>();
            if (otherStorageContainerController.GetId() == targetStorageContainerController.GetId()) {
                targetStorageContainerController = null;
            }
        }
    }

    private void OnInteract() {
        if (targetStorageContainerController != null) {
            targetStorageContainerController.Open(inventory);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == EntityTags.DroppedItem) {
            if (Vector2.Distance(transform.position, other.transform.position) < 0.5f) {
                AttemptItemCollection(other.GetComponent<DroppedItemController>());
            }
        }
    }

    private void AttemptItemCollection(DroppedItemController droppedItem) {
        int updatedItemAmount = inventory.AddItem(droppedItem.GetItem(), droppedItem.GetAmount());
        InventoryUpdated();
        if (updatedItemAmount > 0) {
            droppedItem.UpdateAmount(updatedItemAmount);
            droppedItem.RemoveCurrentTarget();
        } else {
            droppedItem.DestroyObject();
        }
    }

    private void InventoryUpdated() {
        var hotbarItems = new StorageItem[8];
        inventory.Items.CopyTo(8 * hotbarRow, hotbarItems, 0, 8);

        hotbar.updateItems(hotbarItems);
    }
}
