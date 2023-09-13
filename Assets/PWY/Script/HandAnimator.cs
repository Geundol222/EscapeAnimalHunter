using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.XInput;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] Animator left_Animator;
    [SerializeField] Animator right_Animator;

    public void Phone_Grip(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.name == "Right Direct Interactor")
        {
            // ������ �ִϸ����� ����
            Debug.Log("���������� ����");
            right_Animator.SetLayerWeight(1, 1);
        }
        else if (args.interactorObject.transform.gameObject.name == "Left Direct Interactor")
        {
            // �޼� �ִϸ����� ����
            Debug.Log("�޼����� ����");
            left_Animator.SetLayerWeight(1, 1);
        }
    }

    public void Phone_Drop(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.name == "Right Direct Interactor")
        {
            // ������ �ִϸ����� ����
            Debug.Log("���������� ����");
            right_Animator.SetLayerWeight(1, 0);
        }
        else if (args.interactorObject.transform.gameObject.name == "Left Direct Interactor")
        {
            // �޼� �ִϸ����� ����
            Debug.Log("�޼����� ����");
            left_Animator.SetLayerWeight(1, 0);
        }
    }
}
