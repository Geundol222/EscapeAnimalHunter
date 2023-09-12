using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int damage = 1;
    public float bulletSpeed = 50;

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
