using UnityEngine;
using UnityEngine.Events;

public class WinTrigger : MonoBehaviour
{
    public UnityEvent onWin;
    
    public void Activate()
    {
        onWin.Invoke();
    }
}
