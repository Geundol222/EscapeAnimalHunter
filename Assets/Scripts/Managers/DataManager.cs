using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    private static BulletManager bullet;
    private static UpgradeManager upgrade;
    private static CarDataManager car;

    public static BulletManager Bullet { get { return bullet; } }
    public static UpgradeManager Upgrade { get {  return upgrade; } }
    public static CarDataManager Car { get { return car; } }

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

        GameObject carObj = new GameObject();
        carObj.name = "CarDataManager";
        carObj.transform.parent = transform;
        car = carObj.AddComponent<CarDataManager>();
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