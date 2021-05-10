using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    private void Start() {
        InventoryUpdated();
        SelectSlot(0);
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

        hotbar.UpdateItems(hotbarItems);
    }

    private void SelectSlot(int slotIndex) {
        selectedHotbarSlot = slotIndex;
        hotbar.UpdateSelectedSlot(selectedHotbarSlot);
    }

    private void OnPreviousHotbarSlot() {
        if (selectedHotbarSlot <= 0) {
            SelectSlot(7);
        } else {
            SelectSlot(selectedHotbarSlot - 1);
        }
    }

    private void OnNextHotbarSlot() {
        if (selectedHotbarSlot >= 7) {
            SelectSlot(0);
        } else {
            SelectSlot(selectedHotbarSlot + 1);
        }
    }

    private void OnScrollHotbar(InputValue input) {
        float value = input.Get<float>();
        if (value > 0) {
            OnPreviousHotbarSlot();
        } else if (value < 0) {
            OnNextHotbarSlot();
        }
    }

    private void OnHotbarSlot1() {
        SelectSlot(0);
    }

    private void OnHotbarSlot2() {
        SelectSlot(1);
    }

    private void OnHotbarSlot3() {
        SelectSlot(2);
    }

    private void OnHotbarSlot4() {
        SelectSlot(3);
    }

    private void OnHotbarSlot5() {
        SelectSlot(4);
    }

    private void OnHotbarSlot6() {
        SelectSlot(5);
    }

    private void OnHotbarSlot7() {
        SelectSlot(6);
    }

    private void OnHotbarSlot8() {
        SelectSlot(7);
    }
}
