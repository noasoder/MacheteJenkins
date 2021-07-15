using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle m_fullscreenToggle;
    [SerializeField] private Dropdown m_resolutionDropdown;
    [SerializeField] private Slider m_audioSlider;

    [SerializeField] private AudioMixer m_audioMixer;

    private Resolution[] m_resolutions;
    [SerializeField] private Dropdown m_resDropdown;

    void Start()
    {
        m_fullscreenToggle.isOn = Screen.fullScreen;

        m_audioMixer.GetFloat("Volume", out float volume);
        m_audioSlider.value = volume;

        m_resolutions = Screen.resolutions;
        m_resDropdown.ClearOptions();

        int currentResolution = 0;

        List<string> strings = new List<string>();
        for (int i = 0; i < m_resolutions.Length; i++)
        {
            string option = m_resolutions[i].width + " X " + m_resolutions[i].height + " : " + m_resolutions[i].refreshRate + "Hz";
            strings.Add(option);

            if(m_resolutions[i].width == Screen.width && m_resolutions[i].height == Screen.height)
            {
                currentResolution = i;
            }
        }

        m_resDropdown.AddOptions(strings);

        //Debug.Log(currentResolution);
        m_resolutionDropdown.value = currentResolution;
        m_resolutionDropdown.RefreshShownValue();
    }


    void Update()
    {
        
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    public void SetVolume(float volume)
    {
        m_audioMixer.SetFloat("Volume", volume);
    }
    public void SetResolution(int index)
    {
        Screen.SetResolution(m_resolutions[index].width, m_resolutions[index].height, Screen.fullScreen);
    }
}
