using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject objectToFollow;

    [SerializeField]
    private GameObject topWall;

    [SerializeField]
    private GameObject rightWall;

    [SerializeField]
    private GameObject bottomWall;

    [SerializeField]
    private GameObject leftWall;

    [SerializeField]
    private float wallOffset = 0.5f;

    private void Start()
    {
        if (!cam) {
            cam = GetComponent<Camera>();
        }
    }

    private void LateUpdate()
    {
        Vector3 newPosition = CalculateCameraCenter();

        float height = cam.orthographicSize * 2f;
        float widthOffset = (height * cam.aspect) / 2f;
        float heightOffset = height / 2f;

        if (newPosition.y + heightOffset >= topWall.transform.position.y - wallOffset) {
            newPosition.y = topWall.transform.position.y - wallOffset - heightOffset;
        } else if (newPosition.y - heightOffset <= bottomWall.transform.position.y + wallOffset) {
            newPosition.y = bottomWall.transform.position.y  + wallOffset + heightOffset;
        }

        if (newPosition.x + widthOffset >= rightWall.transform.position.x - wallOffset) {
            newPosition.x = rightWall.transform.position.x - wallOffset - widthOffset;
        } else if (newPosition.x - widthOffset <= leftWall.transform.position.x + wallOffset) {
            newPosition.x = leftWall.transform.position.x + wallOffset + widthOffset;
        }

        cam.transform.position = newPosition;
    }

    private Vector3 CalculateCameraCenter() {
        return new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, cam.transform.position.z);
    }
}
