using System.Linq;

public class Storage {
    private StorageObject storageObject;
    private StorageItem[] items;
    private int availableSpace;

    public Storage(StorageObject _storageObject) {
        storageObject = _storageObject;
        availableSpace = _storageObject.Spaces;
        items = new StorageItem[_storageObject.Spaces];
    }

    public bool CanItemFit(ItemObject item, int amount) {
        int existingIndex = items.ToList().FindIndex(i => i?.ItemId == item.ItemId && i?.Amount < item.MaxAmount);
        if (existingIndex > -1) {
            StorageItem existingSpace = items[existingIndex];
            if (existingSpace.Amount < item.MaxAmount) {
                return true;
            }
        } else {
            int firstAvailableIndex = items.ToList().FindIndex(e => e == null);
            return firstAvailableIndex > -1;
        }
        return false;
    }

    public int GetTotalItemAmount(ItemObject item) {
        int foundAmount = 0;
        for (int i = 0; i < items.Length; i++) {
            if (items[i]?.ItemId == item.ItemId) {
                foundAmount += items[i].Amount;
            }
        }
        return foundAmount;
    }

    public bool ContainsItemAmount(ItemObject item, int amount) {
        return GetTotalItemAmount(item) >= amount;
    }

    public int AddItem(ItemObject item, int amount) {
        StorageItem storageItem = items.FirstOrDefault(i => i?.ItemId == item.ItemId && i?.Amount < item.MaxAmount);
        if (storageItem != null) {
            if (storageItem.Amount + amount > storageItem.MaxAmount) {
                int incrementAmount = storageItem.MaxAmount - storageItem.Amount;
                storageItem.IncrementAmount(incrementAmount);
                return AddItem(item, amount - incrementAmount);
            } else {
                storageItem.IncrementAmount(amount);
                return 0;
            }
        } else {
            int firstAvailableSpace = items.ToList().FindIndex(i => i  == null);
            if (firstAvailableSpace > -1) {
                if (item.MaxAmount < amount) {
                    items[firstAvailableSpace] = new StorageItem(item, item.MaxAmount);
                    return AddItem(item, amount - item.MaxAmount);
                } else {
                    items[firstAvailableSpace] = new StorageItem(item, amount);
                    return 0;
                }
            } else {
                return amount;
            }
        }
    }

    public int AddItem(ItemObject item, int amount, int space) {
        StorageItem storageItem = items[space];
        if (storageItem == null) {
            if (item.MaxAmount < amount) {
                items[space] = new StorageItem(item, item.MaxAmount);
                return amount - item.MaxAmount;
            } else {
                items[space] = new StorageItem(item, amount);
                return 0;
            }
        } else if (storageItem.ItemId == item.ItemId) {
            if (storageItem.Amount + amount > storageItem.MaxAmount) {
                int incrementAmount = storageItem.MaxAmount - storageItem.Amount;
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

    public int RemoveItem(ItemObject item, int amount) {
        if (amount != 0) {
            int storageItemIndex = items.ToList().FindIndex(i => i?.ItemId == item.ItemId);
            if (storageItemIndex > -1) {
                StorageItem storageItem = items[storageItemIndex];
                if (storageItem.Amount <= amount) {
                    items[storageItemIndex] = null;
                    return RemoveItem(item, amount - storageItem.Amount);
                } else {
                    storageItem.DecrementAmount(amount);
                    return 0;
                }
            }
        }
        return amount;
    }

    public int RemoveItem(ItemObject item, int amount, int space) {
        StorageItem storageItem = items[space];
        if (storageItem != null && storageItem.ItemId == item.ItemId) {
            if (storageItem.Amount <= amount ) {
                items[space] =  null;
                return amount - storageItem.Amount;
            } else {
                storageItem.DecrementAmount(amount);
                return 0;
            }
        }
        return amount;
    }

    public int ReplaceSpaceItem(ItemObject item, int amount, int space) {
        int storageAmount = amount > item.MaxAmount ? item.MaxAmount : amount;
        items[space] = new StorageItem(item, storageAmount);
        return amount - storageAmount;
    }
}
