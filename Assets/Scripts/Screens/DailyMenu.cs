
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyMenu : Tab
{
    [SerializeField]
    private Button _claimRewardButton;
    [SerializeField]
    private List<DayConteiner> _claimRewardConteiners = new List<DayConteiner>();
    private const string LastLoginKey = "LastLoginDate";
    private const string LoginStreakKey = "LoginStreak";
    private const int MaxStreakDays = 15;

    private void Start()
    {
        _backButton?.onClick.AddListener(GameManager.Instance.CloseWindow);
        _claimRewardButton.onClick.AddListener(ClaimReward);
        CheckLoginStreak();
        UpdateBalance();
        PlayerBalance.UpdateBalanse += UpdateBalance;
        int loginStreak = PlayerPrefs.GetInt(LoginStreakKey, 0);
        for (int i = 0; i <= loginStreak; i++)
        {
            _claimRewardConteiners[i].CollectConteiner();
        }
    }

    private void CheckLoginStreak()
    {
        // Получаем дату последнего входа из PlayerPrefs
        string lastLoginString = PlayerPrefs.GetString(LastLoginKey, "");
        DateTime lastLoginDate;

        // Если данных о последнем входе нет, установить его как минимальную дату
        if (!DateTime.TryParse(lastLoginString, out lastLoginDate))
        {
            lastLoginDate = DateTime.MinValue;
        }

        // Получаем текущую дату
        DateTime currentDate = DateTime.UtcNow.Date;

        // Проверяем, был ли вход сегодня
        if (lastLoginDate == currentDate)
        {
            Debug.Log("Вы уже заходили сегодня.");
            _claimRewardButton.onClick.RemoveListener(ClaimReward);
            return;
        }
        _claimRewardButton.onClick.AddListener(ClaimReward);

        // Проверяем разницу в днях между последним входом и текущей датой
        int daysDifference = (currentDate - lastLoginDate).Days;

        // Если прошло больше одного дня, сбрасываем счетчик
        if (daysDifference > 1)
        {
            PlayerPrefs.SetInt(LoginStreakKey, 1); // Начинаем заново
        }
        else
        {
            // Увеличиваем счетчик на 1, но не больше максимального значения
            int currentStreak = PlayerPrefs.GetInt(LoginStreakKey, 1);
            PlayerPrefs.SetInt(LoginStreakKey, Mathf.Min(currentStreak + 1, MaxStreakDays));
        }

        // Сохраняем текущую дату как последнюю дату входа
        PlayerPrefs.SetString(LastLoginKey, currentDate.ToString("yyyy-MM-dd"));
        PlayerPrefs.Save();
    }

    private void ClaimReward()
    {
        int loginStreak = PlayerPrefs.GetInt(LoginStreakKey, 0);

        if (loginStreak > 0)
        {
            Debug.Log($"Ваша награда за {loginStreak} дней входа выдана!");

            PlayerBalance.Instance.AddMoney(_claimRewardConteiners[loginStreak].AwardAmount);
            _claimRewardButton.onClick.RemoveListener(ClaimReward);
            // Если достигнут максимум, обнуляем счетчик
            if (loginStreak >= MaxStreakDays)
            {
                PlayerPrefs.SetInt(LoginStreakKey, 0);
            }

            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Нет доступных наград.");
        }
    }
    private void OnEnable()
    {
        //CheckLoginStreak();
    }
}
