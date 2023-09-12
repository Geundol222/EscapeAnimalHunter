using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static BulletManager bullet;

    public static BulletManager Bullet { get { return bullet; } }

    private void Awake()
    {
        InitData();
    }

    private void InitData()
    {
        GameObject bulletObj = new GameObject();
        bulletObj.name = "BulletManager";
        bulletObj.transform.parent = transform;
        bullet = bulletObj.AddComponent<BulletManager>();
    }

    public int money = 0;

    public void GetCost(int cost)
    {
        money += cost;
    }

    public void RemoveCost(int cost)
    {
        money -= cost;
    }
}