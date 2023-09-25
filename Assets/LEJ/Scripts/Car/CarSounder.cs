using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        damager.OnDie += StopAccelSound;
        driver.OnCurSpeedChanged += PlaySound;
    }

    private void OnDisable()
    {
        slider.OnStartGrab -= PlayGearSoundOnce;
        gearState.GetComponent<SetGearState>().OnCurGearStateChanged -= MakeGearSound;
        damager.OnDie -= StopEngineSound;
        damager.OnDie -= StopAccelSound;
        driver.OnCurSpeedChanged -= PlaySound;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">drive, openDoor, closeDoor, gearHandle, accel</param>
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

    private void PlaySound()
    {
        if (driver.CurSpeed >= 1f && !FindSound("accel").GetComponent<AudioSource>().isPlaying)
        {
            PlayAccelSound();
            StopEngineSound();
        }
        
        if (driver.CurSpeed < 1f && FindSound("accel").GetComponent<AudioSource>().isPlaying)
        {
            StopAccelSound();
            PlayEngineSound();
        }
    }

    private void PlayAccelSound()
    {
        if (!FindSound("accel").GetComponent<AudioSource>().isPlaying)
            FindSound("accel").GetComponent<AudioSource>().Play();
    }
    private void StopAccelSound()
    {
        if (FindSound("accel").GetComponent<AudioSource>().isPlaying)
            FindSound("accel").GetComponent<AudioSource>().Stop();
    }

    private void PlayEngineSound()
    {
        if (!FindSound("engine").GetComponent<AudioSource>().isPlaying)
            FindSound("engine").GetComponent<AudioSource>().Play();
    }

    private void StopEngineSound()
    {
        if (FindSound("engine").GetComponent<AudioSource>().isPlaying)
            FindSound("engine").GetComponent<AudioSource>().Stop();
    }

    private void PlayGearSoundOnce()
    {
        FindSound("gearHandle").GetComponent<AudioSource>().PlayOneShot(FindSound("gearHandle").GetComponent<AudioSource>().clip);
    }
}
