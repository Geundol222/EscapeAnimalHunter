using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDetecter : MonoBehaviour
{
    public bool isRightTriggerPressed;
    public bool isLeftTriggerPressed;
    public float rightJoyStickYValue;

    public void OnRightTriggerPressed(InputValue value)
    {
        isRightTriggerPressed = value.isPressed;
    }

    public void OnLeftTriggerPressed(InputValue value)
    {
        isLeftTriggerPressed = value.isPressed;
    }

    public void OnRightJoyStick(InputValue value)
    {
        rightJoyStickYValue = value.Get<Vector2>().y;
    }
}
