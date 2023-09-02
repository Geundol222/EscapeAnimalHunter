using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarInteractor : MonoBehaviour
{
    [SerializeField] Transform carPlayerPosition;
    [SerializeField] Transform characterControllerObj;
    public bool isPlayerTakingCar;

    public void Interact()
    {
        isPlayerTakingCar = !isPlayerTakingCar;

        characterControllerObj.GetComponent<CharacterController>().enabled = false;
        transform.SetParent(carPlayerPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

}
