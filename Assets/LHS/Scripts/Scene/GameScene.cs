using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] CharacterController characterController;

    Bullet bullet;

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        characterController.enabled = false;
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
        characterController.enabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
    }

    private void CreatePrefab()
    {
       for (int i = 0; i < 10; i++)
       {
            bullet = GameManager.Resource.Instantiate<Bullet>("Prefabs/Bullet", true);
            GameManager.Resource.Destroy(bullet.gameObject);
       }
    }
}
