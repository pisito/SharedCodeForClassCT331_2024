using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDoAction : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public string targetTag = "Player";
    public bool debug = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            if(debug) Debug.Log(name + ": Trigger Action from " + other.name);
            onTriggerEnter?.Invoke();
        }
    }
}
