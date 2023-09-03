using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] double bulletSpeed;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask animalMask;

    Rigidbody rb;

    float fireAngle;
    float tipAngle;
    float ySpeed = 0f;
    float arriveTime = 0f;
    float maxHeight = 0f;
    float zPos;
    float yPos;
    double zVelo;
    double yVelo;

    float fz = 0;
    float fy = 0;

    float grav;

    Vector3 maxHVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        grav = Physics.gravity.magnitude;
        zPos = transform.position.z;
        yPos = transform.position.y;
        fireAngle = -transform.rotation.eulerAngles.x;
        tipAngle = fireAngle;
        maxHeight = (Mathf.Pow((float)bulletSpeed, 2) * Mathf.Pow(Mathf.Sin(fireAngle), 2)) / 2f * grav;

        StartCoroutine(BulletFlyRoutine());
    }

    IEnumerator BulletFlyRoutine()
    {
        while (true)
        {
            arriveTime += Time.deltaTime;
            zVelo = bulletSpeed * Mathf.Cos(fireAngle * Mathf.Deg2Rad) * arriveTime;
            yVelo = bulletSpeed * Mathf.Sin(fireAngle * Mathf.Deg2Rad) * arriveTime;

            double zz = zVelo;
            double yy = yVelo - (0.5 * grav * Mathf.Pow(arriveTime, 2));

            fz = Convert.ToSingle(zz);
            fy = Convert.ToSingle(yy);

            float curZ = gameObject.transform.position.z;
            float curY = gameObject.transform.position.y;

            float duration = 0;

            duration += Time.deltaTime;

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, fy + yPos, fz + zPos);            

            if (curY < maxHeight)
                tipAngle = Mathf.Lerp(fireAngle, 0f, Time.deltaTime / duration);
            else if (curY == maxHeight)
            {
                tipAngle = 0f;
                duration = 0f;
            }
            else
                tipAngle = Mathf.Lerp(0f, -fireAngle, Time.deltaTime / duration);

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
