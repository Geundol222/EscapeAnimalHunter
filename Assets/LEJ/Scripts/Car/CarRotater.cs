using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotater : MonoBehaviour
{
    [SerializeField] float rayDistant;
    [SerializeField] float rotationAmount;
    int layerMask;
    bool xRotate;

    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, rayDistant))
        {
            xRotate = true;
        }
        else
        {
            rotationAmount = 0;
            xRotate = false;

        }

        if (xRotate)
        {
            rotationAmount -= Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotationAmount, transform.rotation.y, transform.rotation.z);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawRay(transform.position, transform.forward * rayDistant);
        Gizmos.DrawRay(transform.position, -transform.forward * rayDistant);
        Gizmos.DrawRay(transform.position, transform.right * rayDistant);
        Gizmos.DrawRay(transform.position, -transform.right * rayDistant);

    }
}
