
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
        // �������� ���� ���������� ����� �� PlayerPrefs
        string lastLoginString = PlayerPrefs.GetString(LastLoginKey, "");
        DateTime lastLoginDate;

        // ���� ������ � ��������� ����� ���, ���������� ��� ��� ����������� ����
        if (!DateTime.TryParse(lastLoginString, out lastLoginDate))
        {
            lastLoginDate = DateTime.MinValue;
        }

        // �������� ������� ����
        DateTime currentDate = DateTime.UtcNow.Date;

        // ���������, ��� �� ���� �������
        if (lastLoginDate == currentDate)
        {
            Debug.Log("�� ��� �������� �������.");
            _claimRewardButton.onClick.RemoveListener(ClaimReward);
            return;
        }
        _claimRewardButton.onClick.AddListener(ClaimReward);

        // ��������� ������� � ���� ����� ��������� ������ � ������� �����
        int daysDifference = (currentDate - lastLoginDate).Days;

        // ���� ������ ������ ������ ���, ���������� �������
        if (daysDifference > 1)
        {
            PlayerPrefs.SetInt(LoginStreakKey, 1); // �������� ������
        }
        else
        {
            // ����������� ������� �� 1, �� �� ������ ������������� ��������
            int currentStreak = PlayerPrefs.GetInt(LoginStreakKey, 1);
            PlayerPrefs.SetInt(LoginStreakKey, Mathf.Min(currentStreak + 1, MaxStreakDays));
        }

        // ��������� ������� ���� ��� ��������� ���� �����
        PlayerPrefs.SetString(LastLoginKey, currentDate.ToString("yyyy-MM-dd"));
        PlayerPrefs.Save();
    }

    private void ClaimReward()
    {
        int loginStreak = PlayerPrefs.GetInt(LoginStreakKey, 0);

        if (loginStreak > 0)
        {
            Debug.Log($"���� ������� �� {loginStreak} ���� ����� ������!");

            PlayerBalance.Instance.AddMoney(_claimRewardConteiners[loginStreak].AwardAmount);
            _claimRewardButton.onClick.RemoveListener(ClaimReward);
            // ���� ��������� ��������, �������� �������
            if (loginStreak >= MaxStreakDays)
            {
                PlayerPrefs.SetInt(LoginStreakKey, 0);
            }

            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("��� ��������� ������.");
        }
    }
    private void OnEnable()
    {
        //CheckLoginStreak();
    }
}
