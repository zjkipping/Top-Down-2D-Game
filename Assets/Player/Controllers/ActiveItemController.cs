using UnityEngine;

public class ActiveItemController : MonoBehaviour {
    [SerializeField]
    private InventoryController inventoryController;

    [SerializeField]
    private HotbarController hotbarController;

    private void Awake() {
        if (!inventoryController) {
            
        }
    }
}