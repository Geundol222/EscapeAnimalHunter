using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;   // �̱����� �Ҵ��� ����

    public GameObject player;

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
