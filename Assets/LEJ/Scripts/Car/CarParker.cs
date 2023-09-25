using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CarParker : MonoBehaviour
{
    [SerializeField] LayerMask parkingPointLayer;
    public bool isParking = true;
    public bool isInBaseCamp;
    public UnityAction OnParkedOrNot;

    private void Start()
    {
        DetectIsParkingInBaseCamp();
    }
    private void OnEnable()
    {
        DataManager.Car.OnGearStateIsChanged += DetectIsParkingInBaseCamp;
    }

    private void OnDisable()
    {
        DataManager.Car.OnGearStateIsChanged -= DetectIsParkingInBaseCamp;
    }


    private void OnTriggerStay(Collider other)
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

    void DetectIsParkingInBaseCamp()
    {
        if (isInBaseCamp && DataManager.Car.carCurState == CarDataManager.GearState.Parking)
        {
            isParking = true;
            OnParkedOrNot?.Invoke();
        }
        else
        {
            isParking = false;
            OnParkedOrNot?.Invoke();
        }
    }
}
