using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gogogo : MonoBehaviour
{
    public float time;
    private void Update()
    {
        time += Time.deltaTime;
        transform.Rotate(Vector3.up, time + Time.deltaTime);
        transform.Rotate(Vector3.left, time + Time.deltaTime);
    }
}

