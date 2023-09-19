using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] GameObject loadingSceneUI;

    Material loadingMat;

    float fadeduration = 1f;
    float lerpTime;

    private void Awake()
    {
        loadingMat = GetComponent<Renderer>().material;
    }

    public void FadeOut()
    {
        StartCoroutine(TransparentRoutine(0f, 1f));
    }

    public void FadeIn()
    {
        StartCoroutine(TransparentRoutine(1f, 0f));
    }

    IEnumerator TransparentRoutine(float first, float second)
    {
        lerpTime = 0;

        while (lerpTime < fadeduration)
        {
            lerpTime += Time.deltaTime * 3f;

            Color color = loadingMat.color;
            color.a = Mathf.Lerp(first, second, lerpTime / fadeduration);

            loadingMat.color = color;
            yield return new WaitForEndOfFrame();
        }

        if (loadingMat.color.a >= 0.9f)
        {
            loadingSceneUI.SetActive(true);
        }
        else if (loadingMat.color.a <= 0.1f)
        {
            loadingSceneUI.SetActive(false);
        }
    }
}
