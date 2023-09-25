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
    private static CarDataManager car;
    private static ChallengeManager challenge;

    public static BulletManager Bullet { get { return bullet; } }
    public static UpgradeManager Upgrade { get {  return upgrade; } }
    public static CarDataManager Car { get { return car; } }
    public static ChallengeManager Challenge { get { return challenge; } }

    private void Awake()
    {
        InitData();
    }

    private void Start()
    {
        StartCoroutine(FindPlayerRoutine());
    }

    IEnumerator FindPlayerRoutine()
    {
        yield return new WaitUntil(() => { return GameObject.FindGameObjectWithTag("Player"); });

        player = GameObject.FindGameObjectWithTag("Player");

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

        GameObject carObj = new GameObject();
        carObj.name = "CarDataManager";
        carObj.transform.parent = transform;
        car = carObj.AddComponent<CarDataManager>();

        GameObject challengeObj = new GameObject();
        challengeObj.name = "ChallengeManager";
        challengeObj.transform.parent = transform;
        challenge = challengeObj.AddComponent<ChallengeManager>();
    }
    
    private int money = 100;

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