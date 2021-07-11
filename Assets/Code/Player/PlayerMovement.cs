using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;

    [SerializeField] private float m_moveSpeed = 4f;

    private Vector2 m_movement;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    void Update()
    {
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        m_rb.MovePosition(m_rb.position + m_movement.normalized * m_moveSpeed * Time.deltaTime);
    }
}
