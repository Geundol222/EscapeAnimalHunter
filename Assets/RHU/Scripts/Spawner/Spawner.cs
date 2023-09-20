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
    [SerializeField] private Transform focusPoint;                      // 플레이어의 시작 위치
    private LayerMask groundLayer;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 1000)] private int maxRange;              // 플레이어를 기준으로 도넛 모양의 범위에 생성하기위해 maxRange에서 minRange를 제외한 위치에 생성
    [SerializeField][Range(0, 1000)] private int minRange;

    private RaycastHit hitInfo;

    private void Awake()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }

    /// <summary>
    /// 기타 다른 설정들 모두 완료되면 생성
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartSpawnRoutine());
    }

    /// <summary>
    /// isDebug = ture이면 animalSpawnArray에서 직접 설정한 만큼 동물 생성
    /// false면 가중치기준 랜덤 생성
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
    /// 지정한 동물 생성
    /// </summary>
    /// <param name="prefab">만들 동물의 Prefab</param>
    private void InstantiateAnimal(GameObject prefab)
    {
        GameObject animal = Instantiate(prefab, hitInfo.point, Quaternion.Euler(0, Random.Range(-180, 180), 0));
        SpawnManager.Spawn.CurExistAnimals.Add(animal);
    }

    /// <summary>
    /// 스폰될 위치를 지정된 범위 내에서 랜덤하게 뽑아줌
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
    /// spawnPoint의 위치를 GroundLayer가 될 때까지 랜덤 위치로 이동
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
    /// 각 동물이 maxRange 벗어나면 재생성, 체력 및 상태 초기화
    /// </summary>
    IEnumerator ReSpawnRoutine()
    {
        while (true)
        {
            //for (int i = 0; i < curExistAnimals.Count; i++)         // foreach쓰면 InvalidOperationException 발생
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
    /// 플레이어 기준으로 얼마나 멀어졌는지 체크
    /// </summary>
    /// <param name="animal"> 체크할 동물 </param>
    /// <returns> 멀면 true 아니면 false </returns>
    private bool DistanceCheck(GameObject animal)
    {
        float distanceSquared = (animal.transform.position - focusPoint.position).sqrMagnitude;

        if (distanceSquared > maxRange * maxRange)
            return true;

        return false;
    }

    /// <summary>
    /// 동물을 생성시키는 Coroutine, 기존 SpawnRoutine에서 분리구현
    /// </summary>
    /// <param name="animal">생성할 동물 Prefab</param>
    /// <returns></returns>
    IEnumerator SpawnAnimalRoutine(GameObject animal)
    {
        yield return StartCoroutine(GroundCheckRoutine());

        InstantiateAnimal(animal);

        yield break;
    }

    /// <summary>
    /// SpawnManager에서 동물이 Destroy되면 호출할 함수
    /// </summary>
    /// <param name="animal">생성할 동물 이름</param>
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