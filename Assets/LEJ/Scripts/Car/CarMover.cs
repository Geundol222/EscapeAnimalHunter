using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    [SerializeField] float sphereCastRadius;
    [SerializeField] float castHeight;
    [SerializeField] float maxDistant;

    CharacterController controller;
    float ySpeed;
    int layerMask;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }
    private void Update()
    {
        Fall();
    }


    private void Fall()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (GroundCheck() && ySpeed < 0)
        {

            ySpeed = 0;
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * castHeight, sphereCastRadius, Vector3.down, out hit, maxDistant, layerMask);
    }
}
