using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarReturner : MonoBehaviour
{
    [SerializeField] Transform baseCampParkingSpot;
    LayerMask water;

    private void Start()
    {
        water = 1 << LayerMask.NameToLayer("Water");
    }
    public void ReturnToBaseCamp()
    {
        transform.position = baseCampParkingSpot.position;
        transform.rotation = baseCampParkingSpot.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == water)
        {
            ReturnToBaseCamp();
        }
    }
}
