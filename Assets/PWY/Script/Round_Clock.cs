using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Round_Clock : MonoBehaviour
{
    public RectTransform second_Hand;   // 초침
    public RectTransform minute_Hand;   // 분침
    public RectTransform hour_Hand;     // 시침

    private void Start()
    {
        StartCoroutine(Round_Clock_Time());
    }

    IEnumerator Round_Clock_Time() // 초, 분, 시간을 int로 변환시킨다음 변환시킨만큼 각도를 조정한뒤 무한정 회전값이 올라가지 않게 초, 분, 시간이 60이 되면 z의 회전값을 0으로 만들어줍니다
    {
        while (true)
        {
            // 시간
            string second_Text = DateTime.Now.ToString("ss");   // 초
            string minute_Text = DateTime.Now.ToString("mm");   // 분
            string hour_Text = DateTime.Now.ToString("HH");     // 시간
            
            // 변환
            int second_Int = int.Parse(second_Text);            // 초를 int로 변환
            int minute_Int = int.Parse(minute_Text);            // 분을 int로 변환
            int hour_Int = int.Parse(hour_Text);                // 시간을 int로 변환
            
            // 회전
            second_Hand.localRotation = Quaternion.Euler(0, 180, second_Int * 6); // y를 180도로 안하니 역방으로 이동함, 초당 6도씩 z축 이동시킴
            minute_Hand.localRotation = Quaternion.Euler(0, 180, minute_Int * 6); // 분당 6도씩 z축 이동
            hour_Hand.localRotation = Quaternion.Euler(0, 180, hour_Int * 30f + (minute_Int / 2f)); // 시간에 30도를 이동시킨 뒤 분 / 2만큼 더 이동시킴

            //초기화
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
