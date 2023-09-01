using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneTime : MonoBehaviour
{
    // �޴��� ��ܿ� �ð��� ��Ÿ���� ��ũ��Ʈ�Դϴ�.

    [SerializeField] TMP_Text phorneTime; // phone2k -> Canvas -> BackGround -> Text_Time�� �巡���Ͽ� �־��ݴϴ�.

    private void Start()
    {
        StartCoroutine(GetTimeEveryMin());
    }

    private void GetCurrentTime() // ���� �ð��� ���� ��Ÿ���ϴ�.
    {
        phorneTime.text = DateTime.Now.ToString(("HH:mm"));
    }

    IEnumerator GetTimeEveryMin() // 1�ʸ��� �����Ͽ� ���� �ð��� ���� üũ�մϴ�.
    {
        while (true)
        {
            GetCurrentTime();
            yield return new WaitForSeconds(1);
        }
    }

}
