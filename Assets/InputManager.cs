using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
#if UNITY_EDITOR
    private Scale _scale;
    private bool _isScaleFound;
#endif
    
    private void Start()
    {
#if UNITY_EDITOR
        _scale = FindObjectOfType<Scale>();
        if (_scale)
        {
            _isScaleFound = true;
        }
#endif
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Menu");
        }

        #if UNITY_EDITOR
            if (!_isScaleFound) return;
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _scale.PlayerDealsDamage(1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _scale.EnemyDealsDamage(1);
            }
        #endif
    }
}
