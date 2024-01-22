using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBGTimer : MonoBehaviour
{
    private Image _image;
    
    private void Start()
    {
        _image = GetComponent<Image>();
        StartCoroutine(ChangeBg());
    }
    
    private IEnumerator ChangeBg()
    {
        yield return new WaitForSeconds(0.1f);
        _image.gameObject.SetActive(false);
    }
}
