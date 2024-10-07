using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public Resolution resolution;
    public AudioMixer AudioMixer;
    Resolution[] Resolutions;
    public TMP_Dropdown ResolutionDropdown;
    public Slider VolumeSlider;
    public TMP_Dropdown GraphicsDropdown;  
    private void Start()
    {
        Resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();
        
        List<string> Options = new List<string>();

        int CurrentResolutionIndex = 0; 

        for(int i = 0; i < Resolutions.Length; i++)
        {
            string Option = Resolutions[i].width + " x " + Resolutions[i].height;
            Options.Add(Option);

            if (Resolutions[i].width == Screen.currentResolution.width &&
                Resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = i;
            }
        }
        ResolutionDropdown.AddOptions(Options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue(); 
    }

    public void SetVolume(float VolumeChange)
    {
        AudioMixer.SetFloat("VolumeChange", VolumeChange);
        VolumeSlider.value = VolumeChange; 
    }
    public void SetQuality(int QualityLevel)
    {
        QualitySettings.SetQualityLevel(QualityLevel);
        GraphicsDropdown.value = QualityLevel; 
    }
    public void SetFullScreen(bool FullscreenTog)
    {
        Screen.fullScreen = FullscreenTog;
    }
    public void SetResolution(int ResolutionIndex)
    {
        resolution = Resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
    }
    public void SetShadows(bool ShadowsTog)
    {
        if (!ShadowsTog)
        {
            QualitySettings.shadows = ShadowQuality.Disable;
        }
    }
}
