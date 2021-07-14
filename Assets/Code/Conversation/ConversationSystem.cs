using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationSystem : InteractiveObject
{
    [SerializeField] private List<GameObject> m_textBubbles;

    [SerializeField] private int m_currentBubble = -1;
    private bool m_active = false;
    [SerializeField] private bool m_disableAfterUse = false;
    [SerializeField] private bool m_useTriggerToActivate = false;

    void Start()
    {
        DefaultStart();

        foreach (GameObject item in m_textBubbles)
        {
            item.SetActive(false);
        }
    }

    private void Update()
    {
        DefaultUpdate();

        if(!m_active && Input.GetKeyDown(KeyCode.E) && GetIsHighlighted())
        {
            StartConversation();
        }
    }
    public void StartConversation()
    {
        m_active = true;
        m_currentBubble = 0;
        m_textBubbles[m_currentBubble].SetActive(true);
    }
    //Activates the next text bubble
    public void NextTextBubble()
    {
        if(m_textBubbles.Count > m_currentBubble + 1)
        {
            m_textBubbles[m_currentBubble].SetActive(false);
            m_currentBubble++;
            m_textBubbles[m_currentBubble].SetActive(true);
        }
        else
        {
            //conversation is over
            ResetConversation();
            EnableNextInStory();
            if(m_disableAfterUse)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void ResetConversation()
    {
        foreach (GameObject item in m_textBubbles)
        {
            item.SetActive(false);
        }
        m_currentBubble = -1;
        m_active = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (!m_active && m_useTriggerToActivate)
            {
                StartConversation();
            }
        }
    }
}
