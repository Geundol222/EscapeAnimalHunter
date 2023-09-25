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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 10 && collision.gameObject.layer != 11)
            return;

        if (isForward)
        {
            OnHit?.Invoke(collision.transform.gameObject, transform.forward);
        }
        else
        {
            OnHit?.Invoke(collision.transform.gameObject, -transform.forward);
        }

    }
}
