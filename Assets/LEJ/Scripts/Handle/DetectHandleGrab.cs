using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DetectHandleGrab : MonoBehaviour
{
    [SerializeField] private GameObject rightControllerModel;
    [SerializeField] private GameObject leftControllerModel;

    [SerializeField] private GameObject rightHandleHand;
    [SerializeField] private GameObject leftHandleHand;

    private bool isRightAttach;
    private bool isLeftAttach;

    public bool isRightGrip;
    public bool isLeftGrip;

    private PlayerInputDetecter playerInput;

    Vector3 r_model_pos = new Vector3(-0.109752f, 0.557918f, 0.1156947f);
    Vector3 r_model_rot = new Vector3(-193.233f, 184.919f, -270.367f);

    Vector3 l_model_pos = new Vector3(-0.12f, -0.239f, -0.223f);
    Vector3 l_model_rot = new Vector3(56.832f, 269.491f, 272.294f);

    private void Awake()
    {
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInputDetecter>();
        
        rightControllerModel.SetActive(false);
        leftControllerModel.SetActive(false);
    }

    private void Update()
    {
        if (isRightAttach && playerInput.isRightGripPressed)
            isRightGrip = true;
        else
            isRightGrip = false;

        if (isLeftAttach && playerInput.isLeftGripPressed)
            isLeftGrip = true;
        else
            isLeftGrip = false;
        
        if (isRightGrip)
        {
            rightControllerModel.SetActive(false);
            rightHandleHand.SetActive(true);
        }

        if (!isRightGrip)
        {
            if (rightHandleHand.activeInHierarchy)
            {
                //SetOriginPos(true);
                rightControllerModel.SetActive(true);
                rightHandleHand.SetActive(false);
            }
        }

        if (isLeftGrip)
        {
            leftControllerModel.SetActive(false);
            leftHandleHand.SetActive(true);
        }
        if (!isLeftGrip)
        {
            if (leftHandleHand.activeInHierarchy)
            {
                //SetOriginPos(false);
                leftControllerModel.SetActive(true);
                leftHandleHand.SetActive(false);
            }
        }
        
    }

    void SetOriginPos(bool isRight)
    {
        if (isRight)
        {
            rightControllerModel.transform.localPosition = r_model_pos;
            rightControllerModel.transform.localRotation = Quaternion.Euler(r_model_rot);
        }
        else
        {
            leftControllerModel.transform.localPosition = l_model_pos;
            leftControllerModel.transform.localRotation = Quaternion.Euler(l_model_rot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightController")
            isRightAttach = true;

        if (other.tag == "LeftController")
            isLeftAttach = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RightController")
            isRightAttach = false;
        if (other.tag == "LeftController")
            isLeftAttach = false;
    }
}
