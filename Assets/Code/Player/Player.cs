using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private Animator m_animator;
    [SerializeField] private int m_defaultHealth = 3;
    private int m_health;

    public enum SpawnPositions
    {
        AtDesk = 0,
        AtCustomPosition
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        m_health = m_defaultHealth;
    }

    void Update()
    {

    }

    public void RespawnPlayer(SpawnPositions spawnAt, Vector3 pos = default(Vector3))
    {
        if(spawnAt == SpawnPositions.AtDesk)
        {
            transform.position = Desk.Instance.GetDeskSpawnPoint();
        }
        m_health = m_defaultHealth;
    }
    public Animator GetAnimator()
    {
        return m_animator;
    }
    //Returns true if player survived
    public bool TakeDamage(int amount)
    {
        m_health -= amount;
        //Debug.Log("Health: " + m_health);
        return m_health > 0 ? true : false;
    }
    public int GetHealth()
    {
        return m_health;
    }
}
