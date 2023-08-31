using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.HableCurve;

public class Handle : MonoBehaviour
{
    [SerializeField] float rotateAmount;
    [SerializeField] float controllerSensitivity;
    [SerializeField] float currentAngle;
    [SerializeField] float handleResetSpeed;

    [SerializeField] List<Vector3> arcPoints = new List<Vector3>();
    [SerializeField] int areaPointsNum;
    [SerializeField] GameObject sphereObj;
    [SerializeField] GameObject areas;

    DetectHandleGrab rightHandleGrab;
    DetectHandleGrab leftHandleGrab;

    bool isHandleGripped;
    bool isHandleOrigin;
    float maxAngle = 90f;
    float minAngle = -90f;
    IXRSelectInteractor interactor;

    public void Awake()
    {
        rightHandleGrab = transform.GetChild(0).GetComponent<DetectHandleGrab>(); //ObjForRightHand
        leftHandleGrab = transform.GetChild(1).GetComponent<DetectHandleGrab>(); //ObjForLeftHand
    }

    public void Start()
    {
        MakeArcWithLine();
        areas.transform.localRotation = Quaternion.Euler(50f, -11f, -82f);
        areas.transform.localPosition = new Vector3(0f, 0.1f, 0f);
    }

    public void Update()
    {

        if (rightHandleGrab.isGripHandle == true || leftHandleGrab.isGripHandle == true)
            isHandleGripped = true;
        else
        {
            isHandleGripped = false;

            if (!isHandleOrigin)
                ResetRotation();
        }
    }

    public void ResetRotation()
    {
        //핸들을 놓으면 원래 각도로 천천히 돌아가기 위해 Lerp 사용
        currentAngle = Mathf.Lerp(currentAngle, 0f, handleResetSpeed);

        if (currentAngle == 0f)
            isHandleOrigin = true;
    }

    public void ChangeHadleAngle()
    {
        if (isHandleGripped)
        {
            
        }
    }


    public void SetArea()
    {
    }

    public void MakeArcWithLine()
    {
        float startAngle = 0;
        float arcLength = 360f;
        float radius = 0.2f;

        for (int i = 0; i <= areaPointsNum; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * startAngle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * startAngle) * radius;
            arcPoints.Add(new Vector3(x, 0f, z));

            startAngle += (arcLength / areaPointsNum);
        }

        for (int i = 0; i < areaPointsNum; i++)
        {
            GameObject area = Instantiate(sphereObj);
            area.transform.SetParent(areas.transform);
            area.transform.position = transform.position;
            area.transform.position += arcPoints[i];
        }
    }
}
