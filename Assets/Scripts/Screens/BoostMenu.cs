using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostMenu : Tab
{
    [SerializeField]
    private Button _fullEnergyButton;
    [SerializeField]
    private Button _turboEnergyButton;
    [SerializeField]
    private TextMeshProUGUI _fullEnergyText;
    [SerializeField]
    private TextMeshProUGUI _turboEnergyText;
    [SerializeField] private BoostElement _wings;
    [SerializeField] private BoostElement _fuselage;
    [SerializeField] private BoostElement _reactor;
    [SerializeField] private BoostElement _autopilot;

    private const string turboClicksKey = "TurboClicks";
    private const string fullEnergyClicksKey = "FullEnergyClicks";
    private const string LastClickDateKey = "LastClickDate";
    private const int MaxClicksPerDay = 3;

    private void Start()
    {
        UpdateBalance();
        PlayerBalance.UpdateBalanse += UpdateBalance;
        _backButton?.onClick.AddListener(GameManager.Instance.CloseWindow);
        _turboEnergyButton.onClick.AddListener(() => OnButtonClick(turboClicksKey, _turboEnergyText));
        _fullEnergyButton.onClick.AddListener(() => OnButtonClick(fullEnergyClicksKey, _fullEnergyText));

        CheckResetDailyClicks();
        UpdateButtonTexts();
    }

    private void CheckResetDailyClicks()
    {
        string lastClickDateString = PlayerPrefs.GetString(LastClickDateKey, "");
        DateTime lastClickDate;

        if (!DateTime.TryParse(lastClickDateString, out lastClickDate))
        {
            lastClickDate = DateTime.MinValue;
        }

        DateTime currentDate = DateTime.UtcNow.Date;

        if (lastClickDate != currentDate)
        {
            PlayerPrefs.SetInt(turboClicksKey, 0);
            PlayerPrefs.SetInt(fullEnergyClicksKey, 0);
            PlayerPrefs.SetString(LastClickDateKey, currentDate.ToString("yyyy-MM-dd"));
            PlayerPrefs.Save();
        }
    }

    private void OnButtonClick(string buttonKey, TextMeshProUGUI buttonText)
    {
        int currentClicks = PlayerPrefs.GetInt(buttonKey, 0);

        if (currentClicks < MaxClicksPerDay)
        {
            currentClicks++;
            switch (buttonKey)
            {
                case turboClicksKey:
                    TurboClick();
                    break;
                case fullEnergyClicksKey:
                    FullEnergyClick();
                    break;
            }
            PlayerPrefs.SetInt(buttonKey, currentClicks);
            PlayerPrefs.Save();

        }
    }

    private void UpdateButtonTexts()
    {
        UpdateTurboButton();
        UpdateFullEnergyButton();
    }
    private void UpdateTurboButton()
    {
        _turboEnergyText.text = $"Turbo\n{MaxClicksPerDay - PlayerPrefs.GetInt(turboClicksKey, 0)}/3";
    }
    private void UpdateFullEnergyButton()
    {
        _fullEnergyText.text = $"FullEnergy\n{MaxClicksPerDay - PlayerPrefs.GetInt(fullEnergyClicksKey, 0)}/3";
    }

    private void TurboClick()
    {
        UpdateTurboButton();
        GameManager.Instance.CloseWindow();
        GameManager.Instance._spawner.TurboClick();
    }
    private void FullEnergyClick()
    {
        UpdateFullEnergyButton();
        GameManager.Instance._gameUi.FullEnergy();
    }
    public void FreeUpdate()
    {
        // Случайно выбираем один из элементов
        int randomElementIndex = UnityEngine.Random.Range(0, 4);

        BoostElement selectedElement = null;

        switch (randomElementIndex)
        {
            case 0:
                selectedElement = _wings;
                break;
            case 1:
                selectedElement = _fuselage;
                break;
            case 2:
                selectedElement = _reactor;
                break;
            case 3:
                selectedElement = _autopilot;
                break;
        }

        // Вызов метода повышения ранга у выбранного элемента
        if (selectedElement != null)
        {
            selectedElement.FreeUpgrade();
        }

        Debug.Log("Игрок получил бесплатное случайное улучшение.");
    }

}
