using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGunGrabInteractable : XRGrabInteractable
{
    [SerializeField] Transform gunHandleAttach;
    [SerializeField] Transform gunBodyAttach;

    IXRSelectInteractor[] interactors;

    private void Start()
    {
        interactors = new IXRSelectInteractor[2];
        base.Awake();
    }

    //protected override void OnSelectEntered(SelectEnterEventArgs args)
    //{
    //    base.OnSelectEntered(args);
    //    SetPriority(args);
    //    SetFirstAttachPoint(args);
    //}

    public void SetPriority(SelectEnterEventArgs args)
    {
        if (interactors.Length >= 2)
            return;

        if (interactorsSelecting.Count <= 1 || ReferenceEquals(args.interactorObject, interactorsSelecting[0]))
        {
            interactors[0] = args.interactorObject;
        }
        else
        {
            interactors[1] = args.interactorObject;
        }
    }

    public override Transform GetAttachTransform(IXRInteractor interactor)
    {
        bool isFirst = interactorsSelecting.Count <= 1 || ReferenceEquals(interactor, interactorsSelecting[0]);
           
        if (!isFirst)
        {
            return secondaryAttachTransform;
        }

        return attachTransform != null ? attachTransform : base.GetAttachTransform(interactor);
    }

    public void SetFirstAttachPoint(SelectEnterEventArgs args)
    {
        if (attachTransform == gunBodyAttach && args.interactorObject == interactors[0])
        {
            attachTransform = gunHandleAttach;
            secondaryAttachTransform = gunBodyAttach;
        }
        else
            return;
    }

    //protected override void OnSelectExited(SelectExitEventArgs args)
    //{
    //    base.OnSelectExited(args);
    //    SetSecondAttachPoint(args);
    //}

    public void SetSecondAttachPoint(SelectExitEventArgs args)
    {
        if (attachTransform == gunHandleAttach && args.interactorObject == interactors[0])
        {
            attachTransform = interactors[1].transform;
            secondaryAttachTransform = null;
        }
        else
            return;
    }
}
