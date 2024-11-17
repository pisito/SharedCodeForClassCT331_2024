using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_InputMovement
{
    public Vector3 MoveInput { get; set; }

    public bool IsJumpPressed { get; set; }

    public bool IsSprintPressed { get; set; }


}
