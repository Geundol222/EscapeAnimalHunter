using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using static AnimalData;

public class Graveyard : MonoBehaviour
{
    private static Graveyard grave;
    public static Graveyard graveyard { get { return grave; } }
    int bearCount = 0;
    int moosCount = 0;

    private void Start()
    {
        GameObject graveyardobj = new GameObject();
        graveyardobj.name = "CarDataManager";
        graveyardobj.transform.parent = transform;
        grave = graveyardobj.AddComponent<Graveyard>();
    }

    private void Update()
    {
        GraveyardCount();
    }
    

    public void GraveyardCount()
    {
        // ����Ƽ �׼����� ���ڰ� ����
        // �ִϸ� ����Ʈ�� �ִ� ������Ʈ �������� ������
        // �ִϸ� ����Ʈ�� �ִ� ������ �̸��� Ư�� ������ �����ϸ� ���� Ȯ��
        // ���� �������� 1������������ �ִ� 4����
        
        if (gameObject.GetNamedChild("Bear"))
        {
            //DataManager.Challenge.Bear_Challenge_Attainment = true;
            if (gameObject.GetNamedChild("Bear") && transform.childCount == 1)
            {

            }
            if (gameObject.GetNamedChild("Bear") && transform.childCount == 2)
            {

            }
            if (gameObject.GetNamedChild("Bear") && transform.childCount == 3)
            {

            }
            if (gameObject.GetNamedChild("Bear") && transform.childCount == 4)
            {

            }
            
        }
        Debug.Log(bearCount);

        if (gameObject.GetNamedChild("Moos") && transform.childCount == 4)
        {
            //DataManager.Challenge.Moos_Challenge_Attainment = true;
            if (gameObject.GetNamedChild("Moos") && transform.childCount == 1)
            {

            }
            if (gameObject.GetNamedChild("Moos") && transform.childCount == 2)
            {

            }
            if (gameObject.GetNamedChild("Moos") && transform.childCount == 3)
            {

            }
            if (gameObject.GetNamedChild("Moos") && transform.childCount == 4)
            {

            }
        }
    }
    // GameObject bear;
    // 
    // public GameObject Bear
    // {
    //     get { return bear; }
    //     set 
    //     {
    //         OnChallengeEvent?.Invoke(bear);
    //     }
    // }
    // 
    // GameObject moos;
    // 
    // public GameObject Moos
    // {
    //     get { return moos; }
    //     set
    //     {
    //         OnChallengeEvent?.Invoke(moos);
    //     }
    // }


    //public event UnityAction<GameObject> OnChallengeEvent;

}
