using UnityEngine;
using UnityEngine.Events;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private StorageObject storageObject;

    [SerializeField]
    private UnityEvent inventoryUpdated;

    private Storage storage;
    public Storage Inventory => storage;

    private void Awake() {
        storage = new Storage(storageObject);
    }

    private void Start() {
        inventoryUpdated.Invoke();
        storage.StorageUpdated += () => inventoryUpdated.Invoke();
    }
}
