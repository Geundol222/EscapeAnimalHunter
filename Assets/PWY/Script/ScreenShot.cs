using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.XR.Content.Interaction;

/// 2D 이미지 표현
/// width와 height : width와 height: 생성할 텍스처의 가로 및 세로 크기를 지정합니다. 일반적으로 Screen.width와 Screen.height를 사용하여 현재 화면의 해상도와 같은 크기의 텍스처를 만듭니다.
/// format: 텍스처의 포맷을 나타내는 매개변수로, TextureFormat 열거형을 사용합니다.여기서 TextureFormat.RGB24는 24비트 RGB 색상 포맷을 나타냅니다.이 포맷은 각 픽셀을 8비트의 빨간색, 녹색 및 파란색 채널로 나타내며, 총 24비트로 표현됩니다.
/// 맵(Mipmap) 을 사용할 것인지 여부를 나타내는 불리언 값입니다. 미프맵은 텍스처의 다양한 해상도를 사전에 생성하여 렌더링 품질을 향상시키는 데 사용됩니다.대개 false로 설정하여 미프맵을 사용하지 않도록 합니다.
/// <summary>
/// using System.IO;
/// 우선 System.IO를 추가해 줍시다. 이제 파일 생성과 삭제 등 파일 관리와 관련된 함수를 이용할 수 있게 됩니다.
/// 
/// 1. 파일 생성하기
/// File.Create(path); 파일생성
/// 기본적인 형태입니다. path는 파일이 만들어질 경로와 파일의 이름, 파일의 확장자를 포함합니다.
/// 유니티의 경우, 에디터의 플레이 모드 중에 변경된 내용은 플레이 모드를 종료하면 다시 원래대로 돌아가지만, 
/// 예외적으로 Resources 폴더에 들어 있는 파일과 폴더는 플레이 모드를 종료하더라도 사라지지 않습니다. 
/// 따라서 기본적으로 파일은 "Assets/Resources/" 안에 저장합니다.
/// "NewFile"이라는 이름의 텍스트 파일을 만들고 싶을 때, 다음과 같이 작성하여 파일을 만들 수 있습니다.
/// 
/// File.Create("Assets/Resources/NewFile.txt");
/// 
/// 파일의 존재 유무는 다음의 함수로 알 수 있습니다. bool값이 반환됩니다.
/// File.Exists(path);
/// 
/// 파일의 삭제는 다음의 함수로 할 수 있습니다.	
/// File.Delete(path);
/// 
/// 2.폴더 생성하기
/// Directory.CreateDirectory(path);
/// 기본적인 형태입니다. path는 폴더가 만들어질 경로와 폴더의 이름을 포함합니다.
/// 만들고 싶은 폴더의 이름이 "NewFolder"일 때, 다음과 같이 작성하여 폴더를 만들 수 있습니다.
/// 
/// 폴더의 존재 유무는 다음의 함수로 알 수 있습니다. bool값이 반환됩니다.
/// Directory.Exists(path);
/// 
/// 폴더의 삭제는 다음의 함수로 할 수 있습니다.
/// Directory.Delete(path);
/// 
/// 주의할 점은, 폴더 안에 파일이 남아 있으면 폴더 삭제가 되지 않는다는 점입니다.
/// 따라서 폴더를 제거하려면 안에 들어있는 모든 파일을 삭제해야 하는데요.
/// 
/// string[] allFiles = Directory.GetFiles(path);
/// for (int i = 0; i < allFiles.Length; i++)
/// {
///     File.Delete(allFiles[i]);
/// }
/// yield return null;
/// Directory.Delete(path);
/// 
/// 이렇게 GetFiles를 이용하여 모든 파일의 이름(경로)를 string[] 형식으로 받아올 수 있습니다.
/// 또 주의할 점은 파일을 모두 삭제했어도 즉시 폴더를 삭제할 수 없다는 점입니다.
/// 이유는 잘 모르겠지만... 한 프레임을 기다려준 후에는 폴더 삭제가 가능했습니다. 그래서 저는 Coroutine을 이용해 yield return null로 한 프레임을 기다려 주었습니다. (코루틴 함수에 대해 모른다면 검색해서 꼭 알아보시길 추천합니다)
/// </summary>

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
        shot_Camera.targetTexture = cameraScreenShot;                                           // 지정한 카메라가 보고 있는 곳을 렌더링함
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);  // 스크린샷을 저장할 2D텍스처를 만듬 (크기는 설정한 넓이와 높이 24비트 RGB형식으로 저장함)
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);

        shot_Camera.Render();                                                                   // Render함수를 호출해 지정한 카메라의 현재 프레임을 렌더링하고 cameraScreenShot에 저장

        RenderTexture.active = cameraScreenShot;                                                // 현재 활성화된 렌더텍스터를 cameraScreenShot에 저장
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);                       //
        screenShot.Apply();                                                                     // Apply함수를 호출해 텍스처 변경사항 저장

        byte[] bytes = screenShot.EncodeToPNG();                                                // EncodeToPNG함수를 호출해 위에서 저장한 Texture2D의 screenShot를 png 이미지로 인코딩함
        File.WriteAllBytes(full_Path, bytes);                                                   // 인코딩한 이미지를 가장 위에 있는 full_Path에 저장함

        gallery_picture.sizeDelta += new Vector2(6.33f, 0f);                                    // 사진이 추가될 때마다

        picture = Instantiate(picture);                                                         // RawImage의 인스턴스를 만듬
        picture.rectTransform.parent = gallery_Content.transform;                               // 갤러리의 자식으로 할당                      
        picture.rectTransform.position = gallery_Content.transform.position;                    // 사진과 갤러리의 위치를 똑같이 맞춤
        picture.rectTransform.localRotation = Quaternion.identity;                              // 회전
        picture.rectTransform.localScale = new Vector3(1,1,1);                                  // xyz의 크기를 1로 맞추기
        picture.name = file_Name;                                                               // 파일 이름 바꾸기
        picture.texture = screenShot;                                                           // 만들어진 텍스터를 RawImage에 넣기

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




