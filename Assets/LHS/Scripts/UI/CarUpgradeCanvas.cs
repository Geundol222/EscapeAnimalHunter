using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class CarUpgradeCanvas : MonoBehaviour
{
    [SerializeField] GameObject durabilityMeter;
    [SerializeField] GameObject speedMeter;

    List<Image> durabilityImages;
    List<Image> speedImages;

    int durabilityIndex;
    int speedIndex;

    private void Awake()
    {
        for (int i = 0; i < durabilityMeter.transform.childCount; i++)
        {
            durabilityImages.Add(durabilityMeter.transform.GetChild(i).gameObject.GetComponent<Image>());
        }

        for (int i = 0; i < speedMeter.transform.childCount; i++)
        {
            speedImages.Add(speedMeter.transform.GetChild(i).gameObject.GetComponent<Image>());
        }
    }


}
