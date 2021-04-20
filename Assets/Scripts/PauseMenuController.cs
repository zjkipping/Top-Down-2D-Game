using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private MenuController menuController;

    private void Start()
    {
        if (!menuController) {
            menuController = GetComponent<MenuController>();
        }
    }

    public void ResumeGame() {
        menuController.ToggleMenu();
    }

    public void ChangeSettings() {
        Debug.Log("Change Settings!");
    }

    public void SaveGame() {
        Debug.Log("Saving Game!");
    }

    public void LoadGame() {
        Debug.Log("Loading Game!");
    }

    public void ExitGame() {
        Debug.Log("Saving & Exiting Game!");
        Application.Quit();
    }
}
