using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class XRBoltInteractable : XRBaseInteractable
{
    [SerializeField] Transform boltTransform;

    public UnityEvent<float> OnBoltRotated;

    IXRSelectInteractor selectInteractor;
    float currentAngle = 0.0f;
    float angleDifference;

    private void Start()
    {
        Mathf.Clamp(-angleDifference, 0f, 70f);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        selectInteractor = args.interactorObject;
        currentAngle = FindBoltAngle();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        selectInteractor = null;
        currentAngle = FindBoltAngle();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                RotateBolt();
                if (-angleDifference >= 70f)
                    boltTransform.position = new Vector3(boltTransform.position.x, boltTransform.position.y, selectInteractor.transform.position.z);
            }
        }
    }

    private void RotateBolt()
    {
        float totalAngle = FindBoltAngle();

        angleDifference = currentAngle - totalAngle;
        
        boltTransform.Rotate(transform.forward, -angleDifference);

        currentAngle = totalAngle;
        OnBoltRotated?.Invoke(angleDifference);
    }

    private float FindBoltAngle()
    {
        float totalAngle = 0f;

        foreach (IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        return transform.InverseTransformPoint(position).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        return Vector2.SignedAngle(transform.up, direction);
    }

    private float FindRotationSensitivity()
    {
        return 1.0f / interactorsSelecting.Count;
    }
}
