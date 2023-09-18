using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySoundPlayer : MonoBehaviour
{
    public void PlayButtonSound()
    {
        GameManager.Sound.PlaySound("TapSound");
    }
}
