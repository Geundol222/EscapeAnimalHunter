using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneMap : MonoBehaviour
{
    [SerializeField] List<Transform> marker; // ������ ��Ÿ���� ��Ŀ
    [SerializeField] List<Transform> target; // ��Ŀ�� ǥ�õǾ���� ��ü

    public void Synchronization()
    {
        Vector3 phonePos = new Vector3(target[0].position.x, marker[0].transform.position.y, target[0].position.z);
        marker[0].position = phonePos;
        Vector3 carPos = new Vector3(target[1].position.x, marker[0].transform.position.y, target[1].position.z);
        marker[1].position = carPos;
    }

    private void Update()
    {
        Synchronization();
    }
}
