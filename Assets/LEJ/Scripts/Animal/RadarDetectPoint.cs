using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDetectPoint : MonoBehaviour
{
    [SerializeField] public GameObject detectPointObj;
    [SerializeField] float setActiveTime; //0.1f

    private void Start()
    {
        detectPointObj.transform.gameObject.SetActive(false);
    }
    public void SetActive()
    {
        if (!detectPointObj.activeInHierarchy && setActiveRoutine == null)
        {
            setActiveRoutine = StartCoroutine(SetActiveTime());
            setActiveRoutine = null;
        }
    }

    public Coroutine setActiveRoutine;

    IEnumerator SetActiveTime()
    {
        detectPointObj.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(setActiveTime);
        detectPointObj.transform.gameObject.SetActive(false);
    }
}
