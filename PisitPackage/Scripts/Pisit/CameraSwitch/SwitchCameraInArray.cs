using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraInArray : MonoBehaviour
{
    // List of objects to toggle
    public Transform[] objectsToToggle;

    // Key to toggle to the next object
    public KeyCode toggleKey = KeyCode.T;

    private int currentIndex = 0; // Tracks the currently enabled object's index

    // Start is called before the first frame update
    void Start()
    {
        // Initialize by disabling all objects except the first one
        UpdateActiveObject();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            // Move to the next index in the list
            currentIndex = (currentIndex + 1) % objectsToToggle.Length;

            // Update the active object
            UpdateActiveObject();
        }
    }

    void UpdateActiveObject()
    {
        // Ensure all objects are toggled off
        for (int i = 0; i < objectsToToggle.Length; i++)
        {
            if (objectsToToggle[i] != null)
            {
                // Idea is if the object is equal to index, = true (active), otherwise, false (deactive)
                objectsToToggle[i].gameObject.SetActive(i == currentIndex);
            }
        }
    }
}
