using System.Collections.Generic;

public static class StorageUtil {
    public enum StorageType {
        Debug,
        Small,
        Medium,
        Large
    }

    public static Dictionary<StorageType, int> StorageSpaces = new Dictionary<StorageType, int>() {
        { StorageType.Debug, 2 },
        { StorageType.Small, 8 },
        { StorageType.Medium, 16 },
        { StorageType.Large, 24 },
    };
}
