using UnityEngine;
using UnityEngine.UI;

public class HotbarSlotController : MonoBehaviour
{
    [SerializeField]
    private Text amountText;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Image highlightImage;
    [SerializeField]
    private Text keybindText;

    public void UpdateItem(Sprite item) {
        itemImage.sprite = item;
        itemImage.color = Color.white;
    }

    public void ClearItem() {
        itemImage.sprite = null;
        itemImage.color = Color.clear;
    }

    public void UpdateAmount(string amount) {
        amountText.text = amount;
    }

    public void ClearAmount() {
        amountText.text = "";
    }

    public void UpdateKeybind(string keybind) {
        amountText.text = keybind;
    }

    public void Select() {
        highlightImage.color = Color.white;
    }

    public void Deselect() {
        highlightImage.color = Color.clear;
    }
}
