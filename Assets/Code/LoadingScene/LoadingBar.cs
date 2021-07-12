using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private Image m_image;
    private void Awake()
    {
        m_image = GetComponent<Image>();
        
    }

    void Update()
    {
        if(m_image)
        {
            m_image.fillAmount = SceneLoader.GetLoadingProgress();
        }
    }
}
