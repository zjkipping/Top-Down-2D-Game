using UnityEngine;

public class StorageContainerController : MonoBehaviour
{
    [SerializeField]
    private StorageObject storageObject;

    private Storage storage;
    private int storageSpace;

    private void Awake() {
        storage = new Storage(storageObject);
    }

    public void Open(InventoryController inventoryController) {
        Debug.Log("Opened Storage Container");
    }
}
