using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform playerPoint;
    [SerializeField] LayerMask groundMask;

    [SerializeField] int minDistance;
    [SerializeField] int maxDistance;

    private void Start()
    {
        StartCoroutine(AnimalSpawnRoutine());
    }

    IEnumerator AnimalSpawnRoutine()
    {
        while (true)
        {
            int randomValue = Random.Range(0, 360);

            Quaternion randomRotation = Quaternion.Euler(spawnPoint.rotation.x, spawnPoint.rotation.y + randomValue, spawnPoint.rotation.z);
            spawnPoint.rotation = randomRotation;

            spawnPoint.position = spawnPoint.forward * Random.Range(minDistance, maxDistance) + new Vector3(playerPoint.position.x, 40, playerPoint.position.z);

            RaycastHit hit;

            if (Physics.Raycast(spawnPoint.position, Vector3.down, out hit, 50))
            {
                if (groundMask.IsContain(hit.collider.gameObject.layer))
                {
                    GameManager.Resource.Instantiate<GameObject>("Prefabs/Cube", hit.point, Quaternion.Euler(hit.normal));
                }
                else
                {
                    Debug.Log("NoSpawnAnimal Not Found Ground");
                    continue;
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerPoint.position, minDistance);
        Gizmos.DrawWireSphere(playerPoint.position, maxDistance);
    }
}
