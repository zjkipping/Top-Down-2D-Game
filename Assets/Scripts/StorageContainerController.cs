using UnityEngine;

public class StorageContainerController : MonoBehaviour
{
    [SerializeField] int storageId;

    public void Open() {
        Debug.Log("Opened Storage Container: " + storageId);
    }

    public int GetId() {
        return storageId;
    }
}
