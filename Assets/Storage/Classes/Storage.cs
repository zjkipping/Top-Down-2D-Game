using System.Collections.Generic;
using System.Linq;

public delegate void StorageUpdatedEvent();

[System.Serializable]
public class Storage {
    private StorageObject storageObject;
    private StorageItem[] items;

    public List<StorageItem> Items => items.ToList();

    public event StorageUpdatedEvent StorageUpdated;

    public Storage(StorageObject _storageObject) {
        storageObject = _storageObject;
        items = new StorageItem[_storageObject.Spaces];
        OnStorageUpdated();
    }

    public int GetTotalSpaces() {
        return storageObject.Spaces;
    }

    public StorageItem GetItemAtIndex(int index) {
        return items[index];
    }

    private void OnStorageUpdated() {
        StorageUpdated?.Invoke();
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
                OnStorageUpdated();
                return AddItem(item, amount - incrementAmount);
            } else {
                storageItem.IncrementAmount(amount);
                OnStorageUpdated();
                return 0;
            }
        } else {
            int firstAvailableSpace = items.ToList().FindIndex(i => i  == null);
            if (firstAvailableSpace > -1) {
                if (item.MaxAmount < amount) {
                    items[firstAvailableSpace] = new StorageItem(item, item.MaxAmount);
                    OnStorageUpdated();
                    return AddItem(item, amount - item.MaxAmount);
                } else {
                    items[firstAvailableSpace] = new StorageItem(item, amount);
                    OnStorageUpdated();
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
                OnStorageUpdated();
                return amount - item.MaxAmount;
            } else {
                items[space] = new StorageItem(item, amount);
                OnStorageUpdated();
                return 0;
            }
        } else if (storageItem.ItemId == item.ItemId) {
            if (storageItem.Amount + amount > storageItem.MaxAmount) {
                int incrementAmount = storageItem.MaxAmount - storageItem.Amount;
                storageItem.IncrementAmount(incrementAmount);
                OnStorageUpdated();
                return amount - incrementAmount;
            } else {
                storageItem.IncrementAmount(amount);
                OnStorageUpdated();
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
                    OnStorageUpdated();
                    return RemoveItem(item, amount - storageItem.Amount);
                } else {
                    storageItem.DecrementAmount(amount);
                    OnStorageUpdated();
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
                OnStorageUpdated();
                return amount - storageItem.Amount;
            } else {
                storageItem.DecrementAmount(amount);
                OnStorageUpdated();
                return 0;
            }
        }
        return amount;
    }

    public StorageItem RemoveItem(int space) {
        StorageItem item = items[space];
        items[space] = null;
        OnStorageUpdated();
        return item;
    }

    public StorageItem RemoveItem(int space, int amount) {
        StorageItem item = items[space];
        if (item != null) {
            item.DecrementAmount(amount);
            if (item.Amount <= 0) {
                items[space] = null;
            }
            OnStorageUpdated();
        }
        return item;
    }

    public int ReplaceSpaceItem(ItemObject item, int amount, int space) {
        int storageAmount = amount > item.MaxAmount ? item.MaxAmount : amount;
        items[space] = new StorageItem(item, storageAmount);
        OnStorageUpdated();
        return amount - storageAmount;
    }
}
