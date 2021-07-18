using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Clue : ConversationSystem
{
    [SerializeField] private GameObject m_clueUI;

    private State m_state = State.Invisible;
    [SerializeField] private Desk.Clues m_clueID;
    [SerializeField] private bool m_deactivateWhenClosed = false;

    private AudioSource m_clueAudio;
    private SpriteRenderer m_spriteR;

    [SerializeField] private GameObject m_additionalVisual;
    [SerializeField] private bool m_hideOnRange = false;

    enum State
    {
        Closed = 0,
        Open, 
        Invisible,
    }

    void Start()
    {
        ConvoStart();

        m_clueAudio = GetComponent<AudioSource>();
        m_spriteR = GetComponent<SpriteRenderer>();

        if(m_deactivateWhenClosed && GlobalManager.Instance.HasFoundClue(m_clueID))
        {
            gameObject.SetActive(false);
        }

        if (m_hideOnRange)
        {
            if (m_additionalVisual)
                m_additionalVisual.SetActive(false);
            m_spriteR.enabled = false;

            m_state = State.Invisible;
        }
        else
            m_state = State.Closed;
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
                case State.Invisible:
                    OnStateInvisible();
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

    void OnStateInvisible()
    {
        if (GetIsHighlighted())
        {
            m_spriteR.enabled = true;
            if (m_additionalVisual)
                m_additionalVisual.SetActive(true);
            m_state = State.Closed;
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
