using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerId playerId;

    [SerializeField]
    private float walkSpeed = 4f;

    [SerializeField]
    private float sprintSpeed = 6f;

    [SerializeField]
    private MovementController movementController;

    [SerializeField]
    private SpriteController spriteController;

    private void Start()
    {
        if (!movementController) {
            movementController = GetComponent<MovementController>();
            movementController.ChangeMovement(Vector2.zero);
            movementController.ChangeSpeed(walkSpeed);
        }
        if (!spriteController) {
            spriteController = GetComponent<SpriteController>();
        }
    }

    private void OnMovement(InputValue input) {
        Vector2 movement = input.Get<Vector2>();
        movementController.ChangeMovement(movement);
        spriteController.UpdateDirectionEntityIsFacing(movement);
    }

    private void OnSprint(InputValue input) {
        float sprintInput = input.Get<float>();
        if (sprintInput > 0) {
            movementController.ChangeSpeed(sprintSpeed);
        } else {
            movementController.ChangeSpeed(walkSpeed);
        }
    }

    public PlayerId GetId() {
        return playerId;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }
}
