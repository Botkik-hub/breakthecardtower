using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to change scenes from buttons or other scripts
/// </summary>
public class SceneHanler : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void TransitIn3Seconds(string sceneName)
    {
        StartCoroutine(TransitInSecondsCoroutine(sceneName, 3.0f));
    }
    
    private IEnumerator TransitInSecondsCoroutine(string sceneName, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
