using UnityEngine;

public class InteractionController : MonoBehaviour {
    private InteractableController targetInteractable = null;

    [SerializeField]
    private InventoryController inventoryController;

    private void OnTriggerEnter2D(Collider2D other) {
        InteractableController interactable = other.GetComponent<InteractableController>();
        if (interactable != null) {
            targetInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        InteractableController interactable = other.GetComponent<InteractableController>();
        if (targetInteractable != null && targetInteractable.Id == interactable.Id) {
            targetInteractable = null;
        }
    }

    private void OnInteract() {
        if (targetInteractable != null) {
            targetInteractable.Interact(inventoryController);
        }
    }
}
