using UnityEngine;

public class DroppedItemController : MonoBehaviour
{
    [SerializeField]
    private ItemObject item;

    [SerializeField]
    private int amount;

    [SerializeField]
    private float speed = 3f;

    private PlayerController targetPlayer = null;

    private float delayTargetingPlayerTime = 2;
    private float currentDelay = 0;

    public bool CanCollect => !delayPickup;
    private bool delayPickup = false;

    public void Initialize(ItemObject _item, int _amount, bool _delayPickup = false) {
        item = _item;
        amount = _amount;
        delayPickup = _delayPickup;
        GetComponent<SpriteRenderer>().sprite = item.Sprite;
    }

    private void Update() {
        if (delayPickup) {
            currentDelay += Time.deltaTime;
            if (currentDelay >= delayTargetingPlayerTime) {
                delayPickup = false;
            }
        }
    }

    private void FixedUpdate() {
        if (targetPlayer != null) {
            MoveTowardsTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (CanCollect && other.tag == EntityTags.Player) {
            InventoryController inventoryController = other.gameObject.GetComponent<InventoryController>();
            if (inventoryController.Inventory.CanItemFit(item, amount)) {
                SetTarget(other.gameObject.GetComponent<PlayerController>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (CanCollect && other.tag == EntityTags.Player) {
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

    public ItemObject GetItem() {
        return item;
    }

    public int GetAmount() {
        return amount;
    }

    public void UpdateAmount(int newAmount) {
        amount = newAmount;
    }

    public void DestroyObject() {
        Destroy(gameObject);
    }
}
