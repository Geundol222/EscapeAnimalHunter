using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] LayerMask carnivoreMask;
    [SerializeField] LayerMask herbivoreMask;

    Rigidbody rb;

    public int damage;

    private float fireAngle;
    private float tipAngle;
    private float maxHeight = 0f;
    private float duration = 0f;

    private float grav;

    private float xSpeed;
    private float zSpeed;
    private float ySpeed;

    private Vector3 initialPosition;
    private Vector3 initialVelocity;

    private void Start()
    {
        xSpeed = transform.forward.x * bulletSpeed;
        ySpeed = transform.forward.y * bulletSpeed;
        zSpeed = transform.forward.z * bulletSpeed;

        initialPosition = transform.position;

        rb = GetComponent<Rigidbody>();
        grav = Physics.gravity.magnitude;
        fireAngle = transform.rotation.x;
        maxHeight = transform.position.y + (Mathf.Pow(initialVelocity.y, 2) / (2 * grav)) + initialPosition.y;

        StartCoroutine(BulletFlyRoutine());
    }

    IEnumerator BulletFlyRoutine()
    {
        float elapsedTime = 0f;
        float descentTime = maxHeight * 2f;

        while (true)
        {
            // zVelo = bulletSpeed * Mathf.Cos(fireAngle * Mathf.Deg2Rad) * Time.deltaTime;
            // yVelo = bulletSpeed * Mathf.Sin(fireAngle * Mathf.Deg2Rad) * Time.deltaTime;
            // 
            // float fx = xPos * zVelo;
            // float fz = zPos * zVelo;
            // float fy = (yPos * yVelo) - (0.5f * grav * Mathf.Pow(Time.deltaTime, 2));
            // 
            // transform.position += new Vector3(fx, fy, fz);

            ySpeed -= grav * Time.deltaTime;
            
            transform.position += new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime;

            if (elapsedTime <= maxHeight)
            {
                tipAngle = Mathf.Lerp(fireAngle, 0, elapsedTime / maxHeight);
            }

            elapsedTime += Time.deltaTime;

            gameObject.transform.rotation = Quaternion.Euler(tipAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (carnivoreMask.IsContain(collision.gameObject.layer) || herbivoreMask.IsContain(collision.gameObject.layer))
        {
            StopCoroutine(BulletFlyRoutine());
            StopProjectile();
            transform.parent = collision.gameObject.transform;

            StartCoroutine(DamageRoutine(collision));
        }
        else
        {
            StopCoroutine(BulletFlyRoutine());
            StopProjectile();
            GameManager.Resource.Destroy(gameObject, 10f);
        }
    }

    IEnumerator DamageRoutine(Collision collision)
    {
        int tickCount = 0;

        while (tickCount < 4)
        {
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();
            hittable?.TakeHit(damage);
            tickCount++;
            yield return new WaitForSeconds(1f);
        }

        yield break;
    }

    void StopProjectile()
    {
        // Stop the projectile
        enabled = false;
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Handle collision or other cleanup logic here
        Debug.Log("Projectile has stopped.");
    }
}
