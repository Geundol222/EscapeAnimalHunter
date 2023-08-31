using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class DetectPlayerInput : MonoBehaviour
{
    [SerializeField] public bool isRightGripPressed;
    [SerializeField] public bool isLeftGripPressed;

    private void OnRightGripPressed(InputValue value)
    {
        if (value.isPressed)
            isRightGripPressed = true;
        else
            isRightGripPressed = false;
    }

    private void OnLeftGripPressed(InputValue value)
    {
        if (value.isPressed)
            isLeftGripPressed = true;
        else  
            isLeftGripPressed = false;
    }
}
