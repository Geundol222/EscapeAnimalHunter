using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Phone_Setting : MonoBehaviour
{
    /// <summary>
    /// 미싱날경우 경로
    /// Audio
    /// PWY/SFX/PhoneAudiu
    /// 
    /// 
    /// </summary>
    [Header("Audio")]
    [SerializeField] AudioMixer phoneAudio;
    [SerializeField] Slider audio_Master;           // 전체소리
    [SerializeField] Slider audio_Vibration;        // 진동음
    [SerializeField] Slider audio_CameraShutter;    // 카메라 셔터음
    [Header("brightness")]
    [SerializeField] Transform directionalLight;    // Directional Light오브젝트
    [SerializeField] Slider brightness_X;           // Directional Light의 X회전
    [SerializeField] Slider brightness_Y;           // Directional Light의 Y회전
    [Header("PhoneSize")]
    [SerializeField] Transform phoneScale;          // 핸드폰
    [SerializeField] Slider phone_SizeX;            // 핸드폰의 x크기
    [SerializeField] Slider phone_SizeY;            // 핸드폰의 y크기



    // 소리
    [Range(0f, 1f)]
    public float master;
    [Range(0f, 1f)]
    public float vibration;
    [Range(0f, 1f)]
    public float cameraShutter;

    // 빛
    [Range(0f, 360f)]
    public float x_Brightness;
    [Range(0f, 360f)]
    public float y_Brightness;

    // 핸드폰 크기
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

    #region 소리설정
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

    #region 빛설정
    private void Brightness_Control()
    {
        x_Brightness = brightness_X.value;
        y_Brightness = brightness_Y.value;

        directionalLight.localRotation = Quaternion.Euler(x_Brightness, y_Brightness, 0);
    }
    #endregion

    #region 핸드폰의 크기를 조절하는 함수
    public void Phone_Size()
    {
        phoneSizeX = phone_SizeX.value;
        phoneSizeY = phone_SizeY.value;
        phoneScale.localScale = new Vector3(phoneSizeX, phoneSizeY, transform.localScale.z);
    }
    #endregion


}
