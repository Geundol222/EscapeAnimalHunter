using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colider : MonoBehaviour
{
    [SerializeField] new colider col;

    private void Start()
    {
        col = GetComponent<colider>();
    }
}
