using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{

    [SerializeField] Image back;

    [SerializeField] List<Sprite> ground;

    public void Wolf_BackGround()
    {
        back.sprite = ground[0];
    }

    public void Otter_BackGround()
    {
        back.sprite = ground[1];
    }

    public void Sea_​Otter_BackGround()
    {
        back.sprite = ground[2];
    }

    public void Harp_Seal()
    {
        back.sprite = ground[3];
    }

    public void BonoBono()
    {
        back.sprite = ground[4];
    }
}
