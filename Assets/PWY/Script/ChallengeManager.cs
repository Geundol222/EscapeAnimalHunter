using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] GameObject Graveyard;

    private void Awake()
    {
        SetChallengeManager();
    }

    private void SetChallengeManager()
    {
        if (Graveyard == null)
        {
            Graveyard = Instantiate(Graveyard);
            Debug.Log("»Ð");
            Graveyard.name = "ChallengeManager";
        
            Init();
        }
        else
        {
            Debug.Log("À¸¾Ç");
            Destroy(Graveyard);  // Á¦°Å
        }
    }



    private void Init()
    {
        GameObject bear = new GameObject();
        bear.name = "Bear";
        bear.transform.parent = transform;

        GameObject moos = new GameObject();
        moos.name = "Moos";
        moos.transform.parent = transform;
    }

    public void Bear_()
    {

    }

    public void Moos()
    {

    }
}
