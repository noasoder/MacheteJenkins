using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent AddedClueEvent = new UnityEvent();
    [HideInInspector] public UnityEvent RemovedClueEvent = new UnityEvent();
    [HideInInspector] public UnityEvent IsPausedEvent = new UnityEvent();

    public static GlobalManager Instance { get; private set; }

    private List<Desk.Clues> m_foundClues;
    private bool m_isPaused = false;

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
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetPaused(false);
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

}
