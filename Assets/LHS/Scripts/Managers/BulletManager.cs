using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int damage = 3;
    public float bulletSpeed = 100;

    public void AddDamage(int cost)
    {
        damage += cost;
    }

    public void Reduce(int cost)
    {
        damage -= cost;
    }

    public void AddSpeed(int cost)
    {
        damage += cost;
    }
}
