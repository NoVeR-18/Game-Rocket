using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SnipdropMenu : Tab
{
    [SerializeField]
    private Button _spinButton;
    [SerializeField]
    private TextMeshProUGUI _rewardText;

    private const string LastSpinDateKey = "LastSpinDate";
    private const string RewardGivenKey = "RewardGiven";

    private void Start()
    {
        _spinButton.onClick.AddListener(SpinWheel);
        _backButton?.onClick.AddListener(GameManager.Instance.CloseWindow);
        CheckSpinAvailability();
    }

    private void CheckSpinAvailability()
    {
        string lastSpinDateString = PlayerPrefs.GetString(LastSpinDateKey, "");
        DateTime lastSpinDate;

        if (!DateTime.TryParse(lastSpinDateString, out lastSpinDate))
        {
            lastSpinDate = DateTime.MinValue;
        }

        DateTime currentDate = DateTime.UtcNow.Date;

        if (lastSpinDate == currentDate)
        {
            _spinButton.interactable = false;
        }
    }

    private void SpinWheel()
    {
        int randomReward = UnityEngine.Random.Range(0, 3);

        switch (randomReward)
        {
            case 0:
                GiveFreeBoost();
                StartCoroutine(Close());
                break;
            case 1:
                GiveExtraCoins();
                break;
            case 2:
                GiveRandomUpgrade();
                break;
        }
        DateTime currentDate = DateTime.UtcNow.Date;
        PlayerPrefs.SetString(LastSpinDateKey, currentDate.ToString("yyyy-MM-dd"));
        PlayerPrefs.SetInt(RewardGivenKey, 1);
        PlayerPrefs.Save();

        // Отключаем кнопку после вращения
        _spinButton.interactable = false;
    }
    IEnumerator Close()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.CloseWindow();
        GameManager.Instance._spawner.TurboClick();
    }
    private void GiveFreeBoost()
    {
        // Здесь реализуем логику выдачи бесплатного буста
        _rewardText.text = "Great! You get free boost!";
    }

    private void GiveExtraCoins()
    {
        PlayerBalance.Instance.AddMoney(1500);
        _rewardText.text = "Great! You get Extra crystals!";
    }

    private void GiveRandomUpgrade()
    {
        GameManager.Instance._boostMenu.FreeUpdate();
        _rewardText.text = "Great! You get upgrades for spaceship!";
    }
}
