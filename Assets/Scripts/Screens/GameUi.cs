using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : Tab
{
    private int _maxEnergy = 6000;
    public int _curEnergy = 6000;
    private float energyInterval = 2f;
    private int _increaseProfit = 1;
    [SerializeField]
    private TextMeshProUGUI _currentEnergyText;
    [SerializeField]
    private TextMeshProUGUI _maxEnergyText;
    [SerializeField]
    private Image _energyBar;
    [SerializeField]
    private ProgressBar _progressBar;
    [SerializeField]
    private Image _playerImage;

    private void UpdateEnergyUI()
    {
        _currentEnergyText.text = string.Format("{0:N0}", _curEnergy);
        _maxEnergyText.text = string.Format("{0:N0}", _maxEnergy);
        _energyBar.fillAmount = Mathf.Clamp01((float)_curEnergy / _maxEnergy);
    }


    void Start()
    {
        UpdateBalance();
        PlayerBalance.UpdateBalanse += UpdateBalance;
        _maxEnergy = PlayerPrefs.GetInt("maxEnergy", 6000);
        _increaseProfit = PlayerPrefs.GetInt("IncreaseProfit", 1);
        _curEnergy = PlayerPrefs.GetInt("currentEnergy", _maxEnergy);
        string lastEnergyUpdateStr = PlayerPrefs.GetString("lastEnergyUpdate", DateTime.Now.ToString());
        DateTime lastEnergyUpdate = DateTime.Parse(lastEnergyUpdateStr);
        DateTime currentTime = DateTime.Now;

        TimeSpan timeElapsed = currentTime - lastEnergyUpdate;
        int secondsElapsed = (int)timeElapsed.TotalSeconds;

        int energyToAdd = secondsElapsed / (int)energyInterval;

        _curEnergy = Mathf.Min(Mathf.Abs(_curEnergy + energyToAdd), _maxEnergy);

        PlayerPrefs.SetInt("currentEnergy", _curEnergy);
        PlayerPrefs.SetString("lastEnergyUpdate", currentTime.ToString());
        PlayerPrefs.Save();
        UpdateEnergyUI();

        GameManager.Instance._shopMenu.TryUploadSkin();
    }

    void Update()
    {
        if (_curEnergy < _maxEnergy)
        {
            if (Time.time >= energyInterval)
            {
                _curEnergy++;
                PlayerPrefs.SetInt("currentEnergy", _curEnergy);
                PlayerPrefs.SetString("lastEnergyUpdate", DateTime.Now.ToString());
                PlayerPrefs.Save();
                UpdateEnergyUI();
                energyInterval = Time.time + 5f;
            }
        }
    }
    public void SpendEnergy(FallingObject falling)
    {
        if (falling != null)
        {
            if (_curEnergy >= falling.coast)
            {
                _curEnergy -= falling.coast;
                _progressBar.SetProgress(falling.coast);
                UpdateEnergyUI();
                PlayerBalance.Instance.AddMoney(falling.coast);
                Destroy(falling.gameObject);
            }
        }
        else
        {
            if (_curEnergy > 0)
            {
                _curEnergy--;
                PlayerBalance.Instance.AddMoney(_increaseProfit);
                _progressBar.SetProgress(1);
                UpdateEnergyUI();
            }
        }
    }

    private void OnDisable()
    {
        GameManager.Instance._spawner.SetSpawning(false);
    }
    private void OnEnable()
    {
        GameManager.Instance._spawner.SetSpawning(true);
    }
    public void FullEnergy()
    {
        _curEnergy = _maxEnergy;
        UpdateEnergyUI();
    }
    public void UpgradeMaxEnergy(int addedValue)
    {
        _maxEnergy += addedValue;
        PlayerPrefs.SetInt("maxEnergy", _maxEnergy);
        PlayerPrefs.Save();
    }
    public void UpgradeIncreaseProfit()
    {
        _increaseProfit++;
        PlayerPrefs.SetInt("IncreaseProfit", _increaseProfit);
        PlayerPrefs.Save();
    }
    public void SetSkin(Sprite skinSprite)
    {
        _playerImage.sprite = skinSprite;
        //GetComponent<SpriteRenderer>().sprite = skinSprite;
    }
}
