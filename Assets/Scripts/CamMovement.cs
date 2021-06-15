using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform player;
    public Transform centralAxis;
    public Transform cam;
    public float camSpeed;
    float mouseX;
    float mouseY;
    float wheel;

    private void Start()
    {
        centralAxis.Rotate(90f, 0f, 0f);
    }
    void CamMove()
    {
        if(Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y")*-1;

            centralAxis.rotation = Quaternion.Euler(new Vector3(centralAxis.rotation.x +mouseY, centralAxis.rotation.y + mouseX,0)*camSpeed);

        }
    }
    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel");
        if (wheel >= -1)
        {
            wheel = -1;
        }
        if (wheel <= -15)
        {
            wheel = -15;
        }
            
        cam.localPosition = new Vector3(0,0,wheel);
        
    }
    private void FixedUpdate()
    {
        CamMove();
        Zoom();
        centralAxis.position = new Vector3(player.position.x, 0, player.position.z)+new Vector3(0,10,0); 


    }
}
