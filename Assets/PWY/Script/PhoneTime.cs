using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneTime : MonoBehaviour
{
    // 휴대폰 상단에 시간을 나타내는 스크립트입니다.

    [SerializeField] TMP_Text phorneTime; // phone2k -> Canvas -> BackGround -> Text_Time를 드래그하여 넣어줍니다.

    private void Start()
    {
        StartCoroutine(GetTimeEveryMin());
    }

    private void GetCurrentTime() // 현재 시간과 분을 나타냅니다.
    {
        phorneTime.text = DateTime.Now.ToString(("HH:mm"));
    }

    IEnumerator GetTimeEveryMin() // 1초마다 실행하여 현재 시간과 분을 체크합니다.
    {
        while (true)
        {
            GetCurrentTime();
            yield return new WaitForSeconds(1);
        }
    }

}
