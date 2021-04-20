using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == EntityTags.DroppedItem) {
            if (Vector2.Distance(transform.position, other.transform.position) < 0.5f) {
                ItemId itemId = other.GetComponent<DroppedItemController>().CollectItem();
                Debug.Log("Collected Item: " + itemId);
            }
        }
    }
}
