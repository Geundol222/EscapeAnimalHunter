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
    private LayerMask groundLayer;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 1000)] private int maxRange;              // �÷��̾ �������� ���� ����� ������ �����ϱ����� maxRange���� minRange�� ������ ��ġ�� ����
    [SerializeField][Range(0, 1000)] private int minRange;

    private RaycastHit hitInfo;

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
    /// isDebug = ture�̸� animalSpawnArray���� ���� ������ ��ŭ ���� ����
    /// false�� ����ġ���� ���� ����
    /// </summary>
    IEnumerator StartSpawnRoutine()
    {
        foreach (GameObject animal in SpawnManager.Spawn.AnimalsToAdd)
        {
            yield return StartCoroutine(SpawnAnimalRoutine(animal));
        
            yield return new WaitForSeconds(30f);
        }

        StartCoroutine(ReSpawnRoutine());

        yield break;
    }

    /// <summary>
    /// ������ ���� ����
    /// </summary>
    /// <param name="prefab">���� ������ Prefab</param>
    private void InstantiateAnimal(GameObject prefab)
    {
        GameObject animal = Instantiate(prefab, hitInfo.point, Quaternion.Euler(0, Random.Range(-180, 180), 0));
        SpawnManager.Spawn.CurExistAnimals.Add(animal);
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
            for (int i = 0; i < SpawnManager.Spawn.CurExistAnimals.Count; i++)
            {
                if (DistanceCheck(SpawnManager.Spawn.CurExistAnimals[i]))
                {
                    yield return StartCoroutine(GroundCheckRoutine());

                    SpawnManager.Spawn.ReSpawnAniaml((SpawnManager.Spawn.CurExistAnimals[i]));
                }

                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(3f);
        }
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

    /// <summary>
    /// ������ ������Ű�� Coroutine, ���� SpawnRoutine���� �и�����
    /// </summary>
    /// <param name="animal">������ ���� Prefab</param>
    /// <returns></returns>
    IEnumerator SpawnAnimalRoutine(GameObject animal)
    {
        yield return StartCoroutine(GroundCheckRoutine());

        InstantiateAnimal(animal);

        yield break;
    }

    /// <summary>
    /// SpawnManager���� ������ Destroy�Ǹ� ȣ���� �Լ�
    /// </summary>
    /// <param name="animal">������ ���� �̸�</param>
    public void SpawnAnimal(GameObject animal)
    {
        StartCoroutine(SpawnAnimalRoutine(animal));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(focusPoint.position, minRange);
        Gizmos.DrawWireSphere(focusPoint.position, maxRange);
    }
}