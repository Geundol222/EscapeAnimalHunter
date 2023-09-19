using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartCalling : MonoBehaviour
{
    [SerializeField] AudioSource startAudio;
    [SerializeField] List<GameObject> colling_Activate;

    private void Awake()
    {
        if (DataManager.Challenge.startcalling)
        {
            startAudio.Play();
            startAudio.loop = true;
            colling_Activate[0].SetActive(true);  // Colling_Activate 활성화
            colling_Activate[1].SetActive(true);  // Answer The Phone_Start 활성화
            colling_Activate[2].SetActive(false); // Center Display 비활성화
            colling_Activate[3].SetActive(false); // Phone_BackGround 비활성화 
        }
    }

    private void StopAudio()
    {
        startAudio.Stop(); // 진동소리 멈추기
        startAudio.loop = false; // 진동 반복끄기
    }

    public void StopColling()
    {
        startAudio.Stop(); // 진동소리 멈추기
        startAudio.loop = false; // 진동 반복끄기
        // colling_Activate[0].SetActive(false); // Colling_Activate 비활성화
        colling_Activate[1].SetActive(false); // Answer The Phone_Start 비활성화
        // colling_Activate[2].SetActive(true);  // Center Display 활성화
        // colling_Activate[3].SetActive(true);  // Phone_BackGround 활성화 
        // colling_Activate[4].SetActive(true);  // Answer The Phone 활성화
        colling_Activate[5].SetActive(true);  // Answer The Phone_Quit 활성화
    }


}
