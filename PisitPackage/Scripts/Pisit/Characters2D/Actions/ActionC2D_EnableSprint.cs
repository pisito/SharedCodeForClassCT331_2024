using Pisit.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class ActionC2D_EnableSprint : MonoBehaviour
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

    public void EnablePlayerToSprint()
    {
        if (debug) Debug.Log("Enable player to sprint");
        
        playerController2D.canSprint = true; // enable sprint

        afterActionEvents?.Invoke();
    }

    public void DestroyMySelf()
    {
        Destroy(gameObject, 0.2f); //2 milli seconds
    }
}
