using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SphereCasterNew : MonoBehaviour
{
    [SerializeField] public float radiusMax; //15f
    [SerializeField] float radiusGrowUpSpeed; //10f
    public UnityAction<Transform, bool> OnDetectCarnivore;
    public UnityAction<Transform, bool> OnDetectHerbivore;

    float maxDistance = 0.1f;
    public float curRadius;
    int radarLayer;
    int carnivoreLayer;
    int harbivoreLayer;
    private void Start()
    {
        radarLayer = 1 << LayerMask.NameToLayer("Radar");
        carnivoreLayer = 1 << LayerMask.NameToLayer("Carnivore");
        harbivoreLayer = 1 << LayerMask.NameToLayer("Harbivore");
        curRadius = radiusMax;
    }

        RaycastHit[] hits;

    private void Update()
    {
        curRadius += radiusGrowUpSpeed * Time.deltaTime;

        if (curRadius > radiusMax)
            curRadius = 0;

        hits = Physics.SphereCastAll(transform.position, curRadius, transform.forward, maxDistance, radarLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.transform.parent.gameObject.layer == 11)
                OnDetectHerbivore?.Invoke(hits[i].collider.transform, false);

            if (hits[i].collider.gameObject.transform.parent.gameObject.layer == 10)
                OnDetectCarnivore?.Invoke(hits[i].collider.transform, true);
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
