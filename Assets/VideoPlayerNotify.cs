using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoPlayerNotify : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    
    [SerializeField] private UnityEvent onVideoEnd;
    
    private void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }
    
    public void PlayVideo()
    {
        _videoPlayer.Play();
        StartCoroutine(StartVideo((float)_videoPlayer.clip.length));
    }
    
    private IEnumerator StartVideo(float videoLength)
    {
        yield return new WaitForSeconds(videoLength);
        onVideoEnd.Invoke();
    }
}
