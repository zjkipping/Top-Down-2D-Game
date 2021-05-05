using UnityEngine;

[CreateAssetMenu(fileName = "ItemObject", menuName = "Top Down 2D Game/Items/ItemObject", order = 0)]
public abstract class ItemObject : ScriptableObject {
    [SerializeField]
    protected ItemId id;
    [SerializeField]
    protected ItemType type;
    [SerializeField]
    protected int maxAmount;
    [SerializeField]
    protected Texture2D texture;

    public ItemId ItemId {
        get {
            return id;
        } 
    }

    public ItemType ItemType {
        get {
            return type;
        } 
    }

    public int MaxAmount {
        get {
            return maxAmount;
        }
    }

    public Texture2D Texture {
        get {
            return texture;
        } 
    }
}
