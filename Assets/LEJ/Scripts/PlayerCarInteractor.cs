using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarInteractor : MonoBehaviour
{
    public bool isPlayerTakingCar;

    public void Interact()
    {
        isPlayerTakingCar = !isPlayerTakingCar;
    }

}
