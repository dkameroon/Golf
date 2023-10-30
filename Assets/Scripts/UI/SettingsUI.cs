using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }
    
    [SerializeField] private Button closeButton;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private TextMeshProUGUI musicProcents;
    [SerializeField] private Slider sliderSounds;
    [SerializeField] private TextMeshProUGUI soundsProcents;
    [SerializeField] private AudioSource audio;
    [SerializeField] private SoundManager soundManager;
    private float volumeMusic = 1f;
    private float volumeSounds = 1f;

    private void Awake()
    {
        Instance = this;
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
        sliderSounds.value = PlayerPrefs.GetFloat("SoundsVolume");
        closeButton.onClick.AddListener(() =>
        {
            PlayerPrefs.Save();
            Hide();
            PauseUI.Instance.Show();
        });
        
    }

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        ChangeVolumeMusic();
        ChangeVolumeSounds();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ChangeVolumeMusic()
    {
        volumeMusic = sliderMusic.value;
        audio.volume = volumeMusic;
        PlayerPrefs.SetFloat("MusicVolume", volumeMusic);
        PlayerPrefs.Save();
        musicProcents.text = (volumeMusic * 100).ToString("0") + "%";
    }
    public void ChangeVolumeSounds()
    {
        volumeSounds = sliderSounds.value;
        soundManager.volumeSounds = volumeSounds;
        PlayerPrefs.SetFloat("SoundsVolume", volumeSounds);
        PlayerPrefs.Save();
        soundsProcents.text = (volumeSounds * 100).ToString("0") + "%";
    }
    
    private void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0f);
        PlayerPrefs.Save();
    }
}
