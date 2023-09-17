using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CarBodyCollider : MonoBehaviour
{
    [SerializeField] bool isForward;
    public UnityAction<GameObject, Vector3> OnHit;
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
        if (collision.gameObject.layer != 10 && collision.gameObject.layer != 11)
            return;

        if (isForward)
        {
            Debug.Log("Forward");
            OnHit?.Invoke(collision.transform.gameObject, transform.forward);
        }
        else
        {
            Debug.Log("Back");
            OnHit?.Invoke(collision.transform.gameObject, -transform.forward);
        }

    }
}
