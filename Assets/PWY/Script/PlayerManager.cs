using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;   // 싱글톤을 할당할 변수

    public GameObject player;

    private void Awake()
    {
        if (instance == null)   // instance가 비어있다면
        {
            instance = this;    // instance를 만듬
        }

        else                    // instance가 비어있지 않다면
        {
            Debug.Log("인스턴스 중복체크");
            Destroy(instance);  // 제거
        }
    }


}
