using UnityEngine;

public class TriggerToggle : MonoBehaviour
{
    // The object to toggle
    public GameObject targetObject;

    // Optional: Tag filter to restrict which objects can trigger the toggle
    public string triggerTag = "";

    private void OnTriggerEnter(Collider other)
    {
        // Check if a tag filter is specified and matches the triggering object
        if (string.IsNullOrEmpty(triggerTag) || other.CompareTag(triggerTag))
        {
            // Toggle the active state of the target object
            if (targetObject != null)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }
        }
    }
}
