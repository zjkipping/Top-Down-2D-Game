using System.Linq;
using UnityEngine;

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

    public int AddItem(ItemUtil.ItemId id, int amount) {
        ItemUtil.ItemMaxAmounts.TryGetValue(id, out int maxAmount);
        StorageItem existingSpace = items.FirstOrDefault(i => i.GetItemId() == id && i.GetItemAmount() < maxAmount);
        if (existingSpace != null) {
            int existingItemAmount = existingSpace.GetItemAmount();
            if (existingItemAmount + amount > maxAmount) {
                int incrementAmount = maxAmount - existingItemAmount;
                existingSpace.IncrementAmount(incrementAmount);
                return amount - incrementAmount;
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
        return amount;
    }

    public bool RemoveItem(ItemUtil.ItemId id, int amount) {
        return false;
    }

    public bool RemoveItem(ItemUtil.ItemId id, int amount, int space) {
        return false;
    }
}
