using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Account : MonoBehaviour
{
    /// <summary>
    /// ���� ���� ������ �ִ� ��
    /// 1�ʸ��� �ڸ�ƾ���� Ȯ��
    /// </summary>
    [SerializeField] TMP_Text my_Money_Text;
    
    private void Start()
    {
        StartCoroutine(AccountBalance());
    }

    IEnumerator AccountBalance()
    {
        while (true)
        {
            my_Money_Text.text = $"{GameManager.Data.Money.ToString()} $ ";
            yield return new WaitForSeconds(1f);
        }
    }
}
