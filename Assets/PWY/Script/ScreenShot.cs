using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    
    // 스크린샷찍을 카메라 스크립트 Camera가 비어있을 시 Phone - Phone_CanvasUI - Camera_Activate - Camera_BackGround - PhoneCamera이걸 넣으시면 됩니다.
    public Camera shot_Camera;                       // 찍어줄 카메라
    public RenderTexture display_Camera;             // 카메라가 보여줄 화면

    public RectTransform gallery_picture;

    public GameObject gallery_Content;                       // Gallery_Activate - Content를 넣기
    public RawImage picture; 

    public string folder_Name = "/PWY/ScreenShot/";  // 스크린샷이 저장될 폴더 경로
    public string file_Name = "PhoneScreenShot";     // 스크린샷 이름
    public string file_format = ".png";              // 스크린샷 형식-현재 png
    public string full_Path;                         // 전체 경로

    private int resWidth;                             
    private int resHeight;
    private int colorBit;


    private void Start()
    {
        Start_File_Delete(); // 시작시 스크린샷 폴더안에 있는 모든 파일 제거

        resWidth = 1080;
        resHeight = 1920;
        colorBit = 24;
    }

    private void Start_File_Delete()    // 시작시 스크린샷 폴더안에 있는 모든 파일 제거
    {
        full_Path = Application.dataPath + folder_Name;         
        string[] file_Delete = Directory.GetFiles(full_Path);
        for (int i = 0; i < file_Delete.Length; i++)                // 안에 있는 파일 갯수만큼 반복해서 삭제시킴
        {
            File.Delete(file_Delete[i]);
        }
    }


    public void PhoneScreenShot()
    {
        full_Path = Application.dataPath + folder_Name + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format; // 스크린샷 폴더 경로 + 이름 + 시간 + 형식

        RenderTexture cameraScreenShot = new RenderTexture(resWidth, resHeight, colorBit);      // 지정한 넓이, 높이, 색상(24비트)를 가진 RenderTexture를 생성하고 cameraScreenShot로 선언
        shot_Camera.targetTexture = cameraScreenShot;                                           // 지정한 카메라가 보고 있는 곳을 렌더링함 // 위에서 만든 렌더텍스처를 지정한 카메라에 렌더링함 

        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);  // 렌더링한 카메라의 스크린샷을 저장할 2D텍스처를 만듬 (크기는 설정한 넓이와 높이 24비트 RGB형식으로 저장함)
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);                         // x,y좌표를 0으로 두고 지정한 Texture2D의 넓이와 높이만 조절하여 만듬 

        shot_Camera.Render();                                                                   // Render함수를 호출해 지정한 카메라의 현재 프레임을 렌더링하고 cameraScreenShot에 저장

        RenderTexture.active = cameraScreenShot;                                                // 현재 활성화된 렌더텍스터를 cameraScreenShot에 저장 // cameraScreenShot의 텍스처를 렌더링함
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);                       // 텍스처2d로 만든 텍스처를 지정한 rect의 크기만큼 그려주고 byte로 변환함
        screenShot.Apply();                                                                     // Apply함수를 호출해 텍스처 변경사항 저장

        byte[] bytes = screenShot.EncodeToPNG();                                                // EncodeToPNG함수를 호출해 위에서 저장한 Texture2D의 screenShot를 png 이미지로 인코딩함
        File.WriteAllBytes(full_Path, bytes);                                                   // 인코딩한 이미지를 가장 위에 있는 full_Path에 저장함

        gallery_picture.sizeDelta += new Vector2(6.33f, 0f);                                    // 사진이 추가될 때마다 gallery_picture의 넓이만 지정한 만큼 늘려줌

        picture = Instantiate(picture);                                                         // RawImage의 인스턴스를 만듬
        picture.rectTransform.parent = gallery_Content.transform;                               // 갤러리의 자식으로 할당                      
        picture.rectTransform.position = gallery_Content.transform.position;                    // 사진과 갤러리의 위치를 똑같이 맞춤
        picture.rectTransform.localRotation = Quaternion.identity;                              // 부모의 회전값과 맞춤
        picture.rectTransform.localScale = new Vector3(1, 1, 1);                                  // xyz의 크기를 1로 맞춤
        picture.name = file_Name;                                                               // 파일 이름 바꾸기
        picture.texture = screenShot;                                                           // 만들어진 텍스처를 RawImage에 넣기

        shot_Camera.targetTexture = display_Camera;
    }


    // 테스트용
    /*private void PhoneScreenShot1(RawImage raw)
    {
        full_Path = Application.dataPath + folder_Name + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format; // 스크린샷 폴더 경로 + 이름 + 시간 + 형식
        
        RenderTexture cameraScreenShot = new RenderTexture(resWidth, resHeight, colorBit);      // 지정한 넓이, 높이, 색상(24비트)를 가진 RenderTexture를 생성하고 cameraScreenShot로 선언
        shot_Camera.targetTexture = cameraScreenShot;                                           // 지정한 카메라가 보고 있는 곳을 렌더링함
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);  // 스크린샷을 저장할 2D텍스처를 만듬 (크기는 설정한 넓이와 높이 24비트 RGB형식으로 저장함)
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);
        
        raw.texture = screenShot;
        shot_Camera.Render();                                                                   // Render함수를 호출해 지정한 카메라의 현재 프레임을 렌더링하고 cameraScreenShot에 저장
        
        RenderTexture.active = cameraScreenShot;                                                // 현재 활성화된 렌더텍스터를 cameraScreenShot에 저장
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);                       //
        screenShot.Apply();                                                                     // Apply함수를 호출해 텍스처 변경사항 저장
        
        byte[] bytes = screenShot.EncodeToPNG();                                                // EncodeToPNG함수를 호출해 위에서 저장한 Texture2D의 screenShot를 png 이미지로 인코딩함
        File.WriteAllBytes(full_Path, bytes);                                                   // 인코딩한 이미지를 가장 위에 있는 full_Path에 저장함

        raw = Instantiate(raw);
        raw.transform.parent = gallery_Content.transform;
        raw.name = file_Name;
        raw.texture = screenShot;
    }*/

    // 플레이어가 보는 화면 스크린샷으로 찍는 함수 (안씀)
    /*private void T1(string full_Path*//*경로*//*, string file_Name*//*이름*//*, string file_format*//*형식*//*)
    {
        // 플레이어의 화면에서 스크린샷 찍음
        full_Path = Application.dataPath + folder_Name; // 스크린샷 폴더 경로 
        ScreenCapture.CaptureScreenshot(full_Path + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format); // 스크린샷 찍기
        Debug.Log(full_Path + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format);
        //ScreenCapture.CaptureScreenshot(full_Path + file_Name + DateTime.Now.ToString("yyyyMMddhhmmss") + file_format); // 스크린샷 찍기

    }*/

}




