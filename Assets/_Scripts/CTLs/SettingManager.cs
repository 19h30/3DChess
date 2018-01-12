using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour {
    public Toggle fullScreenToggle;
    public Dropdown resDropdown;
    public Slider volumeSlider;

    public Button applyButton;

    public AudioSource audioSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;

    void OnEnable()
    {
        gameSettings = new GameSettings();

        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApply(); });

        resolutions = Screen.resolutions;
        foreach(Resolution res in resolutions)
        {
            resDropdown.options.Add(new Dropdown.OptionData(res.ToString()));
        }
        LoadSettings();
    }

    public void OnFullScreenToggle()
    {
        gameSettings.fullScreen = Screen.fullScreen = fullScreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resDropdown.value].width, resolutions[resDropdown.value].height, Screen.fullScreen);
        gameSettings.resIndex = resDropdown.value;
    }

    public void OnVolumeChange()
    {
        audioSource.volume = gameSettings.volume = volumeSlider.value;
    }

    public void OnApply()
    {
        PlayerPrefs.SetFloat("volume", gameSettings.volume);
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        volumeSlider.value = gameSettings.volume;
        resDropdown.value = gameSettings.resIndex;
        fullScreenToggle.isOn = gameSettings.fullScreen;
        Screen.fullScreen = gameSettings.fullScreen;

        resDropdown.RefreshShownValue();
    }
}
