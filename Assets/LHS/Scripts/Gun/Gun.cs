using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] Camera scopeCamera;
    [SerializeField] Bullet bullet;

    public bool IsShoot { get; set; } = false;

    public void Fire()
    {
        if (!IsShoot)
        {
            GameManager.Resource.Instantiate<GameObject>("Prefabs/Bullet", scopeCamera.transform.position, scopeCamera.transform.rotation, true);
            IsShoot = true;
        }
        else
            return;
    }
}