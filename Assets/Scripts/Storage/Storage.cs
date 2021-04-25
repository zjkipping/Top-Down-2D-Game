using System.Linq;

public class Storage {
    private StorageUtil.StorageType type;
    private int totalSpace;
    private int availableSpace;
    private StorageItem[] items;

    public Storage(StorageUtil.StorageType type) {
        ChangeType(type);
    }

    public void ChangeType(StorageUtil.StorageType newType) {
        type = newType;
        StorageUtil.StorageSpaces.TryGetValue(type, out totalSpace);
        StorageItem[] newItems = new StorageItem[totalSpace];
        for(int i = 0; i < newItems.Length; i++) {
            if (items != null && items[i].GetItemId() != ItemUtil.ItemId.None) {
                newItems[i] = items[i];
            } else {
                newItems[i] = new StorageItem();
            }
        }
        items = newItems;
    }

    public bool CanItemFit(ItemUtil.ItemId id, int amount) {
        ItemUtil.ItemMaxAmounts.TryGetValue(id, out int maxAmount);
        int existingIndex = items.ToList().FindIndex(i => i.GetItemId() == id && i.GetItemAmount() < maxAmount);
        if (existingIndex > -1) {
            StorageItem existingSpace = items[existingIndex];
            if (existingSpace.GetItemAmount() < maxAmount) {
                return true;
            }
        } else {
            int firstAvailableIndex = items.ToList().FindIndex(i => i.GetItemId() == ItemUtil.ItemId.None);
            return firstAvailableIndex > -1;
        }
        return false;
    }

    public int AddItem(ItemUtil.ItemId id, int amount) {
        ItemUtil.ItemMaxAmounts.TryGetValue(id, out int maxAmount);
        StorageItem existingSpace = items.FirstOrDefault(i => i.GetItemId() == id && i.GetItemAmount() < maxAmount);
        if (existingSpace != null) {
            int existingItemAmount = existingSpace.GetItemAmount();
            if (existingItemAmount + amount > maxAmount) {
                int incrementAmount = maxAmount - existingItemAmount;
                existingSpace.IncrementAmount(incrementAmount);
                return AddItem(id, amount - incrementAmount);
            } else {
                existingSpace.IncrementAmount(amount);
                return 0;
            }
        } else {
            StorageItem firstAvailableSpace = items.FirstOrDefault(i => i.GetItemId() == ItemUtil.ItemId.None);
            if (firstAvailableSpace != null) {
                firstAvailableSpace.UpdateId(id);
                firstAvailableSpace.IncrementAmount(amount);
                return 0;
            } else {
                return amount;
            }
        }
    }

    public int AddItem(ItemUtil.ItemId id, int amount, int space) {
        StorageItem storageItem = items[space];
        if (storageItem.GetItemId() == id) {
            ItemUtil.ItemMaxAmounts.TryGetValue(id, out int maxAmount);
            int existingItemAmount = storageItem.GetItemAmount();
            if (existingItemAmount + amount > maxAmount) {
                int incrementAmount = maxAmount - existingItemAmount;
                storageItem.IncrementAmount(incrementAmount);
                return amount - incrementAmount;
            } else {
                storageItem.IncrementAmount(amount);
                return 0;
            }
        } else {
            return amount;
        }
    }

    public int GetTotalItemAmount(ItemUtil.ItemId id) {
        int foundAmount = 0;
        for (int i = 0; i < items.Length; i++) {
            if (items[i].GetItemId() == id) {
                foundAmount += items[i].GetItemAmount();
            }
        }
        return foundAmount;
    }

    public bool ContainsItemAmount(ItemUtil.ItemId id, int amount) {
        return GetTotalItemAmount(id) >= amount;
    }

    public int RemoveItem(ItemUtil.ItemId id, int amount) {
        StorageItem existingSpace = items.FirstOrDefault(i => i.GetItemId() == id);
        if (existingSpace.GetItemId() == id) {
            int existingItemAmount = existingSpace.GetItemAmount();
            if (existingItemAmount < amount) {
                existingSpace.DecrementAmount(existingItemAmount);
                existingSpace.SetEmpty();
                return RemoveItem(id, amount - existingItemAmount);
            } else {
                existingSpace.DecrementAmount(amount);
                if (existingSpace.GetItemAmount() == 0) {
                    existingSpace.SetEmpty();
                }
                return 0;
            }
        }
        return amount;
    }

    public int RemoveItem(ItemUtil.ItemId id, int amount, int space) {
        StorageItem storageItem = items[space];
        if (storageItem.GetItemId() == id) {
            int existingItemAmount = storageItem.GetItemAmount();
            if (existingItemAmount < amount) {
                storageItem.DecrementAmount(existingItemAmount);
                storageItem.SetEmpty();
                return amount - existingItemAmount;
            } else {
                storageItem.DecrementAmount(amount);
                if (storageItem.GetItemAmount() == 0) {
                    storageItem.SetEmpty();
                }
                return 0;
            }
        }
        return amount;
    }
}
