using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TestPo : MonoBehaviour
{
    [SerializeField] XROrigin player;
    [SerializeField] Transform pos;
    [SerializeField] bool carBoarding;

    public Vector3 poos;

    private void Start()
    {
        poos = pos.position;
    }

    public void Test()
    {
        carBoarding = true;
        if (carBoarding)
        {
            player.transform.position = poos;
            player.transform.Rotate(0,-90,0);
        }
    }
}
