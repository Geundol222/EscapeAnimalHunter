using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSetHandTransform : MonoBehaviour
{
    [SerializeField] GameObject r_transform;
    [SerializeField] GameObject l_transform;

    [SerializeField] GameObject r_controllerModel;
    [SerializeField] GameObject l_controllerModel;

    [SerializeField] GameObject r_handleModel;
    [SerializeField] GameObject l_handleModel;

    [SerializeField] GameObject handleRotatePivot;

    DetectHandleGrab handleGrab;

    private void Awake()
    {
        handleGrab = GetComponent<DetectHandleGrab>();
    }

    private void Update()
    {
        if (handleGrab.isRightGrip && r_handleModel.transform.parent != handleRotatePivot.transform)
            ToHandleTransform(true);
        
        if (!handleGrab.isRightGrip && r_handleModel.transform.parent != r_transform)
            ToOriginTransform(true);

        if (handleGrab.isLeftGrip && l_handleModel.transform.parent != handleRotatePivot.transform)
            ToHandleTransform(false);

        if (!handleGrab.isLeftGrip && l_handleModel.transform.parent != l_transform)
            ToOriginTransform(false);
    }

    void ToOriginTransform(bool isRight)
    {
        if (isRight)
        {
            SetActiveTrue(true);
            r_handleModel.transform.SetParent(r_transform.transform);
            r_handleModel.transform.localPosition = Vector3.zero;
            r_handleModel.transform.localRotation = Quaternion.identity;
            SetActiveFalse(true);
        }
        else
        {
            SetActiveTrue(false);
            l_handleModel.transform.SetParent(l_transform.transform);
            l_handleModel.transform.localPosition = Vector3.zero;
            l_handleModel.transform.localRotation = Quaternion.identity;
            SetActiveFalse(false);
        }
    }

    void ToHandleTransform(bool isRight)
    {
        if (isRight)
        {
            SetActiveTrue(true);
            r_handleModel.transform.SetParent(handleRotatePivot.transform);
        }
        else
        {
            SetActiveTrue(false);
            l_handleModel.transform.SetParent(handleRotatePivot.transform);

        }
    }

    void SetActiveTrue(bool isRight)
    {
        if (isRight)
            r_handleModel.SetActive(true);
        else
            l_handleModel.SetActive(true);
    }

    void SetActiveFalse(bool isRight)
    {
        if (isRight)
            r_handleModel.SetActive(false);
        else
            l_handleModel.SetActive(false);
    }
}
