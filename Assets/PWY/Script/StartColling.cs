using Unity.VisualScripting;
using UnityEngine;

public class StartColling : MonoBehaviour
{
    [SerializeField] AudioSource startAudio;
    [SerializeField] GameObject[] Colling_Activate;


    void Start()
    {
        startAudio.Play(); // 진동소리 키기
        startAudio.loop = true; // 진동소리 무한반복
        Colling_Activate[0].SetActive(true); // Colling_Activate 활성화
        Colling_Activate[1].SetActive(true); // Answer The Phone_Start 활성화
        Colling_Activate[2].SetActive(false); // Center Display 비활성화
        Colling_Activate[3].SetActive(false); // Phone_BackGround 비활성화 
    }

    public void StopColling()
    {
        startAudio.Stop(); // 진동소리 멈추기
        startAudio.loop = false; // 진동 반복끄기
        Colling_Activate[0].SetActive(false); // Colling_Activate 비활성화
        Colling_Activate[1].SetActive(false); // Answer The Phone_Start 비활성화
        Colling_Activate[2].SetActive(true);  // Center Display 활성화
        Colling_Activate[3].SetActive(true);  // Phone_BackGround 활성화 
        Colling_Activate[4].SetActive(true);  // Answer The Phone 활성화
    }


}
