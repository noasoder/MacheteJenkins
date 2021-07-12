using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InteractiveObject
{
    [SerializeField] private GameObject m_startFightPrompt;
    [SerializeField] private GameObject m_defeatedPrompt;

    private State m_state = State.Waiting;
    [SerializeField] private Desk.Clues m_clueID = Desk.Clues.Fight1Clue;

    enum State
    {
        Waiting = 0,
        Talking,
        Battle,
        Defeated
    }

    void Start()
    {
        DefaultStart();

        if(GlobalManager.Instance.HasFoundClue(m_clueID))
        {
            m_state = State.Defeated;
        }
    }

    void Update()
    {
        if(!GlobalManager.Instance.IsPaused())
        {
            DefaultUpdate();

            switch (m_state)
            {
                case State.Waiting:
                    OnStateWaiting();
                    break;
                case State.Talking:
                    OnStateTalking();
                    break;
                case State.Battle:
                    OnStateBattle();
                    break;
                case State.Defeated:
                    OnStateDefeated();
                    break;
                default:
                    break;
            }
        }
    }
    void OnStateWaiting()
    {
        if(GetIsHighlighted() && Input.GetKeyDown(KeyCode.E))
        {
            m_startFightPrompt.SetActive(true);
            GlobalManager.Instance.SetCanMove(false);
            m_state = State.Talking;
        }
    }
    void OnStateTalking()
    {

    }
    void OnStateBattle()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            EndFight();
        }
    }
    void OnStateDefeated()
    {
        if(GetIsHighlighted() && Input.GetKeyDown(KeyCode.E))
        {
            GlobalManager.Instance.SetCanMove(false);
            m_defeatedPrompt.SetActive(true);
        }
    }

    public void CloseBattlePrompt()
    {
        if(m_state == State.Talking)
        {
            m_startFightPrompt.SetActive(false);
            GlobalManager.Instance.SetCanMove(true);
            m_state = State.Waiting;
        }
    }
    public void CloseDefeatedPrompt()
    {
        if (m_state == State.Defeated)
        {
            m_defeatedPrompt.SetActive(false);
            GlobalManager.Instance.SetCanMove(true);
        }
    }
    public void StartFight()
    {
        m_startFightPrompt.SetActive(false);
        m_state = State.Battle;
    }
    public void EndFight()
    {
        m_defeatedPrompt.SetActive(true);
        m_state = State.Defeated;
        GlobalManager.Instance.AddFoundClue(m_clueID);
    }
}