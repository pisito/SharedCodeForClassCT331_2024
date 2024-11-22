using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CharacterController3D : MonoBehaviour, I_InputMovement
{
    [Header("Unity components")]
    public CharacterController characterController;

    [Header("Player Required GameObjects")]
    public Transform visualRoots;
    public Transform referenceCamera;

    [Header("Your adjustments")]
    [Tooltip("meter per second")]
    public float moveSpeed = 2.0f;
    [Tooltip("all movement *  scale")]
    public float moveSpeedScale = 1.0f;
    [Tooltip("move * scale")]
    public float sprintScale = 1.5f;
    [Tooltip("minus mean -y direction")]
    public float gravity = -9.81f;
    [Tooltip("gravity * scale")]
    public float gravityScale = 2f;
    [Tooltip("Use Camera forward as front direction")]
    public bool useCameraReference = true;
    [Tooltip("Use Camera forward as front direction")]
    public bool enableRotateVisual = true;
    [Tooltip("degree per second")]
    public float visualRotationSpeed = 5f;
    [Tooltip("visual rotation * scale")]
    public float visualRotationScale = 1f;
    [Tooltip("Enable / Disable Multiple Jump")]
    public bool canMultiJump = true;
    [Tooltip("If Enable, How many max jumps?")]
    public int maxMultiJump = 2;
    [Tooltip("Jump ignore gravity at the frame, jumpHeight * jumpScale * -gravity")]
    public float jumpHeight = 0.7f;
    [Tooltip("jumpPower * scale")]
    public float jumpScale = 1f;

    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "Ground Layer should be a dedicated layer. " +
        "Using Default means it will detect collider of the Player as well since everything will be " +
        "in /default/ folder by default OwO ";
    public LayerMask groundLayer;
    [Tooltip("Distance (Radius) of ground sphere check")]
    public float groundCheckRadius = 0.2f;

    [Header("Flags for Debugs")]
    [Tooltip("draw debug gizmo shape or not")]
    public bool isDrawGizmoSelected = true;
    [Tooltip("Is Character stand on ground")]
    public bool isGrounded = false;
    [Tooltip("Vector that track move input from input system")]
    public Vector3 moveInput;
    [Tooltip("Vector that update move input with reference view i.e. camera")]
    public Vector3 correctedMoveInput;
    [Tooltip("movement velocity XZ plane (Floor)")]
    public Vector3 floorPlaneMovement;
    [Tooltip("movement velocity Y direction (Vertical)")]
    public Vector3 yMovement;
    [Tooltip("Is the jump button pressed?")]
    public bool isJumpPressed;
    [Tooltip("Count total jump applied to jump function")]
    public int jumpedCount;
    [Tooltip("Is the sprint button pressed?")]
    public bool isSprintPressed;

    // Interface provider for MoveInput Access
    public Vector3 MoveInput 
    { 
        get {
            return moveInput;
        }
        set
        {
            moveInput = value;
        }
    }
    public bool IsJumpPressed {
        get
        {
            return isJumpPressed;
        }
        set
        {
            isJumpPressed = value;
        }
    }
    public bool IsSprintPressed {
        get {
            return isSprintPressed;
        }
        set {
            isSprintPressed = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(referenceCamera == null)
        {
            referenceCamera = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    { 

        // Constant Checked
        CheckOnGround();

        HandleMovement();

        if (enableRotateVisual)
        {
            HandleVisualRotation();
        }
        
        // Handle XZ Plane (Floor) Movement
        characterController.Move( floorPlaneMovement * Time.deltaTime );

        // Adjust y-value after setup movement
        HandleGravity();

        if(isJumpPressed)
        {
            HandleJump();

            isJumpPressed = false;
        }

        // Handle y movement
        characterController.Move(yMovement * Time.deltaTime);
    }

    void CheckOnGround()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, groundCheckRadius, groundLayer);
        isGrounded = (overlappedColliders != null && overlappedColliders.Length > 0);
        
        // Reset parameters if on ground
        if(isGrounded)
        {
            jumpedCount = 0;
        }
        
    }

    void HandleMovement()
    {
        Vector3 bufferVector = Vector3.zero;
        if (useCameraReference)
        {
            
            bufferVector = (referenceCamera.forward * moveInput.z)  // Up - down
                            + (referenceCamera.right * moveInput.x); // Left - right
        }
        else
        {
            // Use game object direction instead ~ car, boat
            bufferVector = (transform.forward * moveInput.z)  // Up - down
                            + (transform.right * moveInput.x); // Left - right
        }

        correctedMoveInput = new Vector3(bufferVector.x, 0f, bufferVector.z);
        floorPlaneMovement = correctedMoveInput;

        if (isSprintPressed)
        {
            // sprinting
            floorPlaneMovement = correctedMoveInput * moveSpeed * sprintScale * moveSpeedScale;
            isSprintPressed = false;
        }
        else
        {
            // no sprinting
            floorPlaneMovement = correctedMoveInput *moveSpeed * moveSpeedScale;
        }
    }

    void HandleGravity()
    {
        if (isGrounded && yMovement.y < 0)
        {
            yMovement.y = -0.1f;
        }
        else
        {
            yMovement.y += gravity * gravityScale * Time.deltaTime;
        }

    }

    // Should Applied after HandleGravity()
    void HandleJump()
    {
        //Debug.Log("HandleJump");
        if (isGrounded)
        {
            isGrounded = false;
            yMovement.y = jumpHeight * jumpScale * -gravity;
            jumpedCount++;
        }
        else
        {
            if (canMultiJump)
            {
                if(jumpedCount < maxMultiJump)
                {
                    // Perform normal jump
                    yMovement.y = jumpHeight * jumpScale * -gravity;
                    jumpedCount++;
                }
            }    
        }  
    }

    void HandleVisualRotation()
    {
        Vector3 visualForward = visualRoots.forward;
        // Handle Visual Rotation
        // Real player game object won't rotate
        if (moveInput != Vector3.zero)
        {
            visualForward = correctedMoveInput;
        }
        if (visualForward != Vector3.zero)
        {
            visualForward.y = 0f;
            Quaternion lookAtRotation = Quaternion.LookRotation(visualForward, Vector3.up);
            Quaternion LerpedRotation = Quaternion.Slerp(
                visualRoots.rotation, lookAtRotation, Time.deltaTime * visualRotationSpeed * visualRotationScale);
            visualRoots.rotation = LerpedRotation;
        }
    }

    // Draw debug shaped on selected
    private void OnDrawGizmosSelected()
    {
        if(isDrawGizmoSelected)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, groundCheckRadius);
        }
    }

    #region Utilities in class / access in ... menu of components
    [ContextMenu("Finder/AutoVisualAndMainCameraLink")]
    public void GetVisualAndMainCamera()
    {
        Debug.Log("Auto link Visual and MainCamera");
        referenceCamera = Camera.main.transform;
        visualRoots = transform.Find("Visual");
        if (visualRoots == null)
        {
            visualRoots = transform.Find("visual");
        }
    }

    [ContextMenu("Finder/Auto Link or Add Character Controller")]
    public void GetCharacterController()
    {
        Debug.Log("Auto Link CharacterController or Added if not existed");
        characterController = GetComponent<CharacterController>();
        if(characterController == null)
        {
            characterController= gameObject.AddComponent<CharacterController>();
        }
    }
    #endregion
}
