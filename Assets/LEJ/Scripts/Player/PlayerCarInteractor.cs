using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarInteractor : MonoBehaviour
{
    [SerializeField] Transform carPlayerPosition;
    [SerializeField] Transform characterControllerObj;
    public bool isPlayerTakingCar;
    [SerializeField] bool autoStart;

    public void Start()
    {
        if (autoStart)
            Interact();
    }

    public void Update()
    {
        if (isPlayerTakingCar)
        {
            transform.position = carPlayerPosition.position;
            transform.rotation = carPlayerPosition.rotation;
        }
    }

    public void Interact()
    {
        isPlayerTakingCar = true;

        transform.SetParent(carPlayerPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.SetParent(null);
    }

    public void DisInteract()
    {
        isPlayerTakingCar = false;

    }

}
