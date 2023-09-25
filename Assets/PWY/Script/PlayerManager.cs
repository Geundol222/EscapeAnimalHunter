using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;   // �̱����� �Ҵ��� ����


    private void Awake()
    {
        if (instance == null)   // instance�� ����ִٸ�
        {
            instance = this;    // instance�� ����
        }

        else                    // instance�� ������� �ʴٸ�
        {
            Debug.Log("�ν��Ͻ� �ߺ�üũ");
            Destroy(instance);  // ����
        }
    }


}
