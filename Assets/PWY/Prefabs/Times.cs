using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Times : MonoBehaviour
{
    public TMP_Text t1;

    public static string GetCurrentDate()
    {
        return DateTime.Now.ToString(("HH:mm"));
    }

    private void Update()
    {
        t1.text = GetCurrentDate();
    }

}
