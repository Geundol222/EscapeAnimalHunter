using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] public InputActionProperty triggerAction;
    [SerializeField] public InputActionProperty gripAction; 

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();


        animator.SetFloat("Triger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }
}
