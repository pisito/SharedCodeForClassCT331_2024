using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RotateWithMouseInputOldSystem : MonoBehaviour
{
    public Transform target;

    public float mouseXInput;
    public float mouseYInput;

    // Sensitivity for the mouse movement
    public float sensitivityX = 100f;
    public float sensitivityY = 100f;

    // Clamping angles for vertical rotation
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    // Keep track of your rotation
    private float rotationX = 0f; // Rotation around the X-axis (vertical)
    private float rotationY = 0f; // Rotation around the Y-axis (horizontal)

    public bool invertX = false;
    public bool invertY = true;


    public bool useRotationKey = true;
    // Key to hold for enabling rotation, default can be key or mouse button, mouse1
    public KeyCode rotationKey = KeyCode.LeftShift;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInputs();
        if(useRotationKey) 
        { 
            if(Input.GetKey(rotationKey))
            {
                HandleRoation();
            }
        }
        else
        {
            HandleRoation();
        }
    }

    void HandleMouseInputs()
    {
        mouseXInput = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        mouseYInput = Input.GetAxis("Mouse Y") * sensitivityX * Time.deltaTime;

        
    }

    void HandleRoation()
    {
        // Update rotation values
        rotationY += (mouseXInput * (invertX ? -1 : 1));
        rotationX += (mouseYInput * (invertY ? -1 : 1)); // by default, Y movement should be inverted

        // Clamp the vertical rotation
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        // Apply the rotation to the target transform
        target.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
