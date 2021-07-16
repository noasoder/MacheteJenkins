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
        Fingerprint = 0,
        Wallet, 
        Note,
        CigaretteButts,
        ChalkOutline,
        Footprints,
        RippedClothing,
        HotDog,
        BrokenBottles,
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
    }

    void Start()
    {
        GlobalManager.Instance.AddedClueEvent.AddListener(UpdateClues);
        GlobalManager.Instance.RemovedClueEvent.AddListener(UpdateClues);
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
}
