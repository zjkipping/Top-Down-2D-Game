using UnityEngine;

public class CharacterMenuController : MonoBehaviour
{
    [SerializeField]
    private MenuController menuController;

    private void Awake()
    {
        if (!menuController) {
            menuController = GetComponent<MenuController>();
        }
    }
}
