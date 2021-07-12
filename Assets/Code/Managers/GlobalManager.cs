using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HelperFunctions))]
public class GlobalManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent AddedClueEvent = new UnityEvent();
    [HideInInspector] public UnityEvent RemovedClueEvent = new UnityEvent();
    [HideInInspector] public UnityEvent IsPausedEvent = new UnityEvent();

    public static GlobalManager Instance { get; private set; }

    [HideInInspector] public HelperFunctions m_helperFunctions;

    private List<Desk.Clues> m_foundClues;
    private bool m_isPaused = false;
    private bool m_canMove = true;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        m_foundClues = new List<Desk.Clues>();
        m_helperFunctions = GetComponent<HelperFunctions>();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetPaused(false);
        SetCanMove(true);
    }
    public void AddFoundClue(Desk.Clues clue)
    {
        if (!m_foundClues.Contains(clue))
        {
            m_foundClues.Add(clue);
            AddedClueEvent.Invoke();
        }
    }
    public void RemoveFoundClue(Desk.Clues clue)
    {
        if(m_foundClues.Contains(clue))
        {
            m_foundClues.Remove(clue);
            RemovedClueEvent.Invoke();
        }
    }
    public bool HasFoundClue(Desk.Clues clue)
    {
        return m_foundClues.Contains(clue) ? true : false;
    }
    public List<Desk.Clues> GetFoundClues()
    {
        return m_foundClues;
    }
    public void SetPaused(bool isPaused)
    {
        m_isPaused = isPaused;
        IsPausedEvent.Invoke();
    }
    public bool IsPaused()
    {
        return m_isPaused;
    }
    public bool CanMove()
    {
        return m_canMove;
    }
    public void SetCanMove(bool canMove)
    {
        m_canMove = canMove;
    }

}
