using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneMap : MonoBehaviour
{
    // 카메라랑 카메라 위치 동기화 y제외
    [SerializeField] Transform phoneTransform;
    [SerializeField] Transform camera;
    public float a;

    private void Update()
    {
        TT();
    }

    private void TT()
    {
        float phoneX = phoneTransform.transform.position.x;
        float PhoneZ = phoneTransform.transform.position.z;

        float cameraX = camera.transform.position.x;
        float cameraZ = camera.transform.position.z;

        Vector3 ads = new Vector3(phoneX,a,PhoneZ);
        camera.position = ads;

    }
}
