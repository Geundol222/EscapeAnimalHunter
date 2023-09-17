using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] Image slider;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        anim.Play("LoadingFadeIn");
        slider.fillAmount = 0;
        slider.gameObject.SetActive(false);
    }

    public void FadeOut()
    {
        anim.Play("LoadingFadeOut");
        slider.gameObject.SetActive(true);
    }

    public void SetProgress(float progress)
    {
        slider.fillAmount = progress;
    }
}
