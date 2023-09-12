using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarDecorator : MonoBehaviour
{
    [SerializeField] GameObject materialObj; //contains 5 objects

    [SerializeField] Dictionary<string, Material> materials = new Dictionary<string, Material>();
    [SerializeField] Dictionary<string, Color> colors = new Dictionary<string, Color>();

    public Material FindMaterial(string name)
    {
        return materials[name];
    }

    public Color FindColor(string name)
    {
        return colors[name];
    }

    public void ChangeMaterial(Material whichMat)
    {
        for (int i = 0; i < 5; i++)
        {
            materialObj.transform.GetChild(i).GetComponent<Renderer>().material = whichMat;
        }
    }

    public void ChangeColor(Color color)
    {
        for (int i = 0; i < 5; i++)
        {
            materialObj.transform.GetChild(i).GetComponent<Renderer>().material.color = color;
        }
    }
}
