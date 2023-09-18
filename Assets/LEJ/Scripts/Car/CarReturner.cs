using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarReturner : MonoBehaviour
{
    [SerializeField] Transform parkingPosition;
    LayerMask water;

    private void Start()
    {
        water = 1 << LayerMask.NameToLayer("Water");
    }
    public void ReturnToBaseCamp()
    {
        transform.position = parkingPosition.position;
        transform.rotation = parkingPosition.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == water)
        {
            ReturnToBaseCamp();
        }
    }
}
