using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandleInputOldSystem : MonoBehaviour
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

    [Header("Configs")]
    public KeyCode sprintKey = KeyCode.LeftShift;

    void Start()
    {
        if (targetGameObject != null)
        {
            targetGameObject = transform;
        }
        objectWithMoveInput = targetGameObject.GetComponent<I_InputMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objectWithMoveInput == null)
        {
            Debug.LogWarning("The Object Reference with I_InputMovement Interface is not found");
            return;
        }
        
        HandleMovementAxis();
        HandleSprintButton();
        HandleJumpButton();
    }

    void HandleMovementAxis()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(horizontalInput, 0f, verticalInput);

        objectWithMoveInput.MoveInput = moveInput;
    }

    void HandleJumpButton()
    {
        if(isJumpPressed = Input.GetButtonDown("Jump"))
        {
            objectWithMoveInput.IsJumpPressed = isJumpPressed;
        }
        else
        {
            objectWithMoveInput.IsJumpPressed = false ;
        }
    }

    void HandleSprintButton()
    {
        if (isSprintPressed = Input.GetKey(sprintKey))
        {
            objectWithMoveInput.IsSprintPressed = isSprintPressed;
        }
    }
}
