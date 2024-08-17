using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : Tab
{
    [SerializeField]
    private Image _toogleUnMuteIcon;
    [SerializeField]
    private Button _toogleMuteButton;
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField]
    private Button _gameRules;
    [SerializeField]
    private Button FAQ;


    void Start()
    {
        _backButton?.onClick.AddListener(GameManager.Instance.CloseWindow);
        UpdateBalance();
        _toogleMuteButton.onClick.AddListener(ToggleSound);
        PlayerBalance.UpdateBalanse += UpdateBalance;
    }
    private bool _isMuted = false;

    public void ToggleSound()
    {
        if (_isMuted)
        {
            UnmuteSound();
        }
        else
        {
            MuteSound();
        }
        _isMuted = !_isMuted;
    }

    public void MuteSound()
    {
        _audioMixer.SetFloat("Master ", -80f);
        _toogleUnMuteIcon.gameObject.SetActive(false);
    }

    public void UnmuteSound()
    {
        _audioMixer.SetFloat("Master ", 0f);
        _toogleUnMuteIcon.gameObject.SetActive(true);
    }
}
