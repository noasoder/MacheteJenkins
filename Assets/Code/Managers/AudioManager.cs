using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


    [SerializeField] private List<AudioSource> m_music;

    [SerializeField] private Music m_currentMusic = Music.NoMusic;
    [SerializeField] private bool m_loopCurrent = true;

    public enum Music
    {
        NoMusic = -1,
        Music1 = 0,
        Music2,
        Music3
    };

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if(!m_music[(int)m_currentMusic].isPlaying && m_loopCurrent)
        {
            m_music[(int)m_currentMusic].Play();
        }
    }

    public void StartMusic(Music music)
    {
        if (m_currentMusic == music)
            return;

        if(m_currentMusic != Music.NoMusic)
        {
            m_music[(int)m_currentMusic].Stop();
        }
        m_currentMusic = music;
        m_music[(int)m_currentMusic].Play();
    }
    public void SetLoopMusic(bool loopMusic)
    {
        m_loopCurrent = loopMusic;
    }
}
