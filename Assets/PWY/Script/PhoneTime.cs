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
    // �޴��� ��ܿ� �ð��� ��Ÿ���� ��ũ��Ʈ�Դϴ�.

    [SerializeField] TMP_Text upper_Display_Time; // phone2k -> Canvas -> BackGround -> Text_Time�� �巡���Ͽ� �־��ݴϴ�.
    [SerializeField] TMP_Text center_Display_Time;

    StringBuilder center_Display_Time_builder = new StringBuilder();


    private void Start()
    {
        StartCoroutine(GetTimeEveryMin());
    }

    private void GetCurrentTime_Upper() // ���� �ð��� ���� ��Ÿ���ϴ�.
    {
        upper_Display_Time.text = DateTime.Now.ToString(("HH:mm"));
    }

    private void GetCurrentTime_Center()
    {
        center_Display_Time_builder.Append($"<size=10></size>"); //����ũ��
        center_Display_Time_builder.Append("\n");
        center_Display_Time_builder.Append($"<b></b>");         // ����
        center_Display_Time_builder.Append("\n");
        center_Display_Time_builder.Append($"<i></i>");         // �����
        center_Display_Time_builder.Append("\n");
        center_Display_Time_builder.Append($"<color=#00ffffff></color>"); // ����
        //center_Display_Time.text = $"<size=1> asdasd </size>";
        center_Display_Time.text = DateTime.Now.ToString(("yyyy:MM:dd\nHH:mm:ss"));
    }

    IEnumerator GetTimeEveryMin() // 1�ʸ��� �����Ͽ� ���� �ð��� ���� üũ�մϴ�.
    {
        while (true)
        {
            GetCurrentTime_Upper();
            GetCurrentTime_Center();
            yield return new WaitForSeconds(1);
        }
    }

}
