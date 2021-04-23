using System.Collections.Generic;

public static class ItemUtil {
    public enum ItemId {
        None,
        TestItem,
    }

    public enum ItemType {
        Resource,
        Placeable,
        Weapon,
        Tool,
        Clothing,
        Accessory,
    }

    public static Dictionary<ItemId, int> ItemMaxAmounts = new Dictionary<ItemId, int>(){
        { ItemId.TestItem, 5 }
    };
}
