public class StorageItem {
    private ItemUtil.ItemId id;
    private int amount;

    public StorageItem() {
        id = ItemUtil.ItemId.None;
        amount = 0;
    }

    public StorageItem(ItemUtil.ItemId id, int amount) {
        this.id = id;
        this.amount = amount;
    }

    public void IncrementAmount(int increment = 1) {
        amount += increment;
    }

    public void DecrementAmount(int decrement = 1) {
        amount -= decrement;
    }

    public void UpdateId(ItemUtil.ItemId newId) {
        id = newId;
    }

    public ItemUtil.ItemId GetItemId() {
        return id;
    }

    public int GetItemAmount() {
        return amount;
    }
}
