using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RadarCanvas : MonoBehaviour
{
    [SerializeField] Transform car;
    [SerializeField] SphereCasterNew sphereCaster;
    [SerializeField] GameObject circlePrefab;

    [SerializeField] GameObject[] circles = new GameObject[10];
    int index;

    private void Awake()
    {
        MakePool();
        index = 0;
    }

    private void MakePool()
    {
        for (int i = 0; i < 10; i++)
        {
            circles[i] = CreateNewCircle();
        }
    }

    private GameObject CreateNewCircle()
    {
        GameObject circle = Instantiate(circlePrefab, Vector3.zero, transform.rotation, transform);
        circle.gameObject.transform.localPosition = Vector3.zero;
        circle.gameObject.SetActive(false);
        return circle;
    }

    private void OnEnable()
    {
        sphereCaster.OnDetect += MakePoint;
    }

    private void OnDisable()
    {
        sphereCaster.OnDetect -= MakePoint;
    }

    Coroutine returnCircleRoutine;
    private void MakePoint(Transform animalPosition)
    {
        circles[index].SetActive(true);
        circles[index].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3((car.position.x - animalPosition.position.x) * 0.002f, (animalPosition.position.z - car.position.z) * 0.007f, 0f);
        returnCircleRoutine = StartCoroutine(ReturnCircle());
        index++;
        if (index >= circles.Length)
            index = 0;
    }

    IEnumerator ReturnCircle()
    {
        yield return new WaitForSeconds(0.002f);
        circles[index].gameObject.SetActive(false);
    }

}
