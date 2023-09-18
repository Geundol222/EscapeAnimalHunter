using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class CarSounder : MonoBehaviour
{
    [SerializeField] GameObject soundsObj;
    [SerializeField] GameObject gear;
    [SerializeField] GameObject player;

    XRSliderLEJ slider;
    SetGearState gearState;
    CarDamager damager;
    CarDriver driver;

    List<GameObject> sounds = new List<GameObject>();

    private void Awake()
    {
        slider = gear.GetComponent<XRSliderLEJ>();
        gearState = gear.GetComponent<SetGearState>();
        damager = GetComponent<CarDamager>();
        driver = GetComponent<CarDriver>();

        for (int i = 0; i < soundsObj.transform.childCount; i++)
        {
            sounds.Add(soundsObj.transform.GetChild(i).gameObject);
        }
    }

    private void OnEnable()
    {
        slider.OnStartGrab += PlayGearSoundOnce;
        gearState.OnCurGearStateChanged += MakeGearSound;
        damager.OnDie += StopEngineSound;
        driver.OnCurSpeedChanged += PlayAccelSound;
    }

    private void OnDisable()
    {
        slider.OnStartGrab -= PlayGearSoundOnce;
        gearState.GetComponent<SetGearState>().OnCurGearStateChanged -= MakeGearSound;
        damager.OnDie -= StopEngineSound;
        driver.OnCurSpeedChanged -= PlayAccelSound;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">drive, openDoor, closeDoor, gearHandle</param>
    /// <returns></returns>
    private GameObject FindSound(string name)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (Equals(sounds[i].gameObject.name, name))
            {
                return sounds[i];
            }
        }
        return null;
    }
    private void MakeGearSound(string state)
    {
        switch (state)
        {
            case "Park":
                StopEngineSound();
                break;
            case "Reverse":
            case "Neutral":
            case "Drive":
                PlayEngineSound();
                break;
        }
    }

    private void PlayAccelSound()
    {
        if (driver.CurSpeed > 1)
        {
            FindSound("drive").GetComponent<AudioSource>().Play();
            FindSound("engine").GetComponent<AudioSource>().Stop();
        }
        else
        {
            FindSound("drive").GetComponent<AudioSource>().Stop();
            FindSound("engine").GetComponent<AudioSource>().Play();
        }
    }

    private void PlayEngineSound()
    {
        FindSound("engine").GetComponent<AudioSource>().Play();
    }

    private void StopEngineSound()
    {
        FindSound("engine").GetComponent<AudioSource>().Stop();
    }

    private void PlayGearSoundOnce()
    {
        FindSound("gearHandle").GetComponent<AudioSource>().PlayOneShot(FindSound("gearHandle").GetComponent<AudioSource>().clip);
    }
}
