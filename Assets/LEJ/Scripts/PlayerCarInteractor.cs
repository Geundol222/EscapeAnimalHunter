using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarInteractor : MonoBehaviour
{
    [SerializeField] Transform carPlayerPosition;
    [SerializeField] Transform characterControllerObj;
    public bool isPlayerTakingCar;
    [SerializeField] bool autoStart;
    [SerializeField] GameObject locomotionTurn;

    public void Start()
    {
        if (autoStart)
            Interact();
    }

    public void Interact()
    {
        isPlayerTakingCar = !isPlayerTakingCar;

        locomotionTurn.SetActive(false);

        characterControllerObj.GetComponent<CharacterController>().enabled = false;
        transform.SetParent(carPlayerPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void DisInteract()
    {
        isPlayerTakingCar = !isPlayerTakingCar;

        locomotionTurn.SetActive(true);

        characterControllerObj.GetComponent<CharacterController>().enabled = true;
        transform.SetParent(null);
    }

}
