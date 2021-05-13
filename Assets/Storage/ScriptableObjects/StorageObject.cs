using UnityEngine;

[CreateAssetMenu(fileName = "StorageObject", menuName = "Top Down 2D Game/Storage", order = 0)]
public class StorageObject : ScriptableObject {
    [SerializeField]
    protected StorageSize size;
    [SerializeField]
    protected int spaces;

    public StorageSize Size => size;
    public int Spaces => spaces;
}
