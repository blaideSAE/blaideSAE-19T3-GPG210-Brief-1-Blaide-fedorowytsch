using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlFirstPerson : MonoBehaviour
{
    public GameObject cameraGameObj;
    public Camera playerCamera;
    public float inputFilter;
    public float rotationSpeed;
    public bool invertYAxis;
    private float mouseX, mouseY;

    private float deltaX, deltaY;
    private Vector3 camAngle;
    private Vector3 targetAngle;

    private Transform standInRotor;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        camAngle = cameraGameObj.transform.rotation.eulerAngles;
        camAngle.y = camAngle.y % 360;
        
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y") *(invertYAxis? -1: 1);

        if (Mathf.Abs(mouseX) >= inputFilter)
        {
            deltaX = mouseX*Time.deltaTime *rotationSpeed;
        }
        else
        {
            deltaX = 0;
        }
        if (Mathf.Abs(mouseY) >= inputFilter)
        {
            deltaY = mouseY *Time.deltaTime*rotationSpeed;
        }
        else
        {
            deltaY = 0;
        }
        
        camAngle = cameraGameObj.transform.rotation.eulerAngles;
        camAngle.x += deltaY;
        camAngle.y += deltaX;
        
        // camera angle can not rotate to completely vertical  because weird stuff happens... because im not rotating the z axis at all.
        if (camAngle.x > 50 && camAngle.x < 180)
        {
            camAngle.x = 50;
        }
        else if (camAngle.x < 271 && camAngle.x > 180)
        {
            camAngle.x = 271;
        }
        transform.rotation = Quaternion.Euler(0,camAngle.y,0);
        cameraGameObj.transform.rotation = Quaternion.Euler(camAngle.x,camAngle.y,0);
    }
}
