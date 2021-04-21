using UnityEngine;

public class DroppedItemController : MonoBehaviour
{
    [SerializeField]
    private ItemId itemId;

    [SerializeField]
    private float speed = 3f;

    private PlayerController targetPlayer = null;

    private void FixedUpdate() {
        if (targetPlayer != null) {
            MoveTowardsTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == EntityTags.Player) {
            setTarget(other.gameObject.GetComponent<PlayerController>());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == EntityTags.Player) {
            removeTarget(other.gameObject.GetComponent<PlayerController>());
        }
    }

    private void MoveTowardsTarget() {
        Vector2 targetPlayerPosition = targetPlayer.GetPosition();
        float adjustedSpeed = (speed - 1) + Mathf.Pow(2, speed /  Vector2.Distance(transform.position, targetPlayerPosition));
        transform.position = Vector2.MoveTowards(transform.position, targetPlayerPosition, adjustedSpeed * Time.fixedDeltaTime);
    }

    private void setTarget(PlayerController playerController) {
        if (targetPlayer == null) {
            targetPlayer = playerController;
        }
    }

    private void removeTarget(PlayerController playerController) {
        if (targetPlayer != null && targetPlayer.GetId() == playerController.GetId()) {
            targetPlayer = null;
        }
    }

    public ItemId CollectItem() {
        Destroy(gameObject);
        return itemId;
    }
}
