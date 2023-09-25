using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    
    // ��ũ�������� ī�޶� ��ũ��Ʈ Camera�� ������� �� Phone - Phone_CanvasUI - Camera_Activate - Camera_BackGround - PhoneCamera�̰� �����ø� �˴ϴ�.
    public Camera shot_Camera;                       // ����� ī�޶�
    public RenderTexture display_Camera;             // ī�޶� ������ ȭ��

    public RectTransform gallery_picture;

    public GameObject gallery_Content;                       // Gallery_Activate - Content�� �ֱ�
    public RawImage picture; 

    public string folder_Name = "/PWY/ScreenShot/";  // ��ũ������ ����� ���� ���
    public string file_Name = "PhoneScreenShot";     // ��ũ���� �̸�
    public string file_format = ".png";              // ��ũ���� ����-���� png
    public string full_Path;                         // ��ü ���

    private int resWidth;                             
    private int resHeight;
    private int colorBit;


    private void Start()
    {
        Start_File_Delete(); // ���۽� ��ũ���� �����ȿ� �ִ� ��� ���� ����

        resWidth = 1080;
        resHeight = 1920;
        colorBit = 24;
    }

    private void Start_File_Delete()    // ���۽� ��ũ���� �����ȿ� �ִ� ��� ���� ����
    {
        full_Path = Application.dataPath + folder_Name;         
        string[] file_Delete = Directory.GetFiles(full_Path);
        for (int i = 0; i < file_Delete.Length; i++)                // �ȿ� �ִ� ���� ������ŭ �ݺ��ؼ� ������Ŵ
        {
            File.Delete(file_Delete[i]);
        }
    }


    public void PhoneScreenShot()
    {
        full_Path = Application.dataPath + folder_Name + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format; // ��ũ���� ���� ��� + �̸� + �ð� + ����

        RenderTexture cameraScreenShot = new RenderTexture(resWidth, resHeight, colorBit);      // ������ ����, ����, ����(24��Ʈ)�� ���� RenderTexture�� �����ϰ� cameraScreenShot�� ����
        shot_Camera.targetTexture = cameraScreenShot;                                           // ������ ī�޶� ���� �ִ� ���� �������� // ������ ���� �����ؽ�ó�� ������ ī�޶� �������� 

        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);  // �������� ī�޶��� ��ũ������ ������ 2D�ؽ�ó�� ���� (ũ��� ������ ���̿� ���� 24��Ʈ RGB�������� ������)
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);                         // x,y��ǥ�� 0���� �ΰ� ������ Texture2D�� ���̿� ���̸� �����Ͽ� ���� 

        shot_Camera.Render();                                                                   // Render�Լ��� ȣ���� ������ ī�޶��� ���� �������� �������ϰ� cameraScreenShot�� ����

        RenderTexture.active = cameraScreenShot;                                                // ���� Ȱ��ȭ�� �����ؽ��͸� cameraScreenShot�� ���� // cameraScreenShot�� �ؽ�ó�� ��������
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);                       // �ؽ�ó2d�� ���� �ؽ�ó�� ������ rect�� ũ�⸸ŭ �׷��ְ� byte�� ��ȯ��
        screenShot.Apply();                                                                     // Apply�Լ��� ȣ���� �ؽ�ó ������� ����

        byte[] bytes = screenShot.EncodeToPNG();                                                // EncodeToPNG�Լ��� ȣ���� ������ ������ Texture2D�� screenShot�� png �̹����� ���ڵ���
        File.WriteAllBytes(full_Path, bytes);                                                   // ���ڵ��� �̹����� ���� ���� �ִ� full_Path�� ������

        gallery_picture.sizeDelta += new Vector2(6.33f, 0f);                                    // ������ �߰��� ������ gallery_picture�� ���̸� ������ ��ŭ �÷���

        picture = Instantiate(picture);                                                         // RawImage�� �ν��Ͻ��� ����
        picture.rectTransform.parent = gallery_Content.transform;                               // �������� �ڽ����� �Ҵ�                      
        picture.rectTransform.position = gallery_Content.transform.position;                    // ������ �������� ��ġ�� �Ȱ��� ����
        picture.rectTransform.localRotation = Quaternion.identity;                              // �θ��� ȸ������ ����
        picture.rectTransform.localScale = new Vector3(1, 1, 1);                                  // xyz�� ũ�⸦ 1�� ����
        picture.name = file_Name;                                                               // ���� �̸� �ٲٱ�
        picture.texture = screenShot;                                                           // ������� �ؽ�ó�� RawImage�� �ֱ�

        shot_Camera.targetTexture = display_Camera;
    }


    // �׽�Ʈ��
    /*private void PhoneScreenShot1(RawImage raw)
    {
        full_Path = Application.dataPath + folder_Name + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format; // ��ũ���� ���� ��� + �̸� + �ð� + ����
        
        RenderTexture cameraScreenShot = new RenderTexture(resWidth, resHeight, colorBit);      // ������ ����, ����, ����(24��Ʈ)�� ���� RenderTexture�� �����ϰ� cameraScreenShot�� ����
        shot_Camera.targetTexture = cameraScreenShot;                                           // ������ ī�޶� ���� �ִ� ���� ��������
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);  // ��ũ������ ������ 2D�ؽ�ó�� ���� (ũ��� ������ ���̿� ���� 24��Ʈ RGB�������� ������)
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);
        
        raw.texture = screenShot;
        shot_Camera.Render();                                                                   // Render�Լ��� ȣ���� ������ ī�޶��� ���� �������� �������ϰ� cameraScreenShot�� ����
        
        RenderTexture.active = cameraScreenShot;                                                // ���� Ȱ��ȭ�� �����ؽ��͸� cameraScreenShot�� ����
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);                       //
        screenShot.Apply();                                                                     // Apply�Լ��� ȣ���� �ؽ�ó ������� ����
        
        byte[] bytes = screenShot.EncodeToPNG();                                                // EncodeToPNG�Լ��� ȣ���� ������ ������ Texture2D�� screenShot�� png �̹����� ���ڵ���
        File.WriteAllBytes(full_Path, bytes);                                                   // ���ڵ��� �̹����� ���� ���� �ִ� full_Path�� ������

        raw = Instantiate(raw);
        raw.transform.parent = gallery_Content.transform;
        raw.name = file_Name;
        raw.texture = screenShot;
    }*/

    // �÷��̾ ���� ȭ�� ��ũ�������� ��� �Լ� (�Ⱦ�)
    /*private void T1(string full_Path*//*���*//*, string file_Name*//*�̸�*//*, string file_format*//*����*//*)
    {
        // �÷��̾��� ȭ�鿡�� ��ũ���� ����
        full_Path = Application.dataPath + folder_Name; // ��ũ���� ���� ��� 
        ScreenCapture.CaptureScreenshot(full_Path + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format); // ��ũ���� ���
        Debug.Log(full_Path + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format);
        //ScreenCapture.CaptureScreenshot(full_Path + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format); // ��ũ���� ���

    }*/

}




