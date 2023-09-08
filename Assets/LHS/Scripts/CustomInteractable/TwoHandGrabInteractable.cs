using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteractable : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
    public enum TwoHandRotationType { None, First, Second };
    public TwoHandRotationType twoHandRotationType;
    public bool snapToSecondHand = true;

    private IXRSelectInteractor secondInteractor;
    private Quaternion attachInitialRotation;
    private Quaternion initialRotationOffset;

    private void Start()
    {
        foreach (var item in secondHandGrabPoints)
        {
            item.selectEntered.AddListener(OnSecondHandGrab);
            item.selectExited.AddListener(OnSecondHandRelease);
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(secondInteractor != null && interactorsSelecting[0] != null)
        {
            // Comput the rotation
            if (snapToSecondHand)
                interactorsSelecting[0].transform.rotation = GetTwoHandRotation();
            else
                interactorsSelecting[0].transform.rotation = GetTwoHandRotation() * initialRotationOffset;
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;
        if (twoHandRotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.transform.position - interactorsSelecting[0].transform.position);
        }
        else if (twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.transform.position - interactorsSelecting[0].transform.position, interactorsSelecting[0].transform.up);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.transform.position - interactorsSelecting[0].transform.position, secondInteractor.transform.up);
        }

        return targetRotation;
    }

    public void OnSecondHandGrab(SelectEnterEventArgs args)
    {
        secondInteractor = args.interactorObject;
        initialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * interactorsSelecting[0].transform.rotation;
    }

    public void OnSecondHandRelease(SelectExitEventArgs args)
    {
        secondInteractor = null;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        attachInitialRotation = args.interactorObject.transform.localRotation;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        secondInteractor = null;
        args.interactorObject.transform.localRotation = attachInitialRotation;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor);
    }
}
