using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    private static BulletManager bullet;
    private static UpgradeManager upgrade;

    public static BulletManager Bullet { get { return bullet; } }
    public static UpgradeManager Upgrade { get {  return upgrade; } }

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

        GameObject upgradeObj = new GameObject();
        upgradeObj.name = "UpgradeManager";
        upgradeObj.transform.parent = transform;
        upgrade = upgradeObj.AddComponent<UpgradeManager>();
    }
    
    private int money = 0;

    public int Money
    {
        get { return money; }
        set
        {
            OnCurrentMoneyChanged?.Invoke(value);
            money = value;
        }
    }
    public event UnityAction<int> OnCurrentMoneyChanged;
}