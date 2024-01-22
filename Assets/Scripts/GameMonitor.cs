using UnityEngine;

public class GameMonitor : MonoBehaviour
{
    public static GameMonitor Instance;
    public EGameState state;
    //public bool isPaused = false;
    public GameObject pausePanel;

    public GameObject cardToPlay;
    public bool cardOnDragging;
    public GameObject hoverDisplay;
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    { 
        state = EGameState.PlayerTurn;
        pausePanel.SetActive(false);
        hoverDisplay = GameObject.Find("Hover Display");
    }

    public void SetPausePanel(bool value)
    {
        pausePanel.SetActive(value);
        state = value ? EGameState.Pause : EGameState.PlayerTurn;
    }
}
