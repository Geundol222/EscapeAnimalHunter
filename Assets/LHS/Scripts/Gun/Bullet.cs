using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Bullet : MonoBehaviour
{    
    [SerializeField] LayerMask carnivoreMask;
    [SerializeField] LayerMask herbivoreMask;

    Rigidbody rb;
    Collider col;

    private int damage;
    private int collisionCount;

    private float bulletSpeed;
    private float fireAngle;
    private float tipAngle;
    private float maxHeight = 0f;

    private float grav;

    private float xSpeed;
    private float zSpeed;
    private float ySpeed;

    private Vector3 initialPosition;
    private Vector3 initialVelocity;

    private bool isEffectiveCollider;

    private void Start()
    {
        collisionCount = 0;
        damage = DataManager.Bullet.damage;
        bulletSpeed = DataManager.Bullet.bulletSpeed;

        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        grav = Physics.gravity.magnitude;

        isEffectiveCollider = false;

        xSpeed = transform.forward.x * bulletSpeed;
        ySpeed = transform.forward.y * bulletSpeed;
        zSpeed = transform.forward.z * bulletSpeed;

        initialPosition = transform.position;

        fireAngle = transform.rotation.x;
        maxHeight = transform.position.y + (Mathf.Pow(initialVelocity.y, 2) / (2 * grav)) + initialPosition.y;

        StartCoroutine(BulletSurvivalRoutine());
        StartCoroutine(BulletFlyRoutine());
    }

    IEnumerator BulletSurvivalRoutine()
    {
        yield return new WaitForSeconds(20f);

        if (!isEffectiveCollider)
            GameManager.Resource.Destroy(gameObject);

        yield break;
    }

    /// <summary>
    /// 총알 사격 함수, 총알이 발사되면 포물선을 따라 진행한다.
    /// </summary>
    /// <returns> 총알이 멈출때까지 프레임 단위로 계산 </returns>
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

            if (isEffectiveCollider)
                yield break;

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        StopProjectile();
        StopCoroutine(BulletFlyRoutine());

        if (collisionCount > 1)
            return;

        if (carnivoreMask.IsContain(collision.gameObject.layer) || herbivoreMask.IsContain(collision.gameObject.layer))
        {
            isEffectiveCollider = true;

            ContactPoint contactPoint = collision.contacts[0];
            Vector3 StuckBulletVector = contactPoint.point;

            IHittable hittable = collision.gameObject.GetComponent<IHittable>();
            hittable?.TakeHit(damage);

            GameManager.Resource.Instantiate<GameObject>
                ("Prefabs/StuckBullet", StuckBulletVector, Quaternion.LookRotation(-contactPoint.normal), collision.collider.transform, true);
            GameManager.Resource.Destroy(gameObject);
        }
        else
        {
            isEffectiveCollider = true;

            GameManager.Resource.Destroy(gameObject, 10f);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void StopProjectile()
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
