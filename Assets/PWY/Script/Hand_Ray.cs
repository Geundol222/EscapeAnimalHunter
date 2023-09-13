using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand_Ray : MonoBehaviour
{
    /*[SerializeField] GameObject left_Hand_Point;
    [SerializeField] GameObject right_Hand_Point;

    public float a;
    public float b;
    public float c;

    //public Vector3 boxSize = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 castDirection = Vector3.forward;
    public float maxDistance = 5.0f;
    private void Update()
    {
        ray();
    }

    private void ray()
    {
        Vector3 boxSize = new Vector3(a,b,c);
        float max = 1.0f;
        //RaycastHit hits;
        //float max = 15f;

        //Debug.DrawRay(le.transform.position, le.transform.forward * max, Color.blue, 0.3f);
        //Debug.DrawRay(ri.transform.position, ri.transform.forward * max, Color.blue, 0.3f);
        //Gizmos.color = Color.white;
        //var hits = Physics.SphereCastAll(le.transform.position, 0.1f, le.transform.forward);
        //bool hit = Physics.BoxCastAll(left_Hand_Point.transform.position, left_Hand_Point.transform.lossyScale / 2, transform.forward , out hits , left_Hand_Point.transform.rotation, 1);
        
        // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
        //bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, max);

        RaycastHit[] l_Hits = Physics.BoxCastAll(left_Hand_Point.transform.position, boxSize, castDirection, Quaternion.identity, maxDistance);
        RaycastHit[] r_Hits = Physics.BoxCastAll(right_Hand_Point.transform.position, boxSize, castDirection, Quaternion.identity, maxDistance);

        foreach (RaycastHit hit in l_Hits)
        {
            Debug.Log("Left" + hit.collider.gameObject.name);
            //Destroy(hit.collider);
            left_Hand_Point.transform.position = new Vector3(1,0,0);
        }

        foreach (RaycastHit hit in r_Hits)
        {
            Debug.Log("Right" + hit.collider.gameObject.name);
        }


        //foreach (RaycastHit hit in hits)
        //{
        //    // 충돌한 개체의 정보를 사용하여 원하는 작업을 수행합니다.
        //    Debug.Log("Hit object: " + hit.collider.gameObject.name);
        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(left_Hand_Point.transform.position, new Vector3(a,b,c));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(right_Hand_Point.transform.position, new Vector3(a,b,c));
    }*/
}
