using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    //[SerializeField] private bool m_rollCredits;
    //[SerializeField] private GameObject m_rollingCredits;
    [SerializeField] private GameObject m_creditsUI;
    //[SerializeField] private float m_creditsSpeed;
    //private float m_lerp = 0;

    //[SerializeField] private float m_rollStartPos = 0;
    //[SerializeField] private float m_rollEndPos = 1000;

    
    void Start()
    {
        GlobalManager.Instance.RollCreditsEvent.AddListener(RollCredits);
    }

    //void Update()
    //{
    //    if(m_rollCredits)
    //    {
    //        m_rollingCredits.transform.position = new Vector3(m_rollingCredits.transform.position.x,
    //                                                            Mathf.Lerp(m_rollStartPos, m_rollEndPos, m_lerp),
    //                                                            m_rollingCredits.transform.position.z);
    //        m_lerp += m_creditsSpeed * Time.deltaTime;

    //        if (m_lerp >= 1.0f)
    //        {
    //            //GlobalManager.Instance.m_helperFunctions.LoadScene("MainMenu");
    //        }
    //    }
    //}

    public void RollCredits()
    {
        //m_rollCredits = true;

        m_creditsUI.SetActive(true);
    }
    public void CloseCredits()
    {
        m_creditsUI.SetActive(false);
    }
}
