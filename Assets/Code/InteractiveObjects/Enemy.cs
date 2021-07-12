using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InteractiveObject
{
    [SerializeField] private GameObject m_startFightPrompt;
    [SerializeField] private GameObject m_defeatedPrompt;
    [SerializeField] private GameObject m_playerDefeatedPrompt;

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
            WinFight();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LooseFight();
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
    public void WinFight()
    {
        m_defeatedPrompt.SetActive(true);
        m_state = State.Defeated;
        GlobalManager.Instance.AddFoundClue(m_clueID);
    }
    public void RespawnPlayer()
    {
        m_state = State.Waiting;

        m_defeatedPrompt.SetActive(false);
        m_startFightPrompt.SetActive(false);
        m_playerDefeatedPrompt.SetActive(false);

        GlobalManager.Instance.SetCanMove(true);
        Player.Instance.RespawnPlayer(Player.SpawnPositions.AtDesk);
    }
    public void GoToMainMenu()
    {
        m_state = State.Waiting;

        m_defeatedPrompt.SetActive(false);
        m_startFightPrompt.SetActive(false);
        m_playerDefeatedPrompt.SetActive(false);

        GlobalManager.Instance.m_helperFunctions.LoadSceneWithLoadingScreen("MainMenu");
    }
    public void LooseFight()
    {
        m_playerDefeatedPrompt.SetActive(true);
    }
}