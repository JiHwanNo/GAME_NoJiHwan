using UnityEngine;
using UnityEngine.InputSystem;
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
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y") * -1;

        centralAxis.rotation = Quaternion.Euler(new Vector3(centralAxis.rotation.x + mouseY, centralAxis.rotation.y + mouseX, 0) * camSpeed);
    }

    void TouchZoom()
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        float prevdistance = Vector2.Distance(touchZeroPrevPos, touchOnePrevPos);
        float currentMagnitude = Vector2.Distance(touchZero.position, touchOne.position);

        float difference = prevdistance - currentMagnitude;
        cam.GetComponent<Camera>().fieldOfView += difference * 0.1f;
    }
    void MouseZoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel");

        cam.localPosition = new Vector3(0, 0, wheel) * camSpeed;

    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1) && ShiftBool)
        {
            CamMove();
        }
        if (Input.touchCount == 2)
        {
            TouchZoom();
        }
        if (Input.GetMouseButton(2))
        {
            MouseZoom();
        }

        centralAxis.position = new Vector3(player.position.x, 0, player.position.z) + new Vector3(0, 6f, 3f);

    }

    public void OnShift(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShiftBool = true;
        }
        else if (context.canceled)
        {
            ShiftBool = false;
        }
    }
}
