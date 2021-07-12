using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private GameObject m_target;

    [SerializeField] private Vector3 m_offset;
    [SerializeField] private float m_lerpSpeed = 1f;

    void Start()
    {
        if(!m_target)
        {
            m_target = GameObject.FindWithTag("Player");
        }
    }


    void LateUpdate()
    {
        if(m_target && !GlobalManager.Instance.IsPaused())
        {
            transform.position = Vector3.Lerp(transform.position, m_target.transform.position + m_offset, m_lerpSpeed * Time.deltaTime);
        }
    }
}
