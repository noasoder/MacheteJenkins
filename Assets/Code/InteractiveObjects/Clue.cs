using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Clue : ConversationSystem
{
    [SerializeField] private GameObject m_clueUI;

    private State m_state = State.Closed;
    [SerializeField] private Desk.Clues m_clueID;
    [SerializeField] private bool m_deactivateWhenClosed = false;

    private AudioSource m_clueAudio;

    enum State
    {
        Closed = 0,
        Open, 
        Used, //might be removed
    }

    void Start()
    {
        ConvoStart();

        m_clueAudio = GetComponent<AudioSource>();

        if(m_deactivateWhenClosed && GlobalManager.Instance.HasFoundClue(m_clueID))
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        ConvoUpdate();

        if(!GlobalManager.Instance.IsPaused())
        {
            switch (m_state)
            {
                case State.Closed:
                    OnStateClosed();
                    break;
                case State.Open:
                    OnStateOpen();
                    break;
                case State.Used:
                    break;
                default:
                    Debug.Log("No state on clue!!!");
                    break;
            }
        }
    }

    void OnStateClosed()
    {
        if (Input.GetKeyDown(KeyCode.E) && GetIsHighlighted())
        {
            OpenClue();
        }
        if(Input.GetMouseButton(0) && GetIsHighlighted() && GetMode() == Mode.MouseHover)
        {
            OpenClue();
        }
    }

    void OnStateOpen()
    {

    }

    void OpenClue()
    {
        if(!m_clueAudio.isPlaying)
            m_clueAudio.Play();
        StartConversation();
        GlobalManager.Instance.AddFoundClue(GetClueID());
    }

    public void CloseClue()
    {
        if (m_clueUI)
            m_clueUI.SetActive(false);
        if(m_deactivateWhenClosed)
        {
            gameObject.SetActive(false);
        }
    }

    public Desk.Clues GetClueID()
    {
        return m_clueID;
    }
}
