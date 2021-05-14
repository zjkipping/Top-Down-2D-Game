using System.Collections;

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

    private float delayPickupTime = 2f;
    private bool delayPickup;

    private bool canCollect = false;
    public bool CanCollect => canCollect;

    public void Initialize(ItemObject _item, int _amount, bool _delayPickup = true) {
        GetComponent<SpriteRenderer>().sprite = _item.Sprite;
        item = _item;
        amount = _amount;
        delayPickup = _delayPickup;
        StartCoroutine(ThrowAndBounceItem(new Vector2(1, 0)));
    }

    private IEnumerator ThrowAndBounceItem(Vector2 direction) {
        Vector2 start = transform.position;
        float distance = 1.5f;
        float startRadius = 0.5f;
        int bounces = 3;
        int currentBounce = 1;

        while(currentBounce < bounces) {
            Vector2 end = start + (direction * (distance / currentBounce));
            Vector2 difference = end - start;
            float span = difference.magnitude;
            float radius = startRadius / currentBounce;

            // Override the radius if it's too small to bridge the points.
            float absRadius = Mathf.Abs(radius);
            if(span > 2f * absRadius)
                radius = absRadius = span/2f;

            Vector2 perpendicular = new Vector2(difference.y, -difference.x)/span;
            perpendicular *= Mathf.Sign(radius) * Mathf.Sqrt(radius*radius - span*span/4f);

            Vector2 center = start + difference/2f + perpendicular;

            Vector2 toStart = start - center;
            float startAngle = Mathf.Atan2(toStart.y, toStart.x);

            Vector2 toEnd = end - center;
            float endAngle = Mathf.Atan2(toEnd.y, toEnd.x);

            // Choose the smaller of two angles separating the start & end
            float travel = (endAngle - startAngle + 5f * Mathf.PI) % (2f * Mathf.PI) - Mathf.PI;

            float progress = 0f;
            while (progress < 1f) {
                float angle = startAngle + progress * travel;
                transform.position = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * absRadius;
                progress += Time.deltaTime/0.2f;
                yield return null;
            }

            transform.position = end;
            start = end;
            currentBounce++;
            yield return null;
        }

        if (delayPickup) {
            StartCoroutine(DelayPickup());
        } else {
            canCollect = true;
        }
    }

    private IEnumerator DelayPickup() {
        canCollect = false;
        float currentDelay = 0;
        while(currentDelay <= delayPickupTime) {
            currentDelay += Time.deltaTime;
            yield return null;
        }
        canCollect = true;
    }

    private void FixedUpdate() {
        if (targetPlayer != null) {
            MoveTowardsTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (canCollect && other.tag == EntityTags.Player) {
            InventoryController inventoryController = other.gameObject.GetComponent<InventoryController>();
            if (inventoryController.Inventory.CanItemFit(item, amount)) {
                SetTarget(other.gameObject.GetComponent<PlayerController>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (canCollect && other.tag == EntityTags.Player) {
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
