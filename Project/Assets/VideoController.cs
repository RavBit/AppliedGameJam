using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour {
    public VideoPlayer VP;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    public void Play()
    {
        VP.Stop();
        VP.Play();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
