using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGunDualGrabInteractable : XRBaseInteractable
{
    #region Component
    private Rigidbody rigid;
    #endregion

    #region Inspector
    [SerializeField] Transform mainAttachTransform;
    [SerializeField] Transform subAttachTransform;
    #endregion

    public enum GrabState { None, Main, Sub, Multi }
    private GrabState state;

    private Pose mainPose;
    private Pose subPose;

    private IXRInteractor mainInteractor;
    private IXRInteractor subInteractor;

    protected override void Awake()
    {
        base.Awake();

        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        mainPose = mainAttachTransform.GetLocalPose();
        subPose = subAttachTransform.GetLocalPose();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        switch (state)
        {
            case GrabState.None:
                mainInteractor = args.interactorObject;
                state = GrabState.Main;
                OnGrab();
                break;
            case GrabState.Main:
                subInteractor = args.interactorObject;
                state = GrabState.Multi;
                break;
            case GrabState.Sub:
                mainInteractor = args.interactorObject;
                state = GrabState.Multi;
                break;
        }

        Debug.Log(state);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        if (ReferenceEquals(args.interactorObject, mainInteractor))
        {
            mainInteractor = default;
            switch (state)
            {
                case GrabState.Main:
                    state = GrabState.None;
                    OnDrop();
                    break;
                case GrabState.Multi:
                    state = GrabState.Sub;
                    break;
            }
        }
        else // if (ReferenceEquals(args.interactorObject, subInteractor))
        {
            subInteractor = default;
            switch (state)
            {
                case GrabState.Sub:
                    state = GrabState.None;
                    OnDrop();
                    break;
                case GrabState.Multi:
                    state = GrabState.Main;
                    break;
            }
        }

        Debug.Log(state);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!isSelected)
            return;

        switch (updatePhase)
        {
            case XRInteractionUpdateOrder.UpdatePhase.Dynamic:
            case XRInteractionUpdateOrder.UpdatePhase.OnBeforeRender:
                switch (state)
                {
                    case GrabState.Main:
                        MainGrabUpdate();
                        break;
                    case GrabState.Sub:
                        SubGrabUpdate();
                        break;
                    case GrabState.Multi:
                        MultiGrabUpdate();
                        break;
                }
                break;
            case XRInteractionUpdateOrder.UpdatePhase.Fixed:
                // TODO : Physics process
                break;
            case XRInteractionUpdateOrder.UpdatePhase.Late:
                // TODO : Late process
                break;
        }
    }

    private void MainGrabUpdate()
    {
        // TODO : 
        transform.SetPositionAndRotation(mainInteractor.transform.position, mainInteractor.transform.rotation);
    }

    private void SubGrabUpdate()
    {
        // TODO : 
        transform.SetPositionAndRotation(subInteractor.transform.position + new Vector3(1.0f, 0f, 0f), subInteractor.transform.rotation);
    }

    private void MultiGrabUpdate()
    {
        // TODO : 
        transform.SetPositionAndRotation(mainInteractor.transform.position, mainInteractor.transform.rotation);
    }


    bool oldKinematic;
    bool oldGravity;
    float oldDrag;
    float oldAngularDrag;
    private void OnGrab()
    {
        oldKinematic = rigid.isKinematic;
        oldGravity = rigid.useGravity;
        oldDrag = rigid.drag;
        oldAngularDrag = rigid.angularDrag;

        rigid.isKinematic = true;
        rigid.useGravity = false;
        rigid.drag = 0f;
        rigid.angularDrag = 0f;
    }

    private void OnDrop()
    {
        rigid.isKinematic = oldKinematic;
        rigid.useGravity = oldGravity;
        rigid.drag = oldDrag;
        rigid.angularDrag = oldAngularDrag;
    }

}
