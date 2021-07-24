using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour, MouseInput.IUIActions
{
    public GameObject MenuBackGround;

    MouseInput UI;
    private void Awake()
    {
        UI = new MouseInput();
        UI.UI.SetCallbacks(this);
    }

    private void OnEnable()
    {
        UI.UI.Enable();
    }
    private void OnDisable()
    {
        UI.UI.Disable();
    }
    public void OnESC(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!MenuBackGround.activeSelf)
            {
                MenuBackGround.SetActive(true);
            }
            else
            {
                MenuBackGround.SetActive(false);
            }
        }

    }

    public void EscButton()
    {
        Application.Quit();
    }
}
