using System.Collections.Generic;

using UnityEngine;

public class HotbarController : MonoBehaviour
{
    [SerializeField]
    private List<HotbarSlotController> hotbarSlots;

    public void updateItems(StorageItem[] items) {
        for(int i = 0; i < hotbarSlots.Count; i++) {
            if (items[i] != null) {
                hotbarSlots[i].UpdateAmount(items[i].Amount.ToString());
                hotbarSlots[i].UpdateItem(items[i].Item.Sprite);
            } else {
                hotbarSlots[i].ClearAmount();
                hotbarSlots[i].ClearItem();
            }
        }
    }
}
