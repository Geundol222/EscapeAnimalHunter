using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneMap : MonoBehaviour
{
    [SerializeField] List<Transform> marker; // 지도상에 나타나는 마커
    [SerializeField] List<Transform> target; // 마커가 표시되어야할 개체

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
