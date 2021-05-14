using UnityEngine;

public class RenderOrderController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        if (!spriteRenderer) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void LateUpdate() {
        this.spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (this.spriteRenderer.bounds.min).y * -1;
    }
}
