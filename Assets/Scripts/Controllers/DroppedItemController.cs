using UnityEngine;

public class DroppedItemController : MonoBehaviour
{
    [SerializeField]
    private ItemUtil.ItemId itemId;

    [SerializeField]
    private int amount;

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
            SetTarget(other.gameObject.GetComponent<PlayerController>());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == EntityTags.Player) {
            RemoveTarget(other.gameObject.GetComponent<PlayerController>());
        }
    }

    private void MoveTowardsTarget() {
        Vector2 targetPlayerPosition = targetPlayer.GetPosition();
        float adjustedSpeed = (speed - 1) + Mathf.Pow(2, speed /  Vector2.Distance(transform.position, targetPlayerPosition));
        transform.position = Vector2.MoveTowards(transform.position, targetPlayerPosition, adjustedSpeed * Time.fixedDeltaTime);
    }

    private void SetTarget(PlayerController playerController) {
        if (targetPlayer == null) {
            targetPlayer = playerController;
        }
    }

    public void RemoveTarget(PlayerController playerController) {
        if (targetPlayer != null && targetPlayer.GetId() == playerController.GetId()) {
            targetPlayer = null;
        }
    }

    public void RemoveCurrentTarget() {
        this.targetPlayer = null;
    }

    public void CollectItem(out ItemUtil.ItemId id, out int amount) {
        id = this.itemId;
        amount = this.amount;
    }

    public void UpdateAmount(int newAmount) {
        amount = newAmount;
    }

    public void DestroyObject() {
        Destroy(gameObject);
    }
}
