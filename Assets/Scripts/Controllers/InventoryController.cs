using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private StorageUtil.StorageType type;

    private StorageContainerController targetStorageContainerController = null;
    private Storage inventory;

    private void Start() {
        inventory = new Storage(type);
    }

    public bool CanFitInInventory(ItemUtil.ItemId id, int amount) {
        return inventory.CanItemFit(id, amount);
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
        droppedItem.CollectItem(out ItemUtil.ItemId itemId, out int itemAmount);
        var updatedItemAmount = inventory.AddItem(itemId, itemAmount);
        if (updatedItemAmount > 0) {
            droppedItem.UpdateAmount(updatedItemAmount);
            droppedItem.RemoveCurrentTarget();
        } else {
            droppedItem.DestroyObject();
        }
    }
}
