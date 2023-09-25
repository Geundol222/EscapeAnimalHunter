using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{

    [SerializeField] Image back;

    [SerializeField] List<Sprite> ground;

    [SerializeField] List<GameObject> outline;

    public void Wolf_BackGround()
    {
        back.sprite = ground[0];
        outline[0].SetActive(true);
        outline[1].SetActive(false);
        outline[2].SetActive(false);
        outline[3].SetActive(false);
        outline[4].SetActive(false);
    }

    public void Otter_BackGround()
    {
        back.sprite = ground[1];
        outline[0].SetActive(false);
        outline[1].SetActive(true);
        outline[2].SetActive(false);
        outline[3].SetActive(false);
        outline[4].SetActive(false);
    }

    public void Sea_​Otter_BackGround()
    {
        back.sprite = ground[2];
        outline[0].SetActive(false);
        outline[1].SetActive(false);
        outline[2].SetActive(true);
        outline[3].SetActive(false);
        outline[4].SetActive(false);
    }

    public void Harp_Seal()
    {
        back.sprite = ground[3];
        outline[0].SetActive(false);
        outline[1].SetActive(false);
        outline[2].SetActive(false);
        outline[3].SetActive(true);
        outline[4].SetActive(false);
    }

    public void BonoBono()
    {
        back.sprite = ground[4];
        outline[0].SetActive(false);
        outline[1].SetActive(false);
        outline[2].SetActive(false);
        outline[3].SetActive(false);
        outline[4].SetActive(true);
    }
}
