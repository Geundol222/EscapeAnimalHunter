using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text dateText;
    [SerializeField] TMP_Text timeText;

    private void Awake()
    {
        dateText.text = DateTime.Now.ToString(("yyyy-MM-dd"));
        timeText.text = DateTime.Now.ToString(("HH:mm"));
    }
}
