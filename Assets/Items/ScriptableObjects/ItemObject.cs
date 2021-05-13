using UnityEngine;

public abstract class ItemObject : ScriptableObject {
    [SerializeField]
    protected ItemId id;
    [SerializeField]
    protected ItemType type;
    [SerializeField]
    protected int maxAmount;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    protected GameObject droppedItemPrefab;

    public ItemId ItemId => id;
    public ItemType ItemType => type;
    public int MaxAmount => maxAmount;
    public Sprite Sprite => sprite;
    public GameObject DroppedItemPrefab => droppedItemPrefab;
}
