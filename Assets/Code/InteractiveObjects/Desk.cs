using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public static Desk Instance { get; private set; }

    public List<Clue> m_cluesToActivate;
    private List<Clues> m_addedClues;

    public enum Clues
    {
        FingerPrint = 0,
        Wallet, 
        Bullet, 
        Note,
        CigaretteButts,
        ChalkOutline,
        HotDog
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Clue[] clues = GetComponentsInChildren<Clue>(true);

        foreach (Clue item in clues)
        {
            m_cluesToActivate.Add(item);
        }
    }

    void Start()
    {
        m_addedClues = new List<Clues>();

        foreach (Clue clue in m_cluesToActivate)
        {
            if(m_addedClues.Contains(clue.GetClueID()))
            {
                clue.gameObject.SetActive(true);
            }
            else
            {
                clue.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        
    }

    public void AddClue(Clues clue)
    {
        if(!m_addedClues.Contains(clue))
            m_addedClues.Add(clue);

        //activate clue visual
        for (int i = 0; i < m_cluesToActivate.Count; i++)
        {
            if(m_cluesToActivate[i].GetClueID() == clue)
            {
                m_cluesToActivate[i].gameObject.SetActive(true);
            }
        }
    }

    public void RemoveClue(Clues clue)
    {
        if (m_addedClues.Contains(clue))
            m_addedClues.Remove(clue);


        //deactivate clue visual
        for (int i = 0; i < m_cluesToActivate.Count; i++)
        {
            if (m_cluesToActivate[i].GetClueID() == clue)
            {
                m_cluesToActivate[i].gameObject.SetActive(false);
            }
        }
    }
}
