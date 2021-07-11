using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private void Awake()
    {
    }
    void Start()
    {
        if (Instance)
        {
            Instance = this;
        }
    }

    void Update()
    {
        
    }
}
