using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class HotbarController : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private float cameraOffset = 75f;

    [SerializeField]
    private Text selectedRowText;

    [SerializeField]
    private List<HotbarSlotController> hotbarSlots;

    [SerializeField]
    private InventoryController inventoryController;

    [SerializeField]
    private UnityEvent activeHotbotSlotChanged;

    private int activeRow = 0;
    private int activeSlot = 0;
    private int activeItemIndex = 0;

    private int totalRows => inventoryController.Inventory.GetTotalSpaces() / hotbarSlots.Count;

    private void Awake() {
        if (!rectTransform) {
            rectTransform = GetComponent<RectTransform>();

        }

        if (!inventoryController) {
            inventoryController = GetComponent<InventoryController>();
        }
    }

    private void Start() {
        AnchorToBottom();
        SelectSlot(0);
        SelectRow(0);
    }

    public void AnchorToTop() {
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchoredPosition = new Vector3(0, -cameraOffset);
    }

    public void AnchorToBottom() {
        rectTransform.anchorMax = new Vector2(0.5f, 0f);
        rectTransform.anchorMin = new Vector2(0.5f, 0f);
        rectTransform.anchoredPosition = new Vector3(0, cameraOffset);
    }

    public void UpdateItems() {
        List<StorageItem> items = inventoryController.Inventory.Items;
        for(int i = 0; i < hotbarSlots.Count; i++) {
            int itemIndex = i + (hotbarSlots.Count * activeRow);
            if (items[itemIndex] != null) {
                hotbarSlots[i].UpdateAmount(items[itemIndex].Amount.ToString());
                hotbarSlots[i].UpdateItem(items[itemIndex].Item.Sprite);
            } else {
                hotbarSlots[i].ClearAmount();
                hotbarSlots[i].ClearItem();
            }
        }
    }

    public void UpdateSelectedSlot() {
        for(int i = 0; i < hotbarSlots.Count; i++) {
            if (i == activeSlot) {
                hotbarSlots[i].Select();
            } else {
                hotbarSlots[i].Deselect();
            }
        }
    }
    
    public void UpdateSelectedRow() {
        selectedRowText.text = (activeRow + 1).ToString();
    }

    public void UpdateActiveItemIndex() {
        activeItemIndex = activeSlot + (hotbarSlots.Count * activeRow);
        activeHotbotSlotChanged.Invoke();
    }

    private void SelectSlot(int slotIndex) {
        activeSlot = slotIndex;
        UpdateActiveItemIndex();
        UpdateSelectedSlot();
    }

    private void SelectRow(int rowIndex) {
        activeRow = rowIndex;
        UpdateActiveItemIndex();
        UpdateSelectedRow();
        UpdateItems();
    }

    private void OnPreviousHotbarRow() {
        if (activeRow - 1 >= 0) {
            SelectRow(activeRow - 1);
        } else {
            SelectRow(totalRows - 1);
        }
    }

    private void OnNextHotbarRow() {
        if (activeRow + 1 <= totalRows - 1) {
            SelectRow(activeRow + 1);
        } else {
            SelectRow(0);
        }
    }

    private void OnPreviousHotbarSlot() {
        if (activeSlot <= 0) {
            SelectSlot(7);
        } else {
            SelectSlot(activeSlot - 1);
        }
    }

    private void OnNextHotbarSlot() {
        if (activeSlot >= 7) {
            SelectSlot(0);
        } else {
            SelectSlot(activeSlot + 1);
        }
    }

    private void OnScrollHotbar(InputValue input) {
        float value = input.Get<float>();
        if (value > 0) {
            OnPreviousHotbarSlot();
        } else if (value < 0) {
            OnNextHotbarSlot();
        }
    }

    private void OnDropActiveItem() {
        inventoryController.DropItem(activeItemIndex, 1);
    }

    private void OnDropActiveItemStack() {
        inventoryController.DropItemStack(activeItemIndex);
    }

    private void OnHotbarSlot1() {
        SelectSlot(0);
    }

    private void OnHotbarSlot2() {
        SelectSlot(1);
    }

    private void OnHotbarSlot3() {
        SelectSlot(2);
    }

    private void OnHotbarSlot4() {
        SelectSlot(3);
    }

    private void OnHotbarSlot5() {
        SelectSlot(4);
    }

    private void OnHotbarSlot6() {
        SelectSlot(5);
    }

    private void OnHotbarSlot7() {
        SelectSlot(6);
    }

    private void OnHotbarSlot8() {
        SelectSlot(7);
    }
}
