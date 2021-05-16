using System.Collections.Generic;
using UnityEngine;

public class DebugItemSpawnerController : MonoBehaviour
{
    [SerializeField]
    private List<ItemObject> itemPool;

    [SerializeField]
    private GameObject player;

    private void OnSpawnItem() {
        int itemPoolIndex =  Random.Range(0, itemPool.Count);
        ItemObject item = itemPool[itemPoolIndex];
        int amount = Random.Range(1, item.MaxAmount);
        Vector3 position = player.transform.position;
        GameObject go = Instantiate(item.DroppedItemPrefab, position, Quaternion.identity);
        go.GetComponent<DroppedItemController>().Initialize(item, amount, UtilityMethods.GetRandomDirection());
    }
}
