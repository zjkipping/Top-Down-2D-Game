using UnityEngine;

public class CharacterMenuController : MonoBehaviour
{
    [SerializeField]
    private MenuController menuController;

    private void Start()
    {
        if (!menuController) {
            menuController = GetComponent<MenuController>();
        }
    }
}
