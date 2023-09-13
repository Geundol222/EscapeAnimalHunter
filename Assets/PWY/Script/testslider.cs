using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testslider : MonoBehaviour
{
    [SerializeField] Slider a;
    [SerializeField] Slider b;

    private void Update()
    {
        AA();
    }

    public void AA()
    {
        b.value = -a.value;
    }
}
