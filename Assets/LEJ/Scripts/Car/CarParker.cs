using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CarParker : MonoBehaviour
{
    [SerializeField] string parkingPointLayerName;
    int parkingPointLayerMask;
    public bool isParking;
    public bool isInBaseCamp;

    private void Start()
    {
        parkingPointLayerMask = 1 << LayerMask.NameToLayer(parkingPointLayerName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == parkingPointLayerMask)
        {
            isInBaseCamp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer == parkingPointLayerMask)
        {
            isInBaseCamp = false;
        }
    }
}
