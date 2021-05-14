using System.Collections.Generic;
using UnityEngine;

public class DebugItemSpawnerController : MonoBehaviour
{
    [SerializeField]
    private List<ItemObject> itemPool;

    [SerializeField]
    private GameObject player;

    private void OnSpawnItem() {
        int itemPoolIndex = (int)(Random.value * itemPool.Count);
        ItemObject item = itemPool[itemPoolIndex];
        int amount = (int)(Random.value * (item.MaxAmount - 1)) + 1;
        Vector3 position = player.transform.position - new Vector3(0, 1.5f, 0);
        GameObject go = Instantiate(item.DroppedItemPrefab, position, Quaternion.identity);
        go.GetComponent<DroppedItemController>().Initialize(item, amount);
    }
}
