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
    [SerializeField] Gun gun;
    [SerializeField] float maxAngle;
    [SerializeField] float minAngle;
    [SerializeField] float rotationSensitivity;

    IXRSelectInteractor selectInteractor;
    float currentAngle = 0.0f;
    int tapCount = 0;

    Vector3 originPosition;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        selectInteractor = args.interactorObject;
        originPosition = boltTransform.position;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        selectInteractor = null;
    }

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

    private void RotateBolt()
    {
        attachPoint.position = selectInteractor.transform.position;

        currentAngle = FindBoltAngle();

        //boltTransform.Rotate(new Vector3(0, 0, currentAngle));
        boltTransform.localEulerAngles = new Vector3(0, 0, currentAngle);
    }

    private float FindBoltAngle()
    {
        float totalAngle = 0f;

        Vector2 direction = FindLocalPoint(attachPoint.position);

        totalAngle += ConvertToAngle(direction) * rotationSensitivity;

        Mathf.Clamp(totalAngle, minAngle, maxAngle);

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        Vector3 localPos3D = transform.InverseTransformPoint(position);
        Vector2 localPos2D = localPos3D;
        return localPos2D.normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        float angle = Vector2.SignedAngle(transform.InverseTransformDirection(transform.right), direction) > 0 ? Vector2.SignedAngle(transform.InverseTransformDirection(transform.right), direction) : 0;
        return angle;
    }

    private void ReloadingProcess()
    {
        Vector3 localAttach = transform.InverseTransformPoint(attachPoint.position);
        Vector3 localOrigin = boltTransform.InverseTransformPoint(originPosition);

        if (localAttach.z > localOrigin.z)
        {
            boltTransform.localPosition = localOrigin;

            if (tapCount == 1)
                tapCount = 2;
        }
        else if (localAttach.z < localOrigin.z)
        {
            boltTransform.localRotation = Quaternion.Euler(0, 0, maxAngle);

            if (localOrigin.z - localAttach.z >= 0.15f)
            {
                boltTransform.localPosition = new Vector3(boltTransform.localPosition.x, boltTransform.localPosition.y, localOrigin.z - 0.15f);

                if (tapCount == 0)
                    tapCount = 1;
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
