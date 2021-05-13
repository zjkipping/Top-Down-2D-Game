using UnityEngine;

public class ItemCollectionController : MonoBehaviour {
    [SerializeField]
    private InventoryController inventoryController;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == EntityTags.DroppedItem) {
            if (Vector2.Distance(transform.position, other.transform.position) < 0.5f) {
                AttemptItemCollection(other.GetComponent<DroppedItemController>());
            }
        }
    }

    private void AttemptItemCollection(DroppedItemController droppedItem) {
        int updatedItemAmount = inventoryController.Inventory.AddItem(droppedItem.GetItem(), droppedItem.GetAmount());
        if (updatedItemAmount > 0) {
            droppedItem.UpdateAmount(updatedItemAmount);
            droppedItem.RemoveCurrentTarget();
        } else {
            droppedItem.DestroyObject();
        }
    }
}
