using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DetectHandleGrab : MonoBehaviour
{
    [SerializeField] public bool isLeftHandArea;
    [SerializeField] public string controllerPosition;
    [SerializeField] public bool isAttachedHandle;
    [SerializeField] public bool isGripHandle;

    private DetectPlayerInput playerInput;

    private void Awake()
    {
        playerInput = GameObject.FindWithTag("Player").GetComponent<DetectPlayerInput>();

        if (!isLeftHandArea)
            controllerPosition = "Right";
        else
            controllerPosition = "Left";
    }

    public void Update()
    {
        if (!isLeftHandArea && isAttachedHandle && playerInput.isRightGripPressed
            || isLeftHandArea && isAttachedHandle && playerInput.isLeftGripPressed)
            isGripHandle = true;
        else
            isGripHandle = false;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag != "RightController" && collider.transform.tag != "LeftController")
            return;

        if (!isLeftHandArea)
        {
            if (collider.transform.tag == "RightController")
            {
                isAttachedHandle = true;
            }
            else
                Debug.Log("Left Hand is in wrong position");
        }
        else
        {
            if (collider.transform.tag == "LeftController")
            {
                isAttachedHandle = true;
            }
            else
                Debug.Log("Right Hand is in wrong position");
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag != "RightController" && collider.transform.tag != "LeftController")
            return;

        isAttachedHandle = false;
        Debug.Log($"{controllerPosition} is in outside of handle area");
    }

}