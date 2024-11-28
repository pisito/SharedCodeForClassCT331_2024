using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class HandleNewInputSystem3D : MonoBehaviour
{
    [Tooltip("Any object that implement Interface MoveInput")]
    public Transform targetGameObject;
    private I_InputMovement objectWithMoveInput;
    // I_InputMovement interface;
    public float horizontalInput;
    public float verticalInput;
    public Vector3 moveInput;
    public bool isJumpPressed;
    public bool isSprintPressed;

    void Awake()
    {
        if (targetGameObject == null)
        {
            targetGameObject = transform;
        }
        objectWithMoveInput = targetGameObject.GetComponent<I_InputMovement>();
    }


    private void Update()
    {
        // Update value back from the controller
        isJumpPressed = objectWithMoveInput.IsJumpPressed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input2D = context.ReadValue<Vector2>();

        moveInput = new Vector3(input2D.x, 0f, input2D.y);

        objectWithMoveInput.MoveInput = moveInput;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        // Guide: https://www.youtube.com/watch?v=UlULMaNJmfA
        switch (context.phase)
        {
            case InputActionPhase.Started:
                //Debug.Log("Started Sprinted");
                break;
            case InputActionPhase.Performed:
                //Debug.Log("Perform Sprinted");
                isSprintPressed = true;
                break;
            case InputActionPhase.Canceled:
                //Debug.Log("Canceled Sprinted");
                isSprintPressed = false;
                break;
        }
        objectWithMoveInput.IsSprintPressed = isSprintPressed;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(!isJumpPressed)
        {
            isJumpPressed = true;
            objectWithMoveInput.IsJumpPressed = isJumpPressed;
        }   
    }
}
