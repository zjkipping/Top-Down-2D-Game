using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField]
    private Sprite facingUpSprite;

    [SerializeField]
    private Sprite facingRightSprite;

    [SerializeField]
    private Sprite facingDownSprite;

    [SerializeField]
    private Sprite facingLeftSprite;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private EntityFacingDirection facingDirection = EntityFacingDirection.Down;

    private void Start()
    {
        if (!spriteRenderer) {
          spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void UpdateDirectionEntityIsFacing(Vector2 movement) {
        if (Time.timeScale == 1f) {
            if (movement.x < 0) {
                facingDirection = EntityFacingDirection.Left;
            } else if(movement.x > 0) {
                facingDirection = EntityFacingDirection.Right;
            }

            if (movement.y < 0) {
                facingDirection = EntityFacingDirection.Down;
            } else if (movement.y > 0) {
                facingDirection = EntityFacingDirection.Up;
            }
            
            RenderSpriteFacingDirection();
        }
    }

    private void RenderSpriteFacingDirection() {
        if (facingDirection == EntityFacingDirection.Left) {
            spriteRenderer.sprite = facingLeftSprite;
        } else if (facingDirection == EntityFacingDirection.Right) {
            spriteRenderer.sprite = facingRightSprite;
        }

        if (facingDirection == EntityFacingDirection.Up) {
            spriteRenderer.sprite = facingUpSprite;
        } else if (facingDirection == EntityFacingDirection.Down) {
            spriteRenderer.sprite = facingDownSprite;
        }
    }
}
