using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    [SerializeField] private float m_moveSpeed = 4f;

    private Vector2 m_movement;

    [SerializeField] private bool m_alternativeMovement = false;
    [SerializeField] private bool m_reverseFlip = false;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector2 speed = Vector2.zero;
        if(!GlobalManager.Instance.IsPaused() && GlobalManager.Instance.CanMove())
        {
            if (m_alternativeMovement)
            {

            }
            else
            {
                m_movement.x = Input.GetAxisRaw("Horizontal");
                m_movement.y = Input.GetAxisRaw("Vertical");

                if(m_movement.x < 0)
                {
                    if(m_spriteRenderer.flipX == m_reverseFlip)
                        m_spriteRenderer.flipX = !m_reverseFlip;
                }
                else if(m_movement.x > 0)
                {
                    if (m_spriteRenderer.flipX == !m_reverseFlip)
                        m_spriteRenderer.flipX = m_reverseFlip;
                }
                speed = m_movement.normalized * m_moveSpeed;
                m_rb.MovePosition(m_rb.position + speed);
            }
        }
        Player.Instance.GetAnimator().SetFloat("Speed", speed.sqrMagnitude);
    }
}
