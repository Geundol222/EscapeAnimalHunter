using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCaster : MonoBehaviour
{
    [SerializeField] float radiusMax; //15f
    [SerializeField] float radiusGrowUpSpeed; //10f
    [SerializeField] GameObject circle;
    [SerializeField] float circleGrowUpSpeed; //0.08f

    float maxDistance = 0.1f;
    float curRadius;
    int layerMask;

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Animal");
        curRadius = 0;
        circle.transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        curRadius += radiusGrowUpSpeed * Time.deltaTime;

        if (curRadius > radiusMax)
            curRadius = 0;

        circle.transform.localScale = new Vector3(curRadius * circleGrowUpSpeed, curRadius * circleGrowUpSpeed, curRadius * circleGrowUpSpeed);

        if (circle.transform.localScale.x > 1f)
            circle.transform.localScale = Vector3.zero;

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, curRadius, transform.forward, maxDistance, layerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].transform.GetComponent<RadarDetectPoint>().SetActive();
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        if (Physics.SphereCast(transform.position, curRadius, transform.forward, out RaycastHit hit, maxDistance))
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * maxDistance, curRadius);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * maxDistance, curRadius);
        }
    }
}
