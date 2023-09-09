using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();
            hittable?.TakeHit(1);
        }
    }
}
