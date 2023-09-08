using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneTime : MonoBehaviour
{
    // 휴대폰 상단에 시간을 나타내는 스크립트입니다.

    [SerializeField] TMP_Text upper_Display_Time; // phone2k -> Canvas -> BackGround -> Text_Time를 드래그하여 넣어줍니다.
    [SerializeField] TMP_Text center_Display_Time;

    StringBuilder center_Display_Time_builder = new StringBuilder();


    private void Start()
    {
        StartCoroutine(GetTimeEveryMin());
    }

    private void GetCurrentTime_Upper() // 현재 시간과 분을 나타냅니다.
    {
        upper_Display_Time.text = DateTime.Now.ToString(("HH:mm"));
    }

    private void GetCurrentTime_Center()
    {
        center_Display_Time_builder.Append($"<size=10></size>"); //글자크기
        center_Display_Time_builder.Append("\n");
        center_Display_Time_builder.Append($"<b></b>");         // 굵기
        center_Display_Time_builder.Append("\n");
        center_Display_Time_builder.Append($"<i></i>");         // 기울임
        center_Display_Time_builder.Append("\n");
        center_Display_Time_builder.Append($"<color=#00ffffff></color>"); // 색상
        //center_Display_Time.text = $"<size=1> asdasd </size>";
        center_Display_Time.text = DateTime.Now.ToString(("yyyy:MM:dd\nHH:mm:ss"));
    }

    IEnumerator GetTimeEveryMin() // 1초마다 실행하여 현재 시간과 분을 체크합니다.
    {
        while (true)
        {
            GetCurrentTime_Upper();
            GetCurrentTime_Center();
            yield return new WaitForSeconds(1);
        }
    }

}
