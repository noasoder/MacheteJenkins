using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_canvas;

    private void Start()
    {
        GlobalManager.Instance.IsPausedEvent.AddListener(UpdateCanvas);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalManager.Instance.SetPaused(!GlobalManager.Instance.IsPaused());
        }
    }

    private void UpdateCanvas()
    {
        if (GlobalManager.Instance.IsPaused())
        {
            m_canvas.SetActive(true);
        }
        else
        {
            m_canvas.SetActive(false);
        }
    }
}
