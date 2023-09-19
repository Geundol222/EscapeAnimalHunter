using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RadarCanvas : MonoBehaviour
{
    [SerializeField] Transform car;
    [SerializeField] SphereCasterNew sphereCaster;
    [SerializeField] GameObject pointGreen;

    [SerializeField] GameObject[] circles = new GameObject[10];
    int index;
    int layerMaskForCarnivore;

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
        GameObject circle = Instantiate(pointGreen, Vector3.zero, transform.rotation, transform);
        circle.gameObject.transform.localPosition = Vector3.zero;
        circle.gameObject.SetActive(false);
        return circle;
    }

    private void OnEnable()
    {
        sphereCaster.OnDetectHerbivore += MakePoint;
        sphereCaster.OnDetectCarnivore += MakePoint;
        sphereCaster.OnReset += ResetCircles;
    }

    private void OnDisable()
    {
        sphereCaster.OnDetectHerbivore -= MakePoint;
        sphereCaster.OnDetectCarnivore -= MakePoint;
        sphereCaster.OnReset -= ResetCircles;
    }

    Coroutine returnCircleRoutine;
    private void MakePoint(Transform animalPosition, bool isCarnivore)
    {
        circles[index].SetActive(true);
        circles[index].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3((car.position.x - animalPosition.position.x) * (0.02f / sphereCaster.radiusMax), (animalPosition.position.z - car.position.z) * (0.07f / sphereCaster.radiusMax), 0f);
        
        if (isCarnivore)
            circles[index].gameObject.GetComponent<Image>().color = Color.red;
        else
            circles[index].gameObject.GetComponent<Image>().color = Color.green;

        returnCircleRoutine = StartCoroutine(ReturnCircle());

        index++;
        if (index >= circles.Length)
            index = 0;
    }

    private void ResetCircles()
    {
        for (int i = circles.Length - 1; i > -1; i--)
            circles[i].SetActive(false);
    }

    IEnumerator ReturnCircle()
    {
        yield return new WaitForSeconds(0.002f);
        
        circles[index].gameObject.SetActive(false);
    }

}
