using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask animalMask;

    Rigidbody rb;

    float lerpTime = 0f;
    float fireAngle;
    float tipAngle;
    float maxHeight = 0f;
    float duration = 0f;

    float grav;

    float xSpeed;
    float zSpeed;
    float ySpeed;

    private void Start()
    {
        xSpeed = transform.forward.x * bulletSpeed;
        ySpeed = transform.forward.y * bulletSpeed;
        zSpeed = transform.forward.z * bulletSpeed;

        rb = GetComponent<Rigidbody>();
        grav = Physics.gravity.magnitude;
        fireAngle = -transform.rotation.eulerAngles.x;
        maxHeight = transform.position.y + ((Mathf.Pow(bulletSpeed, 2) * Mathf.Pow(Mathf.Sin(fireAngle), 2)) / (2f * grav));
        
        StartCoroutine(BulletFlyRoutine());

    }

    IEnumerator BulletFlyRoutine()
    {
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

            lerpTime += Time.deltaTime * 0.1f;

            tipAngle = Mathf.Lerp(fireAngle, 0, lerpTime / 1f);

            gameObject.transform.rotation = Quaternion.Euler(tipAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (groundMask.IsContain(collision.gameObject.layer))
        {
            StopAllCoroutines();
            GameManager.Resource.Destroy(gameObject, 10f);
        }
    }
}
