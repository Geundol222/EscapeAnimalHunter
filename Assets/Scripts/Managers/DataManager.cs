using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DataManager : MonoBehaviour
{
    private GameObject player;
    private PlayerMoney playerMoney;

    private static BulletManager bullet;
    private static UpgradeManager upgrade;

    public static BulletManager Bullet { get { return bullet; } }
    public static UpgradeManager Upgrade { get {  return upgrade; } }

    private void Start()
    {
        StartCoroutine(FindPlayerRoutine());
    }

    IEnumerator FindPlayerRoutine()
    {
        yield return new WaitUntil(() => { return GameObject.FindGameObjectWithTag("Player"); });

        Debug.Log("Find Player Complete");

        player = GameObject.FindGameObjectWithTag("Player");

        InitData();
        playerMoney = player.GetComponent<PlayerMoney>();

        yield break;
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
    
    private int money = 9999;

    public int Money { get { return money; } }

    public void InitMoney(int money)
    {
        playerMoney.InitMoney(money);
    }

    public void AddMoney(int cost)
    {
        if (money >= 9999)
            return;
        else
            money += cost;

        InitMoney(money);
    }

    public void RemoveMoney(int cost)
    {
        if (money <= 0)
            money = 0;
        else
            money -= cost;

        InitMoney(money);
    }
}