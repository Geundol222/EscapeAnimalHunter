using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    private Camera mainCam;
    private LoadingUI loadingUI;
    private int sceneIndex = 0;
    public int SceneIndex { get { return sceneIndex; } }

    private BaseScene curScene;
    public BaseScene CurScene
    {
        get
        {
            if (curScene == null)
                curScene = FindObjectOfType<BaseScene>();

            return curScene;
        }
    }

    private void Start()
    {
        LoadingUI ui = GameManager.Resource.Load<LoadingUI>("UI/LoadingSphere");
        loadingUI = Instantiate(ui);
        loadingUI.transform.SetParent(transform, false);
    }

    public void LoadScene(int index)
    {
        sceneIndex = index;
        StartCoroutine(LoadingRoutine(index));
    }

    IEnumerator LoadingRoutine(int index)
    {
        mainCam = Camera.main;

        loadingUI.transform.position = mainCam.transform.position;
        loadingUI.transform.rotation = mainCam.transform.rotation;

        loadingUI.FadeOut();
        GameManager.Sound.Clear();
        yield return new WaitUntil(() => { return GameManager.Sound.IsMuted(); });
        Time.timeScale = 0f;

        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(index);

        while (!oper.isDone)
        {
            yield return null;
        }

        GameManager.Pool.InitPool();
        GameManager.Sound.InitSound();
        GameManager.Sound.FadeInAudio();

        CurScene.LoadAsync();
        while (CurScene.progress < 1f)
        {
            yield return null;
        }

        Time.timeScale = 1f;
        yield return new WaitWhile(() => { return GameManager.Sound.IsMuted(); });
        loadingUI.FadeIn();
    }
}