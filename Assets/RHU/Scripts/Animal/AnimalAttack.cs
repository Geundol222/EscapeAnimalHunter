using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimalData;

public class AnimalAttack : MonoBehaviour
{
    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalName animalName;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();
            hittable?.TakeHit(data.Animals[(int)animalName].attackDamage);
        }
    }
}
