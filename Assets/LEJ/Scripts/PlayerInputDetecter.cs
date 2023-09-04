using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDetecter : MonoBehaviour
{
    public bool isRightGripPressed;
    public bool isLeftGripPressed;
    public float rightJoyStickYValue;
    public float leftJoyStickYValue;

    public void OnRightGripPressed(InputValue value)
    {
        isRightGripPressed = value.isPressed;
    }

    public void OnLeftGripPressed(InputValue value)
    {
        isLeftGripPressed = value.isPressed;
    }

    public void OnRightJoyStick(InputValue value)
    {
        rightJoyStickYValue = Mathf.Clamp(value.Get<Vector2>().y, 0f, 1f);
    }

    public void OnLeftJoyStick(InputValue value)
    {
        leftJoyStickYValue = Mathf.Clamp(value.Get<Vector2>().y, 0f, 1f);
    }
}
