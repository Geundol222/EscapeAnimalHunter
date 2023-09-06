using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Interaction.Toolkit.Transformers.XRGeneralGrabTransformer;

public class XRGunDualGrabInteractable : XRBaseInteractable
{
    #region Component
    private Rigidbody rigid;
    #endregion

    #region Inspector
    [SerializeField] Transform mainAttachTransform;
    [SerializeField] Transform subAttachTransform;
    [SerializeField] TwoHandedRotationMode twoHandedRotationMode;
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
        Transform interactorAttachTransform = mainInteractor.GetAttachTransform(this);
        Pose interactorAttachPose = new Pose(interactorAttachTransform.position, interactorAttachTransform.rotation);
        Pose thisTransformPose = new Pose(transform.position, transform.rotation);

        // Calculate offset of the grab interactable's position relative to its attach transform
        var attachOffset = thisTransformPose.position - mainAttachTransform.position;

        // Compute the new target world pose
        // Transform that offset direction from world space to local space of the transform it's relative to.
        // It will be applied to the interactor's attach position using the orientation of the Interactor's attach transform.
        var positionOffset = mainAttachTransform.InverseTransformDirection(attachOffset);
        var rotationOffset = Quaternion.Inverse(Quaternion.Inverse(thisTransformPose.rotation) * mainAttachTransform.rotation);

        transform.position = (interactorAttachPose.rotation * positionOffset) + interactorAttachPose.position;
        transform.rotation = (interactorAttachPose.rotation * rotationOffset);
    }

    private void SubGrabUpdate()
    {
        Transform interactorAttachTransform = subInteractor.GetAttachTransform(this);
        Pose interactorAttachPose = new Pose(interactorAttachTransform.position, interactorAttachTransform.rotation);
        Pose thisTransformPose = new Pose(transform.position, transform.rotation);

        // Calculate offset of the grab interactable's position relative to its attach transform
        var attachOffset = thisTransformPose.position - subAttachTransform.position;

        // Compute the new target world pose
        // Transform that offset direction from world space to local space of the transform it's relative to.
        // It will be applied to the interactor's attach position using the orientation of the Interactor's attach transform.
        var positionOffset = subAttachTransform.InverseTransformDirection(attachOffset);
        var rotationOffset = Quaternion.Inverse(Quaternion.Inverse(thisTransformPose.rotation) * subAttachTransform.rotation);

        transform.position = (interactorAttachPose.rotation * positionOffset) + interactorAttachPose.position;
        transform.rotation = (interactorAttachPose.rotation * rotationOffset);
    }

    private void MultiGrabUpdate()
    {
        var interactor0 = interactorsSelecting[0];
        var interactor1 = interactorsSelecting[1];

        var interactor0Transform = interactor0.GetAttachTransform(this);
        var interactor1Transform = interactor1.GetAttachTransform(this);

        Vector3 newHandleBar = interactor0Transform.InverseTransformPoint(interactor1Transform.position);

        Quaternion newRotation;
        if (twoHandedRotationMode == TwoHandedRotationMode.FirstHandDirectedTowardsSecondHand)
        {
            newRotation = interactor0Transform.rotation * Quaternion.FromToRotation(m_StartHandleBar, newHandleBar);
        }

        /*
         * XRGeneralGrabTransformer->ComputeAdjustedInteractorPose È®ÀÎ
         * 
        if (interactorsSelecting.Count == 1 || twoHandedRotationMode == TwoHandedRotationMode.FirstHandOnly)
        {
            newHandleBar = m_StartHandleBar;
            return grabInteractable.interactorsSelecting[0].GetAttachTransform(grabInteractable).GetWorldPose();
        }

        if (grabInteractable.interactorsSelecting.Count > 1)
        {
            var interactor0 = grabInteractable.interactorsSelecting[0];
            var interactor1 = grabInteractable.interactorsSelecting[1];

            var interactor0Transform = interactor0.GetAttachTransform(grabInteractable);
            var interactor1Transform = interactor1.GetAttachTransform(grabInteractable);

            newHandleBar = interactor0Transform.InverseTransformPoint(interactor1Transform.position);

            Quaternion newRotation;
            if (m_TwoHandedRotationMode == TwoHandedRotationMode.FirstHandDirectedTowardsSecondHand)
            {
                newRotation = interactor0Transform.rotation * Quaternion.FromToRotation(m_StartHandleBar, newHandleBar);
            }
            else if (m_TwoHandedRotationMode == TwoHandedRotationMode.TwoHandedAverage)
            {
                var forward = (interactor1Transform.position - interactor0Transform.position).normalized;

                var averageRight = Vector3.Slerp(interactor0Transform.right, interactor1Transform.right, 0.5f);
                var up = Vector3.Slerp(interactor0Transform.up, interactor1Transform.up, 0.5f);

                var crossUp = Vector3.Cross(forward, averageRight);
                var angleDiff = Mathf.PingPong(Vector3.Angle(up, forward), 90f);
                up = Vector3.Slerp(crossUp, up, angleDiff / 90f);

                var crossRight = Vector3.Cross(up, forward);
                up = Vector3.Cross(forward, crossRight);

                if (m_FirstFrameSinceTwoHandedGrab)
                {
                    m_FirstFrameSinceTwoHandedGrab = false;
                }
                else
                {
                    // We also keep track of whether the up vector was pointing up or down previously, to allow for objects to be flipped through a series of rotations
                    // Such as a 180 degree rotation on the y, followed by a 180 degree rotation on the x
                    if (Vector3.Dot(up, m_LastTwoHandedUp) <= 0f)
                    {
                        up = -up;
                    }
                }

                m_LastTwoHandedUp = up;

                var twoHandedRotation = Quaternion.LookRotation(forward, up);

                // Given that this rotation method doesn't really consider the first interactor's start rotation, we have to remove the offset pose computed on grab. 
                newRotation = twoHandedRotation * Quaternion.Inverse(m_OffsetPose.rotation);
            }
            else
            {
                newRotation = interactor0Transform.rotation;
            }

            return new Pose(interactor0Transform.position, newRotation);
        }

        newHandleBar = m_StartHandleBar;
        return Pose.identity;*/
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