using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private GameObject m_player;
    private SpriteRenderer m_spriteRenderer;

    [SerializeField] private Sprite m_defaultSprite;
    [SerializeField] private Sprite m_highlightedSprite;

    private bool m_highlighted = false;
    [SerializeField] private float m_highlightDistance = 2f;

    [SerializeField] private Mode m_mode = Mode.Highlight;



    public enum Mode
    {
        Highlight = 0,
        MouseHover
    }

    public void DefaultStart()
    {
        m_player = GameObject.FindWithTag("Player");
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DefaultUpdate()
    {
        switch (m_mode)
        {
            case Mode.Highlight:
                OnModeHighlight();
                break;
            case Mode.MouseHover:
                OnModeMouseHover();
                break;
            default:
                break;
        }
    }

    void OnModeHighlight()
    {
        Vector3 vecToPlayer = m_player.transform.position - transform.position;

        if (vecToPlayer.sqrMagnitude <= m_highlightDistance && !GetIsHighlighted())
        {
            HighlightObject(true);
        }
        else if (vecToPlayer.sqrMagnitude > m_highlightDistance && GetIsHighlighted())
        {
            HighlightObject(false);
        }
    }
    void OnModeMouseHover()
    {

    }
    private void OnMouseEnter()
    {
        if(m_mode == Mode.MouseHover)
        {
            HighlightObject(true);
            transform.localScale += Vector3.one;
        }
    }
    private void OnMouseExit()
    {
        if (m_mode == Mode.MouseHover)
        {
            HighlightObject(false);
            transform.localScale -= Vector3.one;
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
    public Mode GetMode()
    {
        return m_mode;
    }
}
