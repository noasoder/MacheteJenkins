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

    private Animator m_animator;

    private void Start()
    {
        ConvoStart();
    }
    private void Update()
    {
        ConvoUpdate();
    }
    public void ConvoStart()
    {
        DefaultStart();

        foreach (GameObject item in m_textBubbles)
        {
            item.SetActive(false);
        }

        m_animator = GetComponent<Animator>();
    }

    public void ConvoUpdate()
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
        PlayCurrentSound();
        if(m_animator)
            m_animator.SetTrigger("StartTalk");
        GlobalManager.Instance.SetCanMove(false);
    }
    //Activates the next text bubble
    public void NextTextBubble()
    {
        if(m_textBubbles.Count > m_currentBubble + 1)
        {
            m_textBubbles[m_currentBubble].SetActive(false);
            StopCurrentSound();
            m_currentBubble++;
            m_textBubbles[m_currentBubble].SetActive(true);
            PlayCurrentSound();
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
        for (int i = 0; i < m_textBubbles.Count; i++)
        {
            m_textBubbles[i].SetActive(false);
            StopSound(i);
        }
        m_currentBubble = -1;
        m_active = false;
        if (m_animator)
            m_animator.SetTrigger("StopTalk");
        GlobalManager.Instance.SetCanMove(true);
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
    private void PlayCurrentSound()
    {
        if (m_textBubbles[m_currentBubble].GetComponent<AudioSource>())
        {
            m_textBubbles[m_currentBubble].GetComponent<AudioSource>().Play();
        }
    }
    private void StopCurrentSound()
    {
        if (m_textBubbles[m_currentBubble].GetComponent<AudioSource>())
        {
            m_textBubbles[m_currentBubble].GetComponent<AudioSource>().Stop();
        }
    }
    private void PlaySound(int index)
    {
        if (m_textBubbles[index].GetComponent<AudioSource>())
        {
            m_textBubbles[index].GetComponent<AudioSource>().Play();
        }
    }
    private void StopSound(int index)
    {
        if (m_textBubbles[index].GetComponent<AudioSource>())
        {
            m_textBubbles[index].GetComponent<AudioSource>().Stop();
        }
    }
}
