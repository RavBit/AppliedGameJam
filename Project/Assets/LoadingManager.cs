using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour {
    [Header("Loadingscreens: ")]
    public GameObject LoadingScreen;
    public Image LoadingBar;

    public void Next()
    {
        LoadingScreen.SetActive(true);
        LoadLevel(1);

    }

    public void LoadLevel(int sceneindex)
    {
        Debug.Log("Loadlevel");
        StartCoroutine(LoadASync(sceneindex));
    }
    public IEnumerator LoadASync(int sceneindex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBar.fillAmount = progress;
            Debug.Log(progress);
            yield return null;
        }
    }
}
