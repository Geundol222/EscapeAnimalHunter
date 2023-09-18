using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("SpawnData")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform focusPoint;                      // 플레이어의 시작 위치
    [SerializeField] AnimalSpawnArray[] animalSpawnArray;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 1000)] int maxRange;              // 플레이어를 기준으로 도넛 모양의 범위에 생성하기위해 maxRange에서 minRange를 제외한 위치에 생성
    [SerializeField][Range(0, 1000)] int minRange;
    [SerializeField] LayerMask groundLayer;

    private RaycastHit hitInfo;
    private List<GameObject> curExistAnimals = new List<GameObject>();

    [Serializable]
    public class AnimalSpawnArray
    {
        public AnimalData.AnimalName animalName;                // 지정한 동물
        public int spawnCount;                                  // 생성할 개수
    }

    /// <summary>
    /// 기타 다른 설정들 모두 완료되면 생성
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartSpawnRoutine());
    }

    /// <summary>
    /// 시작 시 animalSpawnArray에서 직접 설정한 만큼 동물 생성
    /// </summary>
    IEnumerator StartSpawnRoutine()
    {
        foreach (AnimalSpawnArray spawnList in animalSpawnArray)                  // 생성
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
    /// 지정한 동물이름 기준으로 생성
    /// </summary>
    /// <param name="animalName">생성할 동물 이름</param>
    private void InstantiateAnimal(string animalName)
    {
        GameObject animal = GameManager.Resource.Instantiate<GameObject>($"Prefabs/Animals/{animalName}",
            hitInfo.point, Quaternion.Euler(0, Random.Range(-180, 180), 0), true);
        curExistAnimals.Add(animal);
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
            for (int i = 0; i < curExistAnimals.Count; i++)         // foreach쓰면 InvalidOperationException 발생
            {
                if (DistanceCheck(curExistAnimals[i]))
                {
                    yield return StartCoroutine(GroundCheckRoutine());
                    Debug.Log("동물이 범위 벗어남");
                    RenewalCurAnimal(curExistAnimals[i]);
                }

                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(10f);
        }
    }

    /// <summary>
    /// ReSpawn에서 사용하는 함수
    /// </summary>
    /// <param name="animal">재생성할 동물</param>
    private void RenewalCurAnimal(GameObject animal)
    {
        curExistAnimals.Remove(animal);
        GameManager.Resource.Destroy(animal);
        InstantiateAnimal(animal.name);
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

    public void OnDiedAnimal(GameObject animal)
    {
        Debug.Log(animal.name);
        //RenewalCurAnimal(animal);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(focusPoint.position, minRange);
        Gizmos.DrawWireSphere(focusPoint.position, maxRange);
    }
}