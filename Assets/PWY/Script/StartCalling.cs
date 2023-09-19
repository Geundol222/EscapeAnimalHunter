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
            colling_Activate[0].SetActive(true);  // Colling_Activate Ȱ��ȭ
            colling_Activate[1].SetActive(true);  // Answer The Phone_Start Ȱ��ȭ
            colling_Activate[2].SetActive(false); // Center Display ��Ȱ��ȭ
            colling_Activate[3].SetActive(false); // Phone_BackGround ��Ȱ��ȭ 
        }
    }

    private void StopAudio()
    {
        startAudio.Stop(); // �����Ҹ� ���߱�
        startAudio.loop = false; // ���� �ݺ�����
    }

    public void StopColling()
    {
        startAudio.Stop(); // �����Ҹ� ���߱�
        startAudio.loop = false; // ���� �ݺ�����
        // colling_Activate[0].SetActive(false); // Colling_Activate ��Ȱ��ȭ
        colling_Activate[1].SetActive(false); // Answer The Phone_Start ��Ȱ��ȭ
        // colling_Activate[2].SetActive(true);  // Center Display Ȱ��ȭ
        // colling_Activate[3].SetActive(true);  // Phone_BackGround Ȱ��ȭ 
        // colling_Activate[4].SetActive(true);  // Answer The Phone Ȱ��ȭ
        colling_Activate[5].SetActive(true);  // Answer The Phone_Quit Ȱ��ȭ
    }


}
