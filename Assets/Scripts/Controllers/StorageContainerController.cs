using UnityEngine;

public class StorageContainerController : MonoBehaviour
{
    [SerializeField]
    private StorageUtil.StorageType type;

    private string storageId;
    private Storage storage;
    private int storageSpace;

    private void Start() {
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
