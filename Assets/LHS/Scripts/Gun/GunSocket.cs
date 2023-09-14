using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunSocket : MonoBehaviour
{
    [SerializeField] XRGrabInteractable gun;
    [SerializeField] GameObject gunMat;
    [SerializeField] Transform gunAttachPoint;
    [SerializeField] LayerMask gunMask;

    bool isAttachable = false;

    private void Awake()
    {
        gunMat.SetActive(false);
    }

    private void Update()
    {
        if (isAttachable)
            gun.gameObject.transform.position = gunAttachPoint.position;
        else
            return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gunMask.IsContain(other.gameObject.layer))
        {
            if (gun.isSelected)
            {
                isAttachable = false;
                gunMat.SetActive(true);
            }
            else
            {
                isAttachable = true;
                gunMat.SetActive(false);
            }                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gunMask.IsContain(other.gameObject.layer))
            gunMat.SetActive(false);
    }
}
