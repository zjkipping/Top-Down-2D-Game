using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private StorageContainerController targetStorageContainerController = null;

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
            targetStorageContainerController.Open();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == EntityTags.DroppedItem) {
            if (Vector2.Distance(transform.position, other.transform.position) < 0.5f) {
                ItemId itemId = other.GetComponent<DroppedItemController>().CollectItem();
                Debug.Log("Collected Item: " + itemId);
            }
        }
    }
}
