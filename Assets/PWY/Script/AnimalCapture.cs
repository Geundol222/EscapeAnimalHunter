using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalCapture : MonoBehaviour
{
    #region ���� ��ư���
    public void Capture()
    {
        DataManager.Challenge.Capture();
    }
    #endregion
}
