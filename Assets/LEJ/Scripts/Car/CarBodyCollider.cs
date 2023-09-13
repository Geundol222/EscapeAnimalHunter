using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CarBodyCollider : MonoBehaviour
{
    public UnityAction<int, GameObject> OnHit;
    int carnivoreLayerMask;
    int harbivoreLayerMask;
    int groundLayerMask;

    private void Awake()
    {
        carnivoreLayerMask = 1 << LayerMask.NameToLayer("Carnivore");
        harbivoreLayerMask = 1 << LayerMask.NameToLayer("Harbivore");
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == groundLayerMask || collision.transform.gameObject.layer == carnivoreLayerMask || collision.transform.gameObject.layer == harbivoreLayerMask)
            OnHit?.Invoke(collision.transform.gameObject.layer, collision.gameObject);
    }
}
