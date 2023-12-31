using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Interactions;

public class XRBoltInteractable : XRBaseInteractable
{
    [SerializeField] Transform boltTransform;
    [SerializeField] Transform attachPoint;
    [SerializeField] Transform originPosition;
    [SerializeField] Gun gun;
    [SerializeField] float maxAngle;
    [SerializeField] float minAngle;
    [SerializeField] float rotationSensitivity;

    IXRSelectInteractor selectInteractor;
    float currentAngle = 0.0f;
    int tapCount = 0;

    bool isOpen = false;

    /// <summary>
    /// OnSelectEntered, Init Valiable
    /// </summary>
    /// <param name="args"></param>
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        selectInteractor = args.interactorObject;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        selectInteractor = null;
    }

    /// <summary>
    /// Bolt Rotating Process (Update Routine)
    /// </summary>
    /// <param name="updatePhase"></param>
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                RotateBolt();

                if (boltTransform.localRotation.eulerAngles.z <= minAngle)
                {
                    boltTransform.localRotation = Quaternion.Euler(0, 0, minAngle);
                }

                if (boltTransform.localRotation.eulerAngles.z >= maxAngle)
                {
                    boltTransform.localRotation = Quaternion.Euler(0, 0, maxAngle);
                    Vector3 localAttachPoint = transform.InverseTransformPoint(attachPoint.position);
                    boltTransform.localPosition = new Vector3(boltTransform.localPosition.x, boltTransform.localPosition.y, localAttachPoint.z);
                    ReloadingProcess();
                }
            }
        }
    }

    /// <summary>
    /// Rotating Bolt to currentAngle
    /// </summary>
    private void RotateBolt()
    {
        attachPoint.position = selectInteractor.transform.position;

        currentAngle = FindBoltAngle();

        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

        if (currentAngle == minAngle)
        {
            if (isOpen)
            {
                GameManager.Sound.PlaySound("Bolt/BoltClose");
                isOpen = false;
            }
        }
        else if (currentAngle == maxAngle)
        {
            if (!isOpen)
            {
                GameManager.Sound.PlaySound("Bolt/BoltOpen");
                isOpen = true;
            }
        }

        //boltTransform.Rotate(new Vector3(0, 0, currentAngle));
        boltTransform.localEulerAngles = new Vector3(0, 0, currentAngle);
    }

    /// <summary>
    /// Finding Bolt Angles
    /// </summary>
    /// <returns> the float result between Bolt and Interactor </returns>
    private float FindBoltAngle()
    {
        float totalAngle = 0f;

        Vector2 direction = FindLocalPoint(attachPoint.position);

        totalAngle += ConvertToAngle(direction) * rotationSensitivity;

        Mathf.Clamp(totalAngle, minAngle, maxAngle);

        return totalAngle;
    }

    /// <summary>
    /// Convert Vector3 to Vector2 with position's localPosition
    /// </summary>
    /// <param name="position"></param>
    /// <returns> the Vector2, Vector3 localPosition converted to </returns>
    private Vector2 FindLocalPoint(Vector3 position)
    {
        Vector3 localPos3D = transform.InverseTransformPoint(position);
        Vector2 localPos2D = localPos3D;
        return localPos2D.normalized;
    }

    /// <summary>
    /// Compare the angle converted to Vector2 with the x-axis of Transform using SignedAngle
    /// </summary>
    /// <param name="direction"></param>
    /// <returns> the float Converted angle to Vector2 direction </returns>
    private float ConvertToAngle(Vector2 direction)
    {
        float angle = Vector2.SignedAngle(transform.InverseTransformDirection(transform.right), direction) > 0 ? Vector2.SignedAngle(transform.InverseTransformDirection(transform.right), direction) : 0;
        return angle;
    }

    private void ReloadingProcess()
    {
        Vector3 localAttach = transform.InverseTransformPoint(attachPoint.position);
        Vector3 localOrigin = boltTransform.InverseTransformPoint(originPosition.position);

        if (localAttach.z > localOrigin.z)
        {
            boltTransform.localPosition = localOrigin;

            if (tapCount == 1)
            {
                tapCount = 2;
                GameManager.Sound.PlaySound("Bolt/BoltPush");
            }
        }
        else if (localAttach.z < localOrigin.z)
        {
            boltTransform.localRotation = Quaternion.Euler(0, 0, maxAngle);

            if (localOrigin.z - localAttach.z >= 0.15f)
            {
                boltTransform.localPosition = new Vector3(boltTransform.localPosition.x, boltTransform.localPosition.y, localOrigin.z - 0.15f);

                if (tapCount == 0)
                {
                    tapCount = 1;
                    GameManager.Sound.PlaySound("Bolt/BoltPull");
                }
            }
        }

        CheckReloadComplete();
    }

    private void CheckReloadComplete()
    {
        if (tapCount == 2)
        {
            gun.IsShoot = false;
            tapCount = 0;
        }
        else
            return;
    }
}
