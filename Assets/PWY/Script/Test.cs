using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject t1;

    private void Start()
    {
        float X = t1.transform.position.x;
        float Y = t1.transform.position.y;
        float Z = t1.transform.position.z;
    }

    private void Update()
    {
        T1();
    }

    private void T1()
    {
        for (int i = 0; i < 10; i++)
        {
            t1 = Instantiate(t1);
            break;
        }

        Vector3 Y = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        t1.transform.position = Y;
        //gameObject.transform.parent = t1.transform;
        t1.transform.parent = gameObject.transform;
    }

    //°¶·¯¸®
}
