using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("SpawnData")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform focusPoint;                      // �÷��̾��� ���� ��ġ
    [SerializeField] private AnimalSpawnArray[] animalSpawnArray;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private SpawnManager spawnManager;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 1000)] private int maxRange;              // �÷��̾ �������� ���� ����� ������ �����ϱ����� maxRange���� minRange�� ������ ��ġ�� ����
    [SerializeField][Range(0, 1000)] private int minRange;

    private RaycastHit hitInfo;
    //private List<GameObject> curExistAnimals = new List<GameObject>();

    [Serializable]
    public class AnimalSpawnArray
    {
        public AnimalData.AnimalName animalName;                // ������ ����
        public int spawnCount;                                  // ������ ����
    }

    private void Awake()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }

    /// <summary>
    /// ��Ÿ �ٸ� ������ ��� �Ϸ�Ǹ� ����
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartSpawnRoutine());
    }

    /// <summary>
    /// ���� �� animalSpawnArray���� ���� ������ ��ŭ ���� ����
    /// </summary>
    IEnumerator StartSpawnRoutine()
    {
        foreach (AnimalSpawnArray spawnList in animalSpawnArray)                  // ����
        {
            for (int i = 0; i < spawnList.spawnCount; i++)
            {
                yield return StartCoroutine(GroundCheckRoutine());

                InstantiateAnimal(spawnList.animalName.ToString());

                yield return null;
            }
        }

        StartCoroutine(ReSpawnRoutine());

        yield break;
    }

    /// <summary>
    /// ������ �����̸� �������� ����
    /// </summary>
    /// <param name="animalName">������ ���� �̸�</param>
    private void InstantiateAnimal(string animalName)
    {
        GameObject animal = GameManager.Resource.Instantiate<GameObject>($"Prefabs/Animals/{animalName}",
            hitInfo.point, Quaternion.Euler(0, Random.Range(-180, 180), 0), false);
        //curExistAnimals.Add(animal);
        spawnManager.AddExistAnimal(animal);
        
    }

    /// <summary>
    /// ������ ��ġ�� ������ ���� ������ �����ϰ� �̾���
    /// </summary>
    private void RandomSpawnPoint()
    {
        spawnPoint.position = focusPoint.position;
        float angle = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;
        float radius = Random.Range(minRange, maxRange);
        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);
        spawnPoint.Translate(x, 20, z);
        //Debug.Log($"angle : {angle}");
        //Debug.Log($"radius : {radius}");
        //Debug.Log($"x : {x}");
        //Debug.Log($"cos : {Mathf.Cos(angle)}");
        //Debug.Log($"z : {z}");
        //Debug.Log($"sin : {Mathf.Sin(angle)}");
        //Debug.Log($"Position {spawnPoint.position}");
        //Debug.Log($"Distance : {Vector3.Distance(focusPoint.position, spawnPoint.position)}");
    }

    /// <summary>
    /// spawnPoint�� ��ġ�� GroundLayer�� �� ������ ���� ��ġ�� �̵�
    /// </summary>
    IEnumerator GroundCheckRoutine()
    {
        RaycastHit _hitInfo;
        Physics.Raycast(transform.position, Vector3.down, out _hitInfo, 50);
        hitInfo = _hitInfo;

        while (hitInfo.transform.gameObject.layer != groundLayer)
        {
            RandomSpawnPoint();

            if (Physics.Raycast(spawnPoint.position, Vector3.down, out _hitInfo, 100, groundLayer))
            {
                hitInfo = _hitInfo;

                break;
            }

            yield return new WaitForSeconds(1f);
        }

        yield break;
    }

    /// <summary>
    /// �� ������ maxRange ����� �����, ü�� �� ���� �ʱ�ȭ
    /// </summary>
    IEnumerator ReSpawnRoutine()
    {

        while (true)
        {
            //for (int i = 0; i < curExistAnimals.Count; i++)         // foreach���� InvalidOperationException �߻�
            for (int i = 0; i < spawnManager.CurExistAnimals.Count; i++)
            {
                if (DistanceCheck(spawnManager.CurExistAnimals[i]))
                {
                    yield return StartCoroutine(GroundCheckRoutine());

                    RenewalCurAnimal(spawnManager.CurExistAnimals[i]);
                }

                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(10f);
        }
    }

    /// <summary>
    /// ReSpawn���� ����ϴ� �Լ�
    /// </summary>
    /// <param name="animal">������� ����</param>
    private void RenewalCurAnimal(GameObject animal)
    {
        spawnManager.RemoveExistAnimal(animal);
        Destroy(animal);
        InstantiateAnimal(animal.name);
    }

    /// <summary>
    /// �÷��̾� �������� �󸶳� �־������� üũ
    /// </summary>
    /// <param name="animal"> üũ�� ���� </param>
    /// <returns> �ָ� true �ƴϸ� false </returns>
    private bool DistanceCheck(GameObject animal)
    {
        float distanceSquared = (animal.transform.position - focusPoint.position).sqrMagnitude;

        if (distanceSquared > maxRange * maxRange)
            return true;

        return false;
    }

    public void OnDiedAnimal(GameObject animal)
    {
        RenewalCurAnimal(animal);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(focusPoint.position, minRange);
        Gizmos.DrawWireSphere(focusPoint.position, maxRange);
    }
}