using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CarParker : MonoBehaviour
{
    [SerializeField] LayerMask parkingPointLayer;
    public bool isParking;
    public bool isInBaseCamp;

    private void OnTriggerEnter(Collider other)
    {
        if (parkingPointLayer.IsContain(other.transform.gameObject.layer))
        {
            isInBaseCamp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (parkingPointLayer.IsContain(other.transform.gameObject.layer))
        {
            isInBaseCamp = false;
        }
    }
}
