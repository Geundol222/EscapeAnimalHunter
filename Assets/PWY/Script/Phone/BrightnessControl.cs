using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessControl : MonoBehaviour
{
    [SerializeField] Slider brightness_X;
    [SerializeField] Slider brightness_Y;
    [SerializeField] Transform directionalLight;

    private float x_Brightness;
    private float y_Brightness;

    private void Start()
    {
        brightness_X.value = 90;
    }

    private void Update()
    {
        Brightness_Control();
    }

    private void Brightness_Control()
    {
        x_Brightness = brightness_X.value;
        y_Brightness = brightness_Y.value;

        directionalLight.localRotation = Quaternion.Euler(x_Brightness, y_Brightness, 0);
    }

}
