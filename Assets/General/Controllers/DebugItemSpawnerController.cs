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
        Vector3 direction = new Vector2(Random.Range(-1f, 1f) * 1.5f, Random.Range(-1f, 1f) * 1.5f);
        Vector3 position = player.transform.position + direction;
        GameObject go = Instantiate(item.DroppedItemPrefab, position, Quaternion.identity);
        go.GetComponent<DroppedItemController>().Initialize(item, amount);
    }
}
