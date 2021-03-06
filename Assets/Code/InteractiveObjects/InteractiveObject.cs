using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    [Tooltip("When opened these objects activate to unlock more of the story. Use this for next clue etc.")]
    [SerializeField] private List<GameObject> m_nextInStory;

    private GameObject m_player;
    private SpriteRenderer m_spriteRenderer;

    [SerializeField] private Sprite m_defaultSprite;
    [SerializeField] private Sprite m_highlightedSprite;

    private bool m_highlighted = false;
    [SerializeField] private float m_highlightDistance = 2f;
    [SerializeField] private bool m_zoomInOnHighlighted = true;

    [SerializeField] private Mode m_mode = Mode.Highlight;

    public enum Mode
    {
        Highlight = 0,
        MouseHover,
        NotInteractive
    }

    public void DefaultStart()
    {
        m_player = GameObject.FindWithTag("Player");
        if(m_mode != Mode.NotInteractive)
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
            HighlightObject(true, m_zoomInOnHighlighted);
        }
        else if (vecToPlayer.sqrMagnitude > m_highlightDistance && GetIsHighlighted())
        {
            HighlightObject(false, m_zoomInOnHighlighted);
        }
    }
    void OnModeMouseHover()
    {

    }
    private void OnMouseEnter()
    {
        if(m_mode == Mode.MouseHover && !GlobalManager.Instance.IsPaused())
        {
            HighlightObject(true, m_zoomInOnHighlighted);
            
        }
    }
    private void OnMouseExit()
    {
        if (m_mode == Mode.MouseHover)
        {
            HighlightObject(false, m_zoomInOnHighlighted);
        }
    }

    private void HighlightObject(bool highlight, bool zoomIn)
    {
        m_highlighted = highlight;

        //highlight stuff
        if(highlight)
        {
            if(zoomIn)
                transform.localScale += Vector3.one * 0.5f;
            m_spriteRenderer.sprite = m_highlightedSprite;
        }
        else
        {
            if (zoomIn)
                transform.localScale -= Vector3.one * 0.5f;
            m_spriteRenderer.sprite = m_defaultSprite;
        }
    }

    public bool GetIsHighlighted()
    {
        return m_highlighted;
    }
    public Mode GetMode()
    {
        return m_mode;
    }
    public void EnableNextInStory()
    {
        foreach (GameObject item in m_nextInStory)
        {
            item.SetActive(true);
        }
    }
}
