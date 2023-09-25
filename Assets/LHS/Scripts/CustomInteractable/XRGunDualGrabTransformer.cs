using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class XRGunDualGrabTransformer : XRBaseGrabTransformer
{
    public enum PoseContributor
    {
        /// <summary>
        /// Use the first interactor's data.
        /// </summary>
        First,

        /// <summary>
        /// Use the second interactor's data.
        /// </summary>
        Second,

        /// <summary>
        /// Use an average of the first and second interactor's data.
        /// </summary>
        Average,
    }

    [SerializeField]
    PoseContributor m_MultiSelectPosition = PoseContributor.First;

    /// <summary>
    /// Controls how multiple interactors combine to drive this interactable's position
    /// </summary>
    /// <seealso cref="PoseContributor"/>
    public PoseContributor multiSelectPosition
    {
        get => m_MultiSelectPosition;
        set => m_MultiSelectPosition = value;
    }

    [SerializeField]
    PoseContributor m_MultiSelectRotation = PoseContributor.Average;

    /// <summary>
    /// Controls how multiple interactors combine to drive this interactable's rotation
    /// </summary>
    /// <seealso cref="PoseContributor"/>
    public PoseContributor multiSelectRotation
    {
        get => m_MultiSelectRotation;
        set => m_MultiSelectRotation = value;
    }

    /// <inheritdoc />
    protected override RegistrationMode registrationMode => RegistrationMode.Multiple;

    // For Gizmo
    internal Pose lastInteractorAttachPose { get; private set; }

    Vector3 m_LastUp;

    public enum PoseState
    {
        Main, Sub, Multi
    }

    [SerializeField] Transform gripTransform;

    [SerializeField] GameObject controllerLeftHand;
    [SerializeField] GameObject controllerRightHand;

    [SerializeField] GameObject mainGunRightHand;
    [SerializeField] GameObject subGunLeftHand;
    [SerializeField] GameObject mainGunLeftHand;
    [SerializeField] GameObject subGunRightHand;

    private PoseState state;
    private IXRInteractor mainInteractor;
    private IXRInteractor subInteractor;

    private void Awake()
    {
        mainGunRightHand.SetActive(false);
        mainGunLeftHand.SetActive(false);
        subGunRightHand.SetActive(false);
        subGunLeftHand.SetActive(false);
    }

    /// <summary>
    /// 맨 처음 Grab될 때, mainInteractor를 grabInteractable의 firstInteractorSelecting으로 지정
    /// </summary>
    /// <param name="grabInteractable"></param>
    public override void OnGrab(XRGrabInteractable grabInteractable)
    {
        base.OnGrab(grabInteractable);

        mainInteractor = grabInteractable.firstInteractorSelecting;

        grabInteractable.lastSelectExited.AddListener(OnDrop);
    }

    /// <summary>
    /// 총이 손에서 완전이 떨어질 때 grabInteractable의 attachTransform을 원래의 transform으로 교체
    /// </summary>
    /// <param name="args"></param>
    public void OnDrop(SelectExitEventArgs args)
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        mainGunLeftHand.SetActive(false);
        subGunRightHand.SetActive(false);
        mainGunRightHand.SetActive(false);
        subGunLeftHand.SetActive(false);

        controllerRightHand.SetActive(true);
        controllerLeftHand.SetActive(true);

        grabInteractable.attachTransform = gripTransform;
        args.interactableObject.lastSelectExited.RemoveListener(OnDrop);
    }

    /// <summary>
    /// grab하는 손이 추가 삭제 될 때 호출, 조건에 따라 PoseState의 state 변경
    /// </summary>
    /// <param name="grabInteractable"></param>
    /// <param name="targetPose"></param>
    /// <param name="localScale"></param>
    public override void OnGrabCountChanged(XRGrabInteractable grabInteractable, Pose targetPose, Vector3 localScale)
    {
        if (grabInteractable.interactorsSelecting.Count == 1)
        {
            grabInteractable.ClearMultipleGrabTransformers();

            if (ReferenceEquals(mainInteractor, grabInteractable.interactorsSelecting[0]))
            {
                mainInteractor = grabInteractable.interactorsSelecting[0];
                subInteractor = default;
                state = PoseState.Main;
            }
            else
            {
                mainInteractor = default;
                subInteractor = grabInteractable.interactorsSelecting[0];
                state = PoseState.Sub;
            }
        }
        else if (grabInteractable.interactorsSelecting.Count == 2)
        {
            grabInteractable.AddMultipleGrabTransformer(this);

            if (ReferenceEquals(mainInteractor, grabInteractable.interactorsSelecting[0]))
            {
                mainInteractor = grabInteractable.interactorsSelecting[0];
                subInteractor = grabInteractable.interactorsSelecting[1];
            }
            else
            {
                mainInteractor = grabInteractable.interactorsSelecting[1];
                subInteractor = grabInteractable.interactorsSelecting[0];
            }
            state = PoseState.Multi;
        }
    }

    /// <summary>
    /// Grab Processing (Update Routine)
    /// </summary>
    /// <param name="grabInteractable"></param>
    /// <param name="updatePhase"></param>
    /// <param name="targetPose"></param>
    /// <param name="localScale"></param>
    public override void Process(XRGrabInteractable grabInteractable, XRInteractionUpdateOrder.UpdatePhase updatePhase, ref Pose targetPose, ref Vector3 localScale)
    {
        switch (updatePhase)
        {
            case XRInteractionUpdateOrder.UpdatePhase.Dynamic:
            case XRInteractionUpdateOrder.UpdatePhase.OnBeforeRender:
                UpdateTarget(grabInteractable, ref targetPose);
                break;
        }
    }

    void UpdateTarget(XRGrabInteractable grabInteractable, ref Pose targetPose)
    {
        switch (state)
        {
            case PoseState.Main:
                UpdateTargetMain(grabInteractable, ref targetPose);
                break;
            case PoseState.Sub:
                UpdateTargetSub(grabInteractable, ref targetPose);
                break;
            case PoseState.Multi:
                UpdateTargetMulti(grabInteractable, ref targetPose);
                break;
            default:
                break;
        }
    }

    private void UpdateTargetMain(XRGrabInteractable grabInteractable, ref Pose targetPose)
    {
        if (mainInteractor.transform.gameObject.name == "Left Direct Interactor")
        {
            mainGunLeftHand.SetActive(true);
            subGunRightHand.SetActive(false);

            controllerLeftHand.SetActive(false);
            controllerRightHand.SetActive(true);
        }
        else
        {
            mainGunRightHand.SetActive(true);
            subGunLeftHand.SetActive(false);

            controllerLeftHand.SetActive(true);
            controllerRightHand.SetActive(false);
        }

        Transform interactorAttachTransform = mainInteractor.GetAttachTransform(grabInteractable);
        Pose interactorAttachPose = new Pose(interactorAttachTransform.position, interactorAttachTransform.rotation);
        Pose thisTransformPose = new Pose(transform.position, transform.rotation);

        // Calculate offset of the grab interactable's position relative to its attach transform
        var attachOffset = thisTransformPose.position - grabInteractable.attachTransform.position;

        // Compute the new target world pose
        // Transform that offset direction from world space to local space of the transform it's relative to.
        // It will be applied to the interactor's attach position using the orientation of the Interactor's attach transform.
        var positionOffset = grabInteractable.attachTransform.InverseTransformDirection(attachOffset);
        var rotationOffset = Quaternion.Inverse(Quaternion.Inverse(thisTransformPose.rotation) * grabInteractable.attachTransform.rotation);

        targetPose.position = (interactorAttachPose.rotation * positionOffset) + interactorAttachPose.position;
        targetPose.rotation = (interactorAttachPose.rotation * rotationOffset);
    }

    private void UpdateTargetSub(XRGrabInteractable grabInteractable, ref Pose targetPose)
    {
        if (subInteractor.transform.gameObject.name == "Right Direct Interactorr")
        {
            mainGunLeftHand.SetActive(false);
            subGunRightHand.SetActive(true);

            controllerLeftHand.SetActive(true);
            controllerRightHand.SetActive(false);
        }
        else
        {
            mainGunRightHand.SetActive(false);
            subGunLeftHand.SetActive(true);

            controllerLeftHand.SetActive(false);
            controllerRightHand.SetActive(true);
        }

        grabInteractable.attachTransform = subInteractor.transform;

        Transform interactorAttachTransform = grabInteractable.attachTransform;
        Pose interactorAttachPose = new Pose(interactorAttachTransform.position, grabInteractable.attachTransform.rotation);
        Pose thisTransformPose = new Pose(transform.position, transform.rotation);

        // Calculate offset of the grab interactable's position relative to its attach transform
        var attachOffset = thisTransformPose.position - grabInteractable.secondaryAttachTransform.position;

        // Compute the new target world pose
        // Transform that offset direction from world space to local space of the transform it's relative to.
        // It will be applied to the interactor's attach position using the orientation of the Interactor's attach transform.
        var positionOffset = grabInteractable.secondaryAttachTransform.InverseTransformDirection(attachOffset);
        var rotationOffset = Quaternion.Inverse(Quaternion.Inverse(thisTransformPose.rotation) * grabInteractable.secondaryAttachTransform.rotation);

        targetPose.position = (interactorAttachPose.rotation * positionOffset) + interactorAttachPose.position;
        targetPose.rotation = (interactorAttachPose.rotation * rotationOffset);
    }

    void UpdateTargetMulti(XRGrabInteractable grabInteractable, ref Pose targetPose)
    {
        if (mainInteractor.transform.gameObject.name == "Left Direct Interactor")
        {
            mainGunLeftHand.SetActive(true);
            subGunRightHand.SetActive(true);
        }
        else
        {
            mainGunRightHand.SetActive(true);
            subGunLeftHand.SetActive(true);
        }

        controllerLeftHand.SetActive(false);
        controllerRightHand.SetActive(false);

        grabInteractable.attachTransform = gripTransform;

        Transform primaryAttachTransform = mainInteractor.GetAttachTransform(grabInteractable);
        Pose primaryAttachPose = new Pose(primaryAttachTransform.position, primaryAttachTransform.rotation);
        Transform secondaryAttachTransform = subInteractor.GetAttachTransform(grabInteractable);
        Pose secondaryAttachPose = new Pose(secondaryAttachTransform.position, secondaryAttachTransform.rotation);

        var avrAttachPositionOffset = transform.position - grabInteractable.attachTransform.position;
        var avrAttachRotationOffset = Quaternion.Inverse(Quaternion.Inverse(grabInteractable.secondaryAttachTransform.rotation) * grabInteractable.attachTransform.rotation);
        var interactorAttachPose = primaryAttachPose;

        switch (m_MultiSelectPosition)
        {
            case PoseContributor.First:
                interactorAttachPose.position = primaryAttachPose.position;
                break;
            case PoseContributor.Second:
                interactorAttachPose.position = secondaryAttachPose.position;
                break;
            case PoseContributor.Average:
                interactorAttachPose.position = (primaryAttachPose.position + secondaryAttachPose.position) * 0.5f;
                break;
            default:
                Assert.IsTrue(false, $"Unhandled {nameof(PoseContributor)}={m_MultiSelectPosition}.");
                goto case PoseContributor.First;
        }

        // For rotation, we match the anchor's forward to the vector made by the two interactor positions - imagine a hammer handle.
        // We use the interactor's up as the base of the combined multi-select up, unless it is too similar to the forward vector
        // In that case, we will gradually fall back to the right vector and calculate the final 'up' from that
        var forward = (secondaryAttachPose.position - primaryAttachPose.position).normalized;

        Vector3 up;
        Vector3 right;
        switch (m_MultiSelectRotation)
        {
            case PoseContributor.First:
                up = primaryAttachPose.up;
                right = primaryAttachPose.right;
                if (forward == Vector3.zero)
                    forward = primaryAttachPose.forward;
                break;
            case PoseContributor.Second:
                up = secondaryAttachPose.up;
                right = secondaryAttachPose.right;
                if (forward == Vector3.zero)
                    forward = secondaryAttachPose.forward;
                break;
            case PoseContributor.Average:
                up = Vector3.Slerp(primaryAttachPose.up, secondaryAttachPose.up, 0.5f);
                right = Vector3.Slerp(primaryAttachPose.right, secondaryAttachPose.right, 0.5f);
                if (forward == Vector3.zero)
                    forward = primaryAttachPose.forward;
                break;
            default:
                Assert.IsTrue(false, $"Unhandled {nameof(PoseContributor)}={m_MultiSelectRotation}.");
                goto case PoseContributor.First;
        }

        var crossUp = Vector3.Cross(forward, right);

        var angleDiff = Mathf.PingPong(Vector3.Angle(up, forward), 90f);
        up = Vector3.Slerp(crossUp, up, angleDiff / 90f);

        var crossRight = Vector3.Cross(up, forward);
        up = Vector3.Cross(forward, crossRight);

        m_LastUp = up;

        interactorAttachPose.rotation = Quaternion.LookRotation(forward, up);

        if (!grabInteractable.trackRotation)
        {
            // When not using the rotation of the Interactor we apply the position without an offset
            targetPose.position = interactorAttachPose.position;
            return;
        }

        // Compute the new target world pose
        if (m_MultiSelectRotation == PoseContributor.First || m_MultiSelectRotation == PoseContributor.Second)
        {
            var controllerIndex = m_MultiSelectRotation == PoseContributor.First ? 0 : 1;
            var thisAttachTransform = grabInteractable.GetAttachTransform(grabInteractable.interactorsSelecting[controllerIndex]);
            Transform thisTransform = grabInteractable.transform;
            Pose thisTransformPose = new Pose(thisTransform.position, thisTransform.rotation);

            // Calculate offset of the grab interactable's position relative to its attach transform.
            // Transform that offset direction from world space to local space of the transform it's relative to.
            // It will be applied to the interactor's attach position using the orientation of the Interactor's attach transform.
            var attachOffset = thisTransformPose.position - thisAttachTransform.position;
            var positionOffset = thisAttachTransform.InverseTransformDirection(attachOffset);
            targetPose.position = (interactorAttachPose.rotation * positionOffset) + interactorAttachPose.position;
        }
        else if (m_MultiSelectRotation == PoseContributor.Average)
        {
            // Average rotation does not use offset and keeps objects between two attach points (controllers).
            targetPose.position = interactorAttachPose.position + avrAttachPositionOffset;
        }
        else
        {
            Assert.IsTrue(false, $"Unhandled {nameof(PoseContributor)}={m_MultiSelectRotation}.");
        }

        targetPose.rotation = interactorAttachPose.rotation * avrAttachRotationOffset;
    }

}