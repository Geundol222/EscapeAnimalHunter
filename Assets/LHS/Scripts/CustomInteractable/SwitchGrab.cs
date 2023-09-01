using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchGrab : MonoBehaviour
{
    XRGrabInteractable grabInteractable;

    [SerializeField] Transform gunBodyAttach;
    [SerializeField] Transform gunHandleAttach;

    List<IXRSelectInteractor> interactors;

    private void Awake()
    {
        interactors = new List<IXRSelectInteractor>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void SetPriority(SelectEnterEventArgs args)
    {
        if (grabInteractable.interactorsSelecting.Count <= 1 || ReferenceEquals(args.interactorObject, grabInteractable.interactorsSelecting[0]))
        {
            interactors[0] = args.interactorObject;
        }
        else
        {
            interactors.Add(args.interactorObject);
        }
    }
}
