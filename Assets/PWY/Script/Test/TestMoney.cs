using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoney : MonoBehaviour
{
    public int a = 100;

    private void Update()
    {
        T1();
    }

    private void T1()
    {
        if (a <= 0 )
        {
            //GameManager.Data.Money =  GameManager.Data.Money + 100;
            Destroy( gameObject );
        }
    }
}
