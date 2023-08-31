using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;
using static UnityEngine.Rendering.HableCurve;

public class Handle : MonoBehaviour
{
    [SerializeField] float rotateAmount;
    [SerializeField] float currentAngle;
    [SerializeField] float handleRotateSpeed;
    [SerializeField] float handleResetSpeed;

    [SerializeField] List<Vector3> circlePositions = new List<Vector3>();
    [SerializeField] List<Vector3> changePointPositions = new List<Vector3>();
    List<GameObject> areas = new List<GameObject>();
    [SerializeField] int changePointsNum = 12;
    [SerializeField] float colliderRadius;
    [SerializeField] GameObject sphereObj;
    [SerializeField] GameObject areasParentObj;

    int prevPoint;
    int curPoint;

    DetectHandleGrab rightHandleGrab;
    DetectHandleGrab leftHandleGrab;

    [SerializeField] bool isHandleGripped;
    bool isHandleOrigin;

    public void Awake()
    {
        rightHandleGrab = transform.GetChild(0).GetComponent<DetectHandleGrab>(); //ObjForRightHand
        leftHandleGrab = transform.GetChild(1).GetComponent<DetectHandleGrab>(); //ObjForLeftHand

        SetChangePoints();
        areasParentObj.transform.localRotation = Quaternion.Euler(50f, 0f, 0f);

        for (int i = 0; i < changePointsNum; i++)
        {
            changePointPositions.Add(areas[i].transform.position);
        }

        prevPoint = -1;
        curPoint = -1;
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

        if (isHandleGripped)
        {
            if (curPoint != CheckCurrentChangePoint())
            {
                Debug.Log("Have to change angle");
                ChangeHadleAngle();

                prevPoint = curPoint;
                curPoint = CheckCurrentChangePoint();
            }
        }
    }

    public void ResetRotation()
    {
        //핸들을 놓으면 원래 각도로 천천히 돌아가기 위해 Lerp 사용
        transform.rotation = Quaternion.Euler(-43f, 90f, Mathf.Lerp(currentAngle, 0f, handleResetSpeed));

        if (currentAngle == 0f)
            isHandleOrigin = true;
    }

    public void ChangeHadleAngle()
    {
        currentAngle = transform.rotation.z;

        if (prevPoint < curPoint || prevPoint == 11 && curPoint == 0)
            transform.rotation = Quaternion.Euler(-43f, 90f, Mathf.Lerp(currentAngle, currentAngle + rotateAmount, handleRotateSpeed));
        if (prevPoint > curPoint || prevPoint == 0 && curPoint == 11)
            transform.rotation = Quaternion.Euler(-43f, 90f, Mathf.Lerp(currentAngle, currentAngle - rotateAmount, handleRotateSpeed));
    }


    public void SetChangePoints()
    {
        float startAngle = 0;
        float arcLength = 360f;
        float radius = 0.2f;

        for (int i = 0; i <= changePointsNum; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * startAngle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * startAngle) * radius;
            circlePositions.Add(new Vector3(x, 0f, z));

            startAngle += (arcLength / changePointsNum);
        }

        for (int i = 0; i < changePointsNum; i++)
        {
            GameObject area = Instantiate(sphereObj);
            area.transform.SetParent(areasParentObj.transform);
            area.transform.position = transform.position;
            area.transform.position += circlePositions[i];

            areas.Add(area);
        }
    }

    public int CheckCurrentChangePoint()
    {
        for (int i = 0; i < changePointsNum; i++)
        {
            Collider[] colliders = Physics.OverlapSphere(changePointPositions[i], colliderRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.tag == "RightController" || collider.tag == "LeftController")
                {
                    Debug.Log(i);
                    return i;
                }
                else
                    continue;
            }
        }

        return -1;
    }

    public void CheckIfCurPointChange()
    {
        
    }
}
