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

    public ItemId ItemId { get { return id; } }
    public ItemType ItemType { get { return type; } }
    public int MaxAmount { get { return maxAmount; } }
    public Sprite Sprite { get { return sprite; } }
    public GameObject DroppedItemPrefab { get { return droppedItemPrefab; } }
}
