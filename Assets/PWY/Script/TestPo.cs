using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Animations;

public class TestPo : MonoBehaviour
{
    [SerializeField] XROrigin player;
    [SerializeField] Transform pos;
    [SerializeField] bool carBoarding;
    [SerializeField] Transform look;

    public Vector3 poos;

    private void Start()
    {
        poos = pos.position;
    }

    public void Test()
    {

        player.transform.position = poos;
        transform.LookAt(look);
    }
        
}
