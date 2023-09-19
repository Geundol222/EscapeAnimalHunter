using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarDecorator : MonoBehaviour
{

    [SerializeField] GameObject materialObj; //contains 5 objects

    [SerializeField] Material solidMat;

    [SerializeField] List<string> colorsName = new List<string>();
    [SerializeField] List<Color> colors = new List<Color>();

    [SerializeField] List<string> materialsName = new List<string>();
    [SerializeField] List<Material> materials = new List<Material>();

    int index;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">giraffe, leopard1, leopard2, zebra, carbon</param>
    public void ChangeMaterial(string name)
    {
        for (int i = 0; i < materialsName.Count; i++)
        {
            if (Equals(materialsName[i], name))
            {
                index = i;
                break;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            materialObj.transform.GetChild(i).GetComponent<Renderer>().material = materials[index];
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">black, white, silver, gold, green, red, blue, cyan, hotPink, pink</param>
    public void ChangeColor(string name)
    {
        for (int i = 0; i < colorsName.Count; i++)
        {
            if (Equals(colorsName[i], name))
            {
                index = i;
                break;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            materialObj.transform.GetChild(i).GetComponent<Renderer>().material = solidMat;
            materialObj.transform.GetChild(i).GetComponent<Renderer>().material.color = colors[index];
        }
    }

    /// <summary>
    /// isMetalic이 true면 광택, false면 매트
    /// </summary>
    /// <param name="isMetalic"></param>
    public void ChangeMetalic(bool isMetalic)
    {
        if (isMetalic)
        {
            for (int i = 0; i < 5; i++)
            {
                materialObj.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.7f);
                materialObj.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_Metallic", 0.2f);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                materialObj.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.3f);
                materialObj.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_Metallic", 0.3f);
            }
        }
    }
}
