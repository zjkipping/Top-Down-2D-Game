public class StorageItem {
    private ItemObject item;
    private int amount;

    public ItemObject Item { get { return item; } }
    public int Amount { get { return amount; } }
    public ItemId ItemId { get { return item.ItemId; } }
    public ItemType ItemType { get { return item.ItemType; } }
    public int MaxAmount { get { return item.MaxAmount; } }

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
