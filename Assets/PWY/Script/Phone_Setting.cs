using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Phone_Setting : MonoBehaviour
{
    /// <summary>
    /// �̳̽���� ���
    /// Audio
    /// PWY/SFX/PhoneAudiu
    /// 
    /// 
    /// </summary>
    [Header("Audio")]
    [SerializeField] AudioMixer phoneAudio;
    [SerializeField] Slider audio_Master;           // ��ü�Ҹ�
    [SerializeField] Slider audio_Vibration;        // ������
    [SerializeField] Slider audio_CameraShutter;    // ī�޶� ������
    [Header("brightness")]
    [SerializeField] Transform directionalLight;    // Directional Light������Ʈ
    [SerializeField] Slider brightness_X;           // Directional Light�� Xȸ��
    [SerializeField] Slider brightness_Y;           // Directional Light�� Yȸ��
    [Header("PhoneSize")]
    [SerializeField] Transform phoneScale;          // �ڵ���
    [SerializeField] Slider phone_SizeX;            // �ڵ����� xũ��
    [SerializeField] Slider phone_SizeY;            // �ڵ����� yũ��



    // �Ҹ�
    [Range(0f, 1f)]
    public float master;
    [Range(0f, 1f)]
    public float vibration;
    [Range(0f, 1f)]
    public float cameraShutter;

    // ��
    [Range(0f, 360f)]
    public float x_Brightness;
    [Range(0f, 360f)]
    public float y_Brightness;

    // �ڵ��� ũ��
    [Range(2.3f, 5f)]
    public float phoneSizeX;
    [Range(2.3f, 5f)]
    public float phoneSizeY;


    private void Start()
    {
        audio_Master.value = 1;
        audio_Vibration.value = 1 ;
        audio_CameraShutter.value = 1;

        phone_SizeX.value = 2.3f;
        phone_SizeY.value = 2.3f;

        brightness_X.value = 90;
    }

    private void Update()
    {
        Audio_Master();
       // Audio_Vibration();
       // Audio_Camera();
        Phone_Size();
        Brightness_Control();
    }

    #region �Ҹ�����
    public void Audio_Master()
    {
        master = audio_Master.value;
        vibration = audio_Vibration.value;
        cameraShutter = audio_CameraShutter.value;
    }

    //public void Audio_Vibration()
    //{
    //    vibration = audio_Vibration.value;
    //}
    //
    //public void Audio_Camera()
    //{
    //    cameraShutter = audio_CameraShutter.value;
    //}
    #endregion

    #region ������
    private void Brightness_Control()
    {
        x_Brightness = brightness_X.value;
        y_Brightness = brightness_Y.value;

        directionalLight.localRotation = Quaternion.Euler(x_Brightness, y_Brightness, 0);
    }
    #endregion

    #region �ڵ����� ũ�⸦ �����ϴ� �Լ�
    public void Phone_Size()
    {
        phoneSizeX = phone_SizeX.value;
        phoneSizeY = phone_SizeY.value;
        phoneScale.localScale = new Vector3(phoneSizeX, phoneSizeY, transform.localScale.z);
    }
    #endregion


}
