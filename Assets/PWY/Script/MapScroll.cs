using UnityEngine;
using UnityEngine.UIElements;

public class MapScroll : MonoBehaviour
{
    //[SerializeField] Transform phone_Map; // 핸드폰
    //[SerializeField] Transform mapCamera; // 미니맵 카메라 위치
    //[SerializeField] Transform Map_PinPoint; // 미니맵에 나타낼 내 위치
    //[SerializeField] Transform pnPoint;

    /*public bool isPhone; // true : 지도에 핸드폰 위치 고정

    private void Update()
    {
        //TouchMap();
        PinPoint();
        TouchSpeed();
    }*/

    /*private void PinPoint()
    {
        float phoneY = phone_Map.position.y;

        float cameraX = mapCamera.position.x;
        float cameraY = mapCamera.position.y;
        float cameraZ = mapCamera.position.z;

        float pinPointX = pnPoint.position.x;
        float pinPointY = pnPoint.position.y;
        float pinPointZ = pnPoint.position.z;

        if (pinPointX != cameraX || pinPointY != cameraY || pinPointZ != cameraZ )
        {
            Vector3 pin = new Vector3(cameraX, cameraY, cameraZ);
            pnPoint.position = pin;
        }
    }

    private void TouchSpeed()
    {
        float phoneX = phone_Map.position.x;
        float phoneY = phone_Map.position.y;
        float phoneZ = phone_Map.position.y;

        float cameraX = mapCamera.position.x;
        float cameraY = mapCamera.position.y;
        float cameraZ = mapCamera.position.z;

        float pinPointX = pnPoint.position.x;
        float pinPointZ = pnPoint.position.z;
        float pinPointY = pnPoint.position.y;

        if (pinPointX != cameraX)
        {
            Debug.Log("asdsdasd");
        }
        
        Vector3 point = new Vector3(phoneX, cameraY, phoneZ);
        mapCamera.localPosition = point;
    }


    private void TouchMap()
    {
        float phoneX = phone_Map.position.x;
        float phoneY = phone_Map.position.y;
        float phoneZ = phone_Map.position.y;

        float cameraX = mapCamera.position.x;
        float cameraY = mapCamera.position.y;
        float cameraZ = mapCamera.position.z;


        Vector3 phone = new Vector3(phoneX, cameraY, phoneZ);
        mapCamera.position = phone;
    }*/


}