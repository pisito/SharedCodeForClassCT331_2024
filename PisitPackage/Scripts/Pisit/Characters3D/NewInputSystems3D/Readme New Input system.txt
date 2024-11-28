Window > Package Manager

Change the filter to [Unity Registry]

Search for Input Systems

Select [Install]

It will prompt to [Restart Unity Editor]

Restart*

Edit > Project Setting

Player > Other Settings > [Active Input Handling] -> Set to [Both] for now to use old and new input system together

Consider change it during build project to optimize

[Attach Player Input Component to your player]

Assign a script that implement On[Action] to process input signal

https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11/manual/PlayerInput.html.

public class MyPlayerScript : MonoBehaviour
{
    // "jump" action becomes "OnJump" method.

    // If you're not interested in the value from the control that triggers the action, use a method without arguments.
    public void OnJump()
    {
        // your Jump code here
    }

    // If you are interested in the value from the control that triggers an action, you can declare a parameter of type InputValue.
    public void OnMove(InputValue value)
    {
        // Read value from control. The type depends on what type of controls.
        // the action is bound to.
        var v = value.Get<Vector2>();

        // IMPORTANT:
        // The given InputValue is only valid for the duration of the callback. Storing the InputValue references somewhere and calling Get<T>() later does not work correctly.
    }
}

