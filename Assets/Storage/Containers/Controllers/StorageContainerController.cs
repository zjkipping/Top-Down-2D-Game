using UnityEngine;

public class StorageContainerController : MonoBehaviour
{
    [SerializeField]
    private StorageType type;

    private string storageId;
    private Storage storage;
    private int storageSpace;

    private void Awake() {
        storageId = UniqueId.generateId();
        storage = new Storage(type);
    }

    public void Open(Storage inventory) {
        Debug.Log("Opened Storage Container: " + storageId);
    }

    public string GetId() {
        return storageId;
    }
}
