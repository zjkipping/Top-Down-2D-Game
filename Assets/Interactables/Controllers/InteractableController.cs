using UnityEngine;
using UnityEngine.Events;

public class InteractableController : MonoBehaviour {
    private string id;

    public string Id => id;

    [SerializeField]
    private UnityEvent<InventoryController> OnInteract;

    private void Start() {
        id = UniqueId.generateId();
    }

    public void Interact(InventoryController inventoryController) {
        Debug.Log("Interacted with: " + Id.ToString());
        OnInteract.Invoke(inventoryController);
    }
}
