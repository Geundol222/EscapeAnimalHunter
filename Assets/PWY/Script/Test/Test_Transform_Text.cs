using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test_Transform_Text : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Transform textTransform;
    [SerializeField] TMP_Text text1;
    [SerializeField] TMP_Text text2;
    [SerializeField] TMP_Text text3;
    [SerializeField] TMP_Text text4;

    private void Start()
    {
        t();
        Invoke("tt", 3f);
    }

    private void Update()
    {
        float x = textTransform.position.x;
        float y = textTransform.position.y;
        float z = textTransform.position.z;

        string positionText = "X: " + x.ToString("F5") + "\nY: " + y.ToString("F5") + "\nZ: " + z.ToString("F5");

        text.text = positionText.ToString();
        text2.text = DateTime.Now.ToString(("HH:mm:ss"));
    }

    private void t()
    {
        text1.text = DateTime.Now.ToString(("HH:mm:ss"));
    }
    private void tt()
    {
        text3.text = DateTime.Now.ToString(("HH:mm:ss"));
    }
    //IEnumerator ttt()
    //{
    //    text3.text = DateTime.Now.ToString(("HH:mm:ss"));
    //    yield return new WaitForSeconds(1f);
    //}
    //IEnumerator tttt()
    //{
    //    text4.text = DateTime.Now.ToString(("HH:mm:ss"));
    //    yield return new WaitForSeconds(2f);
    //}

}
