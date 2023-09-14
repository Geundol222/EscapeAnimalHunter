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
    int layerMaskForCarnivore;
    int layerMaskForHerbivore;
    private void Start()
    {
        layerMaskForCarnivore = 1 << LayerMask.NameToLayer("Carnivore");
        layerMaskForHerbivore = 1 << LayerMask.NameToLayer("Herbivore");
        curRadius = radiusMax;
    }

    private void Update()
    {
        curRadius += radiusGrowUpSpeed * Time.deltaTime;

        if (curRadius > radiusMax)
            curRadius = 0;

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, curRadius, transform.forward, maxDistance, layerMaskForHerbivore);

        for (int i = 0; i < hits.Length; i++)
            OnDetectHerbivore?.Invoke(hits[i].transform, false);

        hits = Physics.SphereCastAll(transform.position, curRadius, transform.forward, maxDistance, layerMaskForCarnivore);

        for (int i = 0; i < hits.Length; i++)
        {
            OnDetectCarnivore?.Invoke(hits[i].transform, true);
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
