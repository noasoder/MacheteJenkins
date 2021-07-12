using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalManager : MonoBehaviour
{
    public UnityEvent AddedClueEvent = new UnityEvent();
    public UnityEvent RemovedClueEvent = new UnityEvent();

    public static GlobalManager Instance { get; private set; }

    private List<Desk.Clues> m_foundClues;

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
}
