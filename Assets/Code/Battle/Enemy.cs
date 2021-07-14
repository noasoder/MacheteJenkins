using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : InteractiveObject
{
    //Wow this is a mess Lol, might clean it up some day :P

    [System.Serializable]
    struct KeyCombination
    {
        public float m_timeToComplete;
        public List<Keys> m_keysToPress;
    }

    public enum Keys
    {
        W = 0, A, S, D
    }

    enum State
    {
        Waiting = 0,
        Talking,
        Battle,
        Defeated
    }


    [SerializeField] private GameObject m_startFightPrompt;
    [SerializeField] private GameObject m_winPrompt;
    [SerializeField] private GameObject m_playerDefeatedPrompt;
    [SerializeField] private GameObject m_battlePrompt;


    private State m_state = State.Waiting;
    [SerializeField] private Desk.Clues m_clueID = Desk.Clues.Fight1Clue;


    [Header("Fighting")]

    [SerializeField] private Text m_keysText;
    [SerializeField] private Text m_keysPressedText;
    [SerializeField] private Image m_timeLeftCircle;
    [SerializeField] private List<KeyCombination> m_keyCombinations;

    private int m_currentkeyCombination = 0;

    private KeyCode m_nextKey; //The next key in current combination
    private int m_nextKeyIndex; //The current key to press
    private float m_currentTimeLeft;


    void Start()
    {
        DefaultStart();

        if(GlobalManager.Instance.HasFoundClue(m_clueID))
        {
            m_state = State.Defeated;
        }

        m_winPrompt.SetActive(false);
        m_startFightPrompt.SetActive(false);
        m_playerDefeatedPrompt.SetActive(false);
        m_battlePrompt.SetActive(false);

        //m_timeLeftCircle.fillClockwise = true;
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
    void OnStateDefeated()
    {
        if(GetIsHighlighted() && Input.GetKeyDown(KeyCode.E))
        {
            GlobalManager.Instance.SetCanMove(false);
            m_winPrompt.SetActive(true);
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
            m_winPrompt.SetActive(false);
            GlobalManager.Instance.SetCanMove(true);
        }
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

        if (Input.GetKeyDown(m_nextKey))
        {
            //if keys left in current combination
            if(m_keyCombinations[m_currentkeyCombination].m_keysToPress.Count > m_nextKeyIndex + 1)
            {
                m_nextKeyIndex++;
                m_nextKey = GetKeyCode();

                UpdateKeysPressedText();
            }
            else if(m_keyCombinations.Count > m_currentkeyCombination + 1) //go to next combination
            {
                m_currentkeyCombination++;
                m_currentTimeLeft = m_keyCombinations[m_currentkeyCombination].m_timeToComplete;

                m_nextKeyIndex = 0;
                m_nextKey = GetKeyCode();

                UpdateKeysText();
                UpdateKeysPressedText();
            }
            else
            {
                WinFight();
            }
        }

        if(m_currentTimeLeft <= 0.0f)
        {
            LooseFight();
        }
        else
        {
            m_currentTimeLeft -= Time.deltaTime;
            m_timeLeftCircle.fillAmount = GetKeyCombinationTimeProgress();
        }

    }
    public void StartFight()
    {
        m_startFightPrompt.SetActive(false);
        m_battlePrompt.SetActive(true);

        m_currentTimeLeft = m_keyCombinations[m_currentkeyCombination].m_timeToComplete;
        
        m_nextKeyIndex = 0;
        m_nextKey = GetKeyCode();

        //Debug.Log(keyString);
        UpdateKeysText();
        UpdateKeysPressedText();

        m_state = State.Battle;
    }
    private void WinFight()
    {
        m_battlePrompt.SetActive(false);
        m_winPrompt.SetActive(true);
        m_state = State.Defeated;
        GlobalManager.Instance.AddFoundClue(m_clueID);
        EnableNextInStory();
    }
    private void LooseFight()
    {
        m_battlePrompt.SetActive(false);
        m_playerDefeatedPrompt.SetActive(true);
    }
    private KeyCode GetKeyCode()
    {
        string keyString = m_keyCombinations[m_currentkeyCombination].m_keysToPress[m_nextKeyIndex].ToString();
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), keyString);
    }
    //Returns the time left to input the combination in a 1 to 0 format
    public float GetKeyCombinationTimeProgress()
    {
        //Debug.Log(m_currentTimeLeft / m_keyCombinations[m_currentkeyCombination].m_timeToComplete);
        return m_currentTimeLeft / m_keyCombinations[m_currentkeyCombination].m_timeToComplete;
    }
    public void UpdateKeysText()
    {
        string keyCombination = "";
        foreach (Keys key in m_keyCombinations[m_currentkeyCombination].m_keysToPress)
        {
            keyCombination += key.ToString();
        }

        m_keysText.text = keyCombination;
    }
    private void UpdateKeysPressedText()
    {
        string keyCombination = "";

        for (int i = 0; i < m_keyCombinations[m_currentkeyCombination].m_keysToPress.Count; i++)
        {
            if(i < m_nextKeyIndex)
            {
                keyCombination += m_keyCombinations[m_currentkeyCombination].m_keysToPress[i].ToString();
            }
        }

        m_keysPressedText.text = keyCombination;
    }




    public void RespawnPlayer()
    {
        m_state = State.Waiting;

        m_winPrompt.SetActive(false);
        m_startFightPrompt.SetActive(false);
        m_playerDefeatedPrompt.SetActive(false);
        m_battlePrompt.SetActive(false);

        GlobalManager.Instance.SetCanMove(true);
        Player.Instance.RespawnPlayer(Player.SpawnPositions.AtDesk);
    }
    public void GoToMainMenu()
    {
        //Currently unnecessary state change and disables
        m_state = State.Waiting;

        m_winPrompt.SetActive(false);
        m_startFightPrompt.SetActive(false);
        m_playerDefeatedPrompt.SetActive(false);
        m_battlePrompt.SetActive(false);

        GlobalManager.Instance.m_helperFunctions.LoadSceneWithLoadingScreen("MainMenu");
    }
}