using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit;

public class XRDualGrabInteractable : XRGrabInteractable
{
    public Transform gunHandleAttach;
    public Transform gunBodyAttach;
    public IXRSelectInteractor[] interactors;

    bool firstAttach = false;
    bool secondAttach = false;

    protected override void Awake()
    {
        interactors = new IXRSelectInteractor[2];
        base.Awake();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        SetPriority(args);
        SetFirstAttachPoint(args);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
    }

    public void SetPriority(SelectEnterEventArgs args)
    {
        if (interactors[1] != null)
            return;

        if (interactorsSelecting.Count <= 1 || ReferenceEquals(args.interactorObject, interactorsSelecting[0]))
        {
            interactors[0] = args.interactorObject;
            firstAttach = true;
        }
        else
        {
            interactors[1] = args.interactorObject;
            secondAttach = true;
        }
    }

    public void SetFirstAttachPoint(SelectEnterEventArgs args)
    {
        attachTransform = gunHandleAttach;

        if (interactors[1] != null)
            secondaryAttachTransform = interactors[1].transform;

        if (args.interactorObject == interactors[0])
        {
            interactorsSelecting[0] = interactors[0];
            if (interactorsSelecting.Count >= 2 && interactors[1] != null)
                interactorsSelecting[1] = interactors[1];

            firstAttach = true;
        }
        else if (args.interactorObject == interactors[1])
        {
            secondAttach = true;
        }
        else
            return;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        SetSecondAttachPoint(args);
        CheckAttachAll();
    }

    public void SetSecondAttachPoint(SelectExitEventArgs args)
    {
        if (args.interactorObject == interactors[0])
            firstAttach = false;
        else if (args.interactorObject == interactors[1])
            secondAttach = false;

        if (!secondAttach)
            return;

        if (interactors[1] != null && args.interactorObject == interactors[0])
        {
            attachTransform = interactors[1].transform;
        }
        else
            return;
    }

    public void CheckAttachAll()
    {
        if (interactorsSelecting.Count > 0)
            return;

        if (interactorsSelecting.Count == 0)
        {
            for (int i = 0; i < interactors.Length; i++)
            {
                interactors[i] = null;
            }
        }
    }
}
