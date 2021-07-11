using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private GameObject m_player;
    private SpriteRenderer m_spriteRenderer;

    [SerializeField] private Sprite m_defaultSprite;
    [SerializeField] private Sprite m_highlightedSprite;

    [SerializeField] private float m_highlightDistance = 2f;
    private bool m_highlighted = false;

    public void DefaultStart()
    {
        m_player = GameObject.FindWithTag("Player");
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DefaultUpdate()
    {
        Vector3 vecToPlayer = m_player.transform.position - transform.position;

        if(vecToPlayer.sqrMagnitude <= m_highlightDistance && !GetIsHighlighted())
        {
            HighlightObject(true);
        }
        else if(vecToPlayer.sqrMagnitude > m_highlightDistance && GetIsHighlighted())
        {
            HighlightObject(false);
        }
    }

    private void HighlightObject(bool highlight)
    {
        m_highlighted = highlight;


        //highlight stuff
        if(highlight)
            m_spriteRenderer.sprite = m_highlightedSprite;
        else
            m_spriteRenderer.sprite = m_defaultSprite;
    }

    public bool GetIsHighlighted()
    {
        return m_highlighted;
    }
}
