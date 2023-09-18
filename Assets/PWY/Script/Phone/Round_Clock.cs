using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Round_Clock : MonoBehaviour
{
    public RectTransform second_Hand;   // ��ħ
    public RectTransform minute_Hand;   // ��ħ
    public RectTransform hour_Hand;     // ��ħ

    private void Start()
    {
        StartCoroutine(Round_Clock_Time());
    }

    IEnumerator Round_Clock_Time() // ��, ��, �ð��� int�� ��ȯ��Ų���� ��ȯ��Ų��ŭ ������ �����ѵ� ������ ȸ������ �ö��� �ʰ� ��, ��, �ð��� 60�� �Ǹ� z�� ȸ������ 0���� ������ݴϴ�
    {
        while (true)
        {
            // �ð�
            string second_Text = DateTime.Now.ToString("ss");   // ��
            string minute_Text = DateTime.Now.ToString("mm");   // ��
            string hour_Text = DateTime.Now.ToString("HH");     // �ð�
            
            // ��ȯ
            int second_Int = int.Parse(second_Text);            // �ʸ� int�� ��ȯ
            int minute_Int = int.Parse(minute_Text);            // ���� int�� ��ȯ
            int hour_Int = int.Parse(hour_Text);                // �ð��� int�� ��ȯ
            
            // ȸ��
            second_Hand.localRotation = Quaternion.Euler(0, 180, second_Int * 6); // y�� 180���� ���ϴ� �������� �̵���, �ʴ� 6���� z�� �̵���Ŵ
            minute_Hand.localRotation = Quaternion.Euler(0, 180, minute_Int * 6); // �д� 6���� z�� �̵�
            hour_Hand.localRotation = Quaternion.Euler(0, 180, hour_Int * 30f + (minute_Int / 2f)); // �ð��� 30���� �̵���Ų �� �� / 2��ŭ �� �̵���Ŵ

            //�ʱ�ȭ
            if (second_Int == 60)
            {
                second_Hand.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (minute_Int == 60)
            {
                minute_Hand.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (hour_Int == 60)
            {
                hour_Hand.localRotation = Quaternion.Euler(0, 180, 0);
            }
            
            yield return new WaitForSeconds(1);
        }
    }
    #region 
    #endregion
}
