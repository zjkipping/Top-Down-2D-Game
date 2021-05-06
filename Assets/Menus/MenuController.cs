using UnityEngine;

public class MenuController : MonoBehaviour
{
    public Canvas menuUI;

    [SerializeField]
    private bool pauseTimeWhenOpen = true;

    private void Awake()
    {
        if (!menuUI) {
            menuUI = GetComponent<Canvas>();
        }
    }

    private void Start() {
        menuUI.enabled = false;
    }

    public void ToggleMenu() {
        menuUI.enabled = !menuUI.enabled;
        if (pauseTimeWhenOpen) {
            if (menuUI.enabled) {
                Time.timeScale = 0f;
            } else {
                Time.timeScale = 1f;
            }
        }
    }
}
