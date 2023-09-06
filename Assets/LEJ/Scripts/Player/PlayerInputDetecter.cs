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
        rightJoyStickYValue = value.Get<Vector2>().y;
    }

    public void OnLeftJoyStick(InputValue value)
    {
        leftJoyStickYValue = value.Get<Vector2>().y;
    }
}
