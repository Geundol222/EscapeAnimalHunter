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

                if (boltTransform.rotation.eulerAngles.z <= minAngle)
                {
                    boltTransform.rotation = Quaternion.Euler(0, 0, minAngle);
                }

                if (boltTransform.rotation.eulerAngles.z >= maxAngle)
                {
                    boltTransform.rotation = Quaternion.Euler(0, 0, maxAngle);
                    boltTransform.position = new Vector3(boltTransform.position.x, boltTransform.position.y, attachPoint.position.z);
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
        Mathf.Clamp(currentAngle, minAngle, maxAngle);

        boltTransform.localEulerAngles = new Vector3(0, 0, currentAngle);
    }

    private float FindBoltAngle()
    {
        float totalAngle = 0f;

        Vector2 direction = FindLocalPoint(attachPoint.position);

        totalAngle += ConvertToAngle(direction) * rotationSensitivity;

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
        return Vector2.SignedAngle(boltTransform.right, direction);
    }

    private void ReloadingProcess()
    {
        if (attachPoint.position.z > originPosition.z)
        {
            boltTransform.position = originPosition;
        }
        else if (attachPoint.position.z < originPosition.z)
        {
            if (originPosition.z - attachPoint.position.z >= 0.4f)
            {
                boltTransform.position = new Vector3(boltTransform.position.x, boltTransform.position.y, originPosition.z - 0.4f);
                gun.IsShoot = false;
            }
        }
    }
}
