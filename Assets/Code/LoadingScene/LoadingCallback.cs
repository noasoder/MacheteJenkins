using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCallback : MonoBehaviour
{
    bool m_isDone = false;
    void Update()
    {
        if(!m_isDone)
        {
            SceneLoader.LoaderCB();
            m_isDone = true;
        }
    }
}
