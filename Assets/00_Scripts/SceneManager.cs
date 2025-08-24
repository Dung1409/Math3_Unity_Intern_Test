using System;
using System.Collections;
using UnityEngine;
public class SceneManager : MonoBehaviour
{
    // void Awake()
    // {
    //     Observer.AddListener("LoadWinScene", LoadWinScene);
    //     Observer.AddListener("LoadLoseScene", LoadLoseScene);
    // }


    // public void LoadScene(string SCENE_NAME)
    // {
    //     StartCoroutine(WaitLoadScene(0.5F, SCENE_NAME));
    // }

    // IEnumerator WaitLoadScene(float time, string SCENE_NAME)
    // {
    //     yield return new WaitForSeconds(time);
    //     UnityEngine.SceneManagement.SceneManager.LoadScene(SCENE_NAME);
    // }

    // public void LoadWinScene() => LoadScene("WinScene");
    // public void LoadLoseScene() => LoadScene("LoseScene");
    // public void LoadGameScene() => LoadScene("Test");


    // void OnDestroy()
    // {
    //     Observer.RemoveListener("LoadWinScene", LoadWinScene);
    //     Observer.RemoveListener("LoadLoseScene", LoadLoseScene);
    // }
}
