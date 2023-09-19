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
    [SerializeField] private LayerMask groundLayer;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 1000)] private int maxRange;              // 플레이어를 기준으로 도넛 모양의 범위에 생성하기위해 maxRange에서 minRange를 제외한 위치에 생성
    [SerializeField][Range(0, 1000)] private int minRange;

    [Header("Debug")]
    [SerializeField] private bool isDebug;
    [SerializeField] private AnimalSpawnArray[] animalSpawnArray;

    private RaycastHit hitInfo;
    //private List<GameObject> curExistAnimals = new List<GameObject>();

    [Serializable]
    public class AnimalSpawnArray
    {
        public AnimalData.AnimalName animalName;                // 지정한 동물
        public int spawnCount;                                  // 생성할 개수
    }

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
    /// 시작 시 animalSpawnArray에서 직접 설정한 만큼 동물 생성
    /// </summary>
    IEnumerator StartSpawnRoutine()
    {
        if (isDebug)
        {
            foreach (AnimalSpawnArray animal in animalSpawnArray)                  // 생성
            {
                for (int i = 0; i < animal.spawnCount; i++)
                {
                    yield return StartCoroutine(GroundCheckRoutine());

                    InstantiateAnimal(animal.animalName.ToString());
                    
                    yield return null;
                }
            }
        }
        else
        {
            foreach (GameObject animal in SpawnManager.Spawn.AnimalsToAdd)
            {
                yield return StartCoroutine(SpawnAnimalRoutine(animal));

                yield return null;
            }
        }
        
        yield break;
    }

    /// <summary>
    /// 지정한 동물이름 기준으로 생성
    /// </summary>
    /// <param name="animalName">생성할 동물 이름</param>
    private void InstantiateAnimal(GameObject prefab)
    {
        GameObject animal = Instantiate(prefab, hitInfo.point, Quaternion.Euler(0, Random.Range(-180, 180), 0));
        SpawnManager.Spawn.AddExistAnimal(animal);
    }

    private void InstantiateAnimal(string animalName)
    {
        GameObject animal = GameManager.Resource.Instantiate<GameObject>($"Prefabs/Animals/{animalName}",
            hitInfo.point, Quaternion.Euler(0, Random.Range(-180, 180), 0), false);
        SpawnManager.Spawn.AddExistAnimal(animal);
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

    IEnumerator SpawnAnimalRoutine(GameObject animal)
    {
        yield return StartCoroutine(GroundCheckRoutine());

        InstantiateAnimal(animal);

        yield break;
    }

    //public void OnDiedAnimal(GameObject animal)
    //{
    //    SpawnManager.Spawn.RemoveExistAnimal(animal);
    //    Destroy(animal);
    //    StartCoroutine(SpawnAnimalRoutine(animal));
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(focusPoint.position, minRange);
        Gizmos.DrawWireSphere(focusPoint.position, maxRange);
    }
}