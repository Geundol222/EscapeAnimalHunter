using Unity.VisualScripting;
using UnityEngine;

public class StartColling : MonoBehaviour
{
    [SerializeField] AudioSource startAudio;
    [SerializeField] GameObject[] Colling_Activate;


    void Start()
    {
        startAudio.Play(); // �����Ҹ� Ű��
        startAudio.loop = true; // �����Ҹ� ���ѹݺ�
        Colling_Activate[0].SetActive(true); // Colling_Activate Ȱ��ȭ
        Colling_Activate[1].SetActive(true); // Answer The Phone_Start Ȱ��ȭ
        Colling_Activate[2].SetActive(false); // Center Display ��Ȱ��ȭ
        Colling_Activate[3].SetActive(false); // Phone_BackGround ��Ȱ��ȭ 
    }

    public void StopColling()
    {
        startAudio.Stop(); // �����Ҹ� ���߱�
        startAudio.loop = false; // ���� �ݺ�����
        Colling_Activate[0].SetActive(false); // Colling_Activate ��Ȱ��ȭ
        Colling_Activate[1].SetActive(false); // Answer The Phone_Start ��Ȱ��ȭ
        Colling_Activate[2].SetActive(true);  // Center Display Ȱ��ȭ
        Colling_Activate[3].SetActive(true);  // Phone_BackGround Ȱ��ȭ 
        Colling_Activate[4].SetActive(true);  // Answer The Phone Ȱ��ȭ
    }


}
