using System.Collections;
using System.Collections.Generic;

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

    public void Initialize(ItemObject _item, int _amount, Vector2 direction, bool _delayPickup = true) {
        GetComponent<SpriteRenderer>().sprite = _item.Sprite;
        item = _item;
        amount = _amount;
        delayPickup = _delayPickup;
        StartCoroutine(ThrowAndBounceItem(direction));
    }

    private IEnumerator ThrowAndBounceItem(Vector2 direction) {
        int currentBounce = 0;
        float[] bounceDistance = new float[3]{ 1.25f, 0.75f, 0.5f };
        float[] bounceTime = new float[3]{2.75f, 2.5f, 2.35f};

        while(currentBounce < 3) {
            Vector2 start = transform.position;
            Vector2 end = start + (direction * bounceDistance[currentBounce]);
            Vector2 middle = start + (end - start) / 2 + (Vector2.up * bounceDistance[currentBounce]);
            float count = 0;

            while(count < 1f) {
                Vector3 m1 = Vector3.Lerp(start, middle, count);
                Vector3 m2 = Vector3.Lerp(middle, end, count);
                transform.position = Vector3.Lerp(m1, m2, count);

                count += Time.deltaTime * bounceTime[currentBounce];
                yield return null;
            }

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
