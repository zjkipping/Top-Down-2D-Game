using UnityEngine;

[CreateAssetMenu(fileName = "PlaceableObject", menuName = "Top Down 2D Game/Items/PlaceableObject", order = 0)]
public class PlaceableObject : ItemObject {
    [SerializeField]
    private GameObject placedItemPrefab;

    public GameObject PlacedItemPrefab { get { return placedItemPrefab; } }

    private void Awake() {
        type = ItemType.Placeable;
    }
}
