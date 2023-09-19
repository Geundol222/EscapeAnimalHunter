using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalCapture : MonoBehaviour
{
    public int _BearCaptureCount;
    public int _MooseCaptureCount;
    public int _DeerCaptureCount;
    public int _TigerCaptureCount;


    public bool _isCapturing;

    private void Start()
    {
        Capture(name);
    }

    public void bb()
    {
        _isCapturing = true;
    }

    #region 동물 잡아가기
    public void Capture(string animalName)
    {
        // _BearCaptureCount  = DataManager.Challenge.bearCaptureCount;
        // _MooseCaptureCount = DataManager.Challenge.mooseCaptureCount;
        // _DeerCaptureCount  = DataManager.Challenge.deerCaptureCount;
        // _TigerCaptureCount = DataManager.Challenge.tigerCaptureCount;
        if (_isCapturing)
        {
            if (animalName == "Bear")
            {
                if (_BearCaptureCount > 0)
                {
                    _BearCaptureCount = 0;
                    Destroy(gameObject);
                }
            }
            else if (animalName == "Moose")
            {
                if (_MooseCaptureCount > 0)
                {
                    _MooseCaptureCount = 0;
                    Destroy(gameObject);
                }
            }
            else if (animalName == "Deer")
            {
                if (_DeerCaptureCount > 0)
                {
                    _DeerCaptureCount = 0;
                    Destroy(gameObject);
                }
            }
            else if (animalName == "Tiger")
            {
                if (_TigerCaptureCount > 0)
                {
                    _TigerCaptureCount = 0;
                    Destroy(gameObject);
                }
            }
            _isCapturing = false;
        }
    }

    #endregion
}
