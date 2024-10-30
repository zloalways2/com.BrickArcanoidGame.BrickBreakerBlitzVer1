using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundscapeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _masterAudioMixer;
    [SerializeField] private Button _effectsToggleButton;
    [SerializeField] private Button _musicToggleButton;
    [SerializeField] private Sprite _activeAudioIcon;
    [SerializeField] private Sprite _inactiveAudioIcon;

    private bool _isEffectsEnabled;
    private bool _isMusicEnabled;

    private const string EffectsPrefKey = "EffectsEnabled";
    private const string MusicPrefKey = "MusicEnabled";

    private void Start()
    {
        _isEffectsEnabled = PlayerPrefs.GetInt(EffectsPrefKey, 1) == 1;
        _isMusicEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1;

        ApplyAudioSettings();
        UpdateToggleButtonVisuals();
        
        _effectsToggleButton.onClick.AddListener(ToggleEffects);
        _musicToggleButton.onClick.AddListener(ToggleMusic);
    }

    private void ToggleEffects()
    {
        _isEffectsEnabled = !_isEffectsEnabled;
        ApplyAudioSettings();
        UpdateToggleButtonVisuals();
        SaveAudioPreferences();
    }

    private void ToggleMusic()
    {
        _isMusicEnabled = !_isMusicEnabled;
        ApplyAudioSettings();
        UpdateToggleButtonVisuals();
        SaveAudioPreferences();
    }

    private void ApplyAudioSettings()
    {
        _masterAudioMixer.SetFloat("EffectsVolume", _isEffectsEnabled ? 0f : -80f);
        _masterAudioMixer.SetFloat("MusicVolume", _isMusicEnabled ? 0f : -80f);
    }

    private void UpdateToggleButtonVisuals()
    {
        _effectsToggleButton.image.sprite = _isEffectsEnabled ? _activeAudioIcon : _inactiveAudioIcon;
        _musicToggleButton.image.sprite = _isMusicEnabled ? _activeAudioIcon : _inactiveAudioIcon;
    }

    private void SaveAudioPreferences()
    {
        PlayerPrefs.SetInt(EffectsPrefKey, _isEffectsEnabled ? 1 : 0);
        PlayerPrefs.SetInt(MusicPrefKey, _isMusicEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }
}
