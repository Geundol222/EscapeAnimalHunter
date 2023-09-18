using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 0.2f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 0.4f;
        CreatePrefab();
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 0.6f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 1f;
        GameManager.Sound.PlaySound("BGM", Audio.BGM, 0.7f);
        yield return new WaitForSecondsRealtime(0.1f);
    }

    private void CreatePrefab()
    {

    }
}
