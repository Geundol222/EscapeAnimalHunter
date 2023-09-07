using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCanvasRotater : MonoBehaviour
{
    [SerializeField] GameObject car;

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, -car.transform.eulerAngles.y);
    }
}
