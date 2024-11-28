using Pisit.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionC2D_AddMaxJump : MonoBehaviour
{
    public CharacterController2D playerController2D;
    public UnityEvent afterActionEvents;
    public bool debug = false;

    private void Start()
    {
        if (playerController2D == null)
        {
            GameObject foundObject = GameObject.FindGameObjectWithTag("Player");
            playerController2D = foundObject.GetComponent<CharacterController2D>();
        }
    }

    public void AddMaxJumpCount(int value)
    {
        if (debug) Debug.Log("Add Max Jump to the player by " + value); ;
        
        playerController2D.multiJumpMax += value;

        afterActionEvents?.Invoke();
    }

    public void DestroyMySelf()
    {
        Destroy(gameObject, 0.2f); //2 milli seconds
    }
}
