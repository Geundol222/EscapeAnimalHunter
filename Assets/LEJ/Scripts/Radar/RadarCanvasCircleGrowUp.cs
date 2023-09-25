using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarCanvasCircleGrowUp : MonoBehaviour
{
    [SerializeField] GameObject circle; //scale 0 ~ 1
    [SerializeField] SphereCasterNew caster;
    float circleCurScale;

    private void Update()
    {
        circleCurScale = caster.curRadius / caster.radiusMax ;

        circle.GetComponent<RectTransform>().localScale = new Vector3(circleCurScale, circleCurScale, circleCurScale);
    }
}
