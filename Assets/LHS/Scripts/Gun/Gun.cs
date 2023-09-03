using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] Bullet bullet;

    bool isShoot;

    public void Fire()
    {
        if (!isShoot)
        {
            GameManager.Resource.Instantiate<GameObject>("Prefabs/Bullet", muzzlePoint.localPosition, muzzlePoint.localRotation, true);
            isShoot = true;
        }
        else
            return;
    }
}
