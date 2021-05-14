using UnityEngine;

public class ItemCollectionController : MonoBehaviour {
    [SerializeField]
    private InventoryController inventoryController;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == EntityTags.DroppedItem) {
            DroppedItemController droppedItem = other.GetComponent<DroppedItemController>();
            if (droppedItem.CanCollect && Vector2.Distance(transform.position, other.transform.position) < 0.5f) {
                AttemptItemCollection(droppedItem);
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
