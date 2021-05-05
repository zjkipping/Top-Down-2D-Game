using UnityEngine;

[CreateAssetMenu(fileName = "ResourceObject", menuName = "Top Down 2D Game/Items/ResourceObject", order = 0)]
public class ResourceObject : ItemObject {
  
  private void Awake() {
    type = ItemType.Resource;
  }
}
