using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] Camera scopeCamera;
    [SerializeField] Bullet bullet;

    bool isShoot;

    public void Fire()
    {
        if (!isShoot)
        {
            GameManager.Resource.Instantiate<GameObject>("Prefabs/Bullet", scopeCamera.transform.position, scopeCamera.transform.rotation, true);
            isShoot = true;
        }
        else
            return;
    }
}
