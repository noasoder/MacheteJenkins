using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : InteractiveObject
{
    [SerializeField] private GameObject m_clueUI;

    private State m_state = State.Closed;
    [SerializeField] private Desk.Clues m_clueID;

    enum State
    {
        Closed = 0,
        Open, 
        Used, //might be removed
    }

    void Start()
    {
        DefaultStart();
    }

    void Update()
    {
        if(!GlobalManager.Instance.m_isPaused)
        {
            DefaultUpdate();

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
    }

    void OnStateOpen()
    {

    }

    void OpenClue()
    {
        m_clueUI.SetActive(true);
        GlobalManager.Instance.AddFoundClue(GetClueID());
    }

    public void CloseClue()
    {
        m_clueUI.SetActive(false);
    }

    public Desk.Clues GetClueID()
    {
        return m_clueID;
    }
}
