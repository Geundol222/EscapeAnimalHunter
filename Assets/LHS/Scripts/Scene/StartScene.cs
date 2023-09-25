using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private void Start()
    {
        GameManager.Sound.PlaySound("BGM", Audio.BGM, 0.7f);
    }
}
