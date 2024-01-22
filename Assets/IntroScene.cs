using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroScene : MonoBehaviour
{
    private void Start()
    {
        var video = GetComponent<VideoPlayer>().clip.length;
        StartCoroutine(StartGame((float)video));
    }
    
    private IEnumerator StartGame(float videoLength)
    {
        yield return new WaitForSeconds(videoLength);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
