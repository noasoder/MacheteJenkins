using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public enum SpawnPositions
    {
        AtDesk = 0,
        AtCustomPosition
    }

    private void Awake()
    {
    }
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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
    }
}
