using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class MoveSoundPlayer : MonoBehaviour
{
    DynamicMoveProvider dynamicMoveProvider;

    private void Awake()
    {
        dynamicMoveProvider = GetComponent<DynamicMoveProvider>();
    }

    private void Start()
    {
        StartCoroutine(MoveSoundPlayRoutine());
    }

    IEnumerator MoveSoundPlayRoutine()
    {
        while (true)
        {
            if (dynamicMoveProvider != null)
            {
                if (dynamicMoveProvider.locomotionPhase == LocomotionPhase.Moving)
                    GameManager.Sound.PlaySound("Walk1");

                yield return new WaitForSeconds(0.7f);

                if (dynamicMoveProvider.locomotionPhase == LocomotionPhase.Moving)
                    GameManager.Sound.PlaySound("Walk2");
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();

    }
}
