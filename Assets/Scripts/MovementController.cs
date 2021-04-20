using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Vector2 movement;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private Rigidbody2D rb;

    private void Start()
    {
        if (!rb) {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate() {
        if (Time.timeScale == 1f) {
            UpdateMovement();
        }
    }

    private void UpdateMovement() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void ChangeSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public void ChangeMovement(Vector2 newMovement) {
        movement = newMovement;
    }
}
