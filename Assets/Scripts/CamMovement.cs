using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class CamMovement : MonoBehaviour, MouseInput.IMouseActions
{
    public Transform player;
    public Transform centralAxis;
    public Transform cam;
    public float camSpeed;
    float mouseX;
    float mouseY;
    float wheel;

    bool ShiftBool;
    MouseInput IMouseActions;

    public Text test;
    private void Awake()
    {
        IMouseActions = new MouseInput();
        IMouseActions.Mouse.SetCallbacks(this);
        ShiftBool = false;
    }

    private void OnEnable()
    {
        IMouseActions.Mouse.Enable();
    }
    private void OnDisable()
    {
        IMouseActions.Mouse.Disable();
    }
    void CamMove()
    {
        if (Input.GetMouseButton(1)&&ShiftBool)
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y") * -1;

            centralAxis.rotation = Quaternion.Euler(new Vector3(centralAxis.rotation.x + mouseY, centralAxis.rotation.y + mouseX, 0) * camSpeed);

        }
    }
    void Zoom()
    { // 줌이 픽스트업데이트에서 계속 고정값으로 넣기 때문에... 안됨.
        if(Input.touchCount ==2)
        {
            
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            Vector2 touchZeroPrePos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrePos = touchOne.position - touchOne.deltaPosition;

            float PreMagnitude = (touchZeroPrePos - touchOnePrePos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float different = currentMagnitude - PreMagnitude;
            
            cam.localPosition = new Vector3(0, 0, different*0.01f);
            test.text = cam.localPosition.ToString();
        }
        else
        {
            wheel += Input.GetAxis("Mouse ScrollWheel");

            cam.localPosition = new Vector3(0, 0, wheel) * camSpeed;
        }
    }
    private void FixedUpdate()
    {
        CamMove();
        Zoom();
        centralAxis.position = new Vector3(player.position.x, 0, player.position.z) + new Vector3(0, 6f, 6.5f);

    }

    public void OnShift(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShiftBool = true;
        }
        else if(context.canceled)
        {
            ShiftBool = false;
        }
    }
}
