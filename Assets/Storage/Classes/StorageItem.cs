[System.Serializable]
public class StorageItem {
    private ItemObject item;
    private int amount;

    public ItemObject Item => item;
    public int Amount => amount;
    public ItemId ItemId => item.ItemId;
    public ItemType ItemType => item.ItemType;
    public int MaxAmount => item.MaxAmount;

    public StorageItem(ItemObject _item, int _amount) {
        item = _item;
        amount = _amount;
    }

    public void IncrementAmount(int increment = 1) {
        amount += increment;
    }

    public void DecrementAmount(int decrement = 1) {
        amount -= decrement;
    }
}
