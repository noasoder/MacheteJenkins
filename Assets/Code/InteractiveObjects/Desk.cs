using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public static Desk Instance { get; private set; }

    private List<Clue> m_cluesToActivate;

    [SerializeField] GameObject m_playerSpawn;

    public enum Clues
    {
        FingerPrint = 0,
        Wallet, 
        Bullet, 
        Note,
        CigaretteButts,
        ChalkOutline,
        HotDog,
        Fight1Clue,
        Fight2Clue
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        m_cluesToActivate = new List<Clue>();

        Clue[] clues = GetComponentsInChildren<Clue>(true);
        foreach (Clue item in clues)
        {
            m_cluesToActivate.Add(item);
        }

        GlobalManager.Instance.AddedClueEvent.AddListener(UpdateClues);
        GlobalManager.Instance.RemovedClueEvent.AddListener(UpdateClues);
    }

    void Start()
    {
        UpdateClues();
    }

    void Update()
    {
        
    }

    public void UpdateClues()
    {
        if(GlobalManager.Instance)
        {
            List<Clues> foundClues = GlobalManager.Instance.GetFoundClues();

            //activate clue visual
            foreach (Clue clue in m_cluesToActivate)
            {
                if (foundClues.Contains(clue.GetClueID()))
                {
                    clue.gameObject.SetActive(true);
                }
                else
                {
                    clue.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("No GlobalManager in the scene!!! Add one from prefabs");
        }
    }

    public Vector3 GetDeskSpawnPoint()
    {
        return m_playerSpawn.transform.position;
    }

    //public void RemoveClue(Clues clue)
    //{
    //    if (m_addedClues.Contains(clue))
    //        m_addedClues.Remove(clue);


    //    //deactivate clue visual
    //    for (int i = 0; i < m_cluesToActivate.Count; i++)
    //    {
    //        if (m_cluesToActivate[i].GetClueID() == clue)
    //        {
    //            m_cluesToActivate[i].gameObject.SetActive(false);
    //        }
    //    }
    //}
}
