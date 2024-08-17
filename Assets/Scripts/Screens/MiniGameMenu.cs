using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameMenu : Tab
{
    private int killCount = 0;
    private int rewardThreshold = 50;
    private int highScore = 0;
    [SerializeField]
    private TextMeshProUGUI _currentScoreText;
    [SerializeField]
    private TextMeshProUGUI _bestScoreText;
    [SerializeField]
    private TextMeshProUGUI _killsUntilRewardText;
    [SerializeField]
    private TextMeshProUGUI _awardCount;
    [SerializeField] private Transform _miniGame;

    private int _awardSize = 500;
    public List<GameObject> _createdObjects = new List<GameObject>();

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
        UpdateKillsUntilRewardUI();
    }

    private void OnEnable()
    {
        killCount = 0;
        UpdateScoreUI();
        _miniGame.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _miniGame?.gameObject.SetActive(false);
        foreach (var obj in _createdObjects)
        {
            Destroy(obj);
        }
    }

    public void EnemyKilled()
    {
        killCount++;

        UpdateScoreUI();
        UpdateKillsUntilRewardUI();

        if (killCount % rewardThreshold == 0)
        {
            GiveReward();
        }

        if (killCount > highScore)
        {
            highScore = killCount;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    private void GiveReward()
    {
        PlayerBalance.Instance.AddMoney(_awardSize);
    }

    private void UpdateKillsUntilRewardUI()
    {
        int killsUntilReward = rewardThreshold - (killCount % rewardThreshold);

        _awardCount.text = string.Format("{0:N0}", _awardSize);
        _killsUntilRewardText.text = $"{killsUntilReward}\nkills";
    }


    private void UpdateScoreUI()
    {
        _currentScoreText.text = $"SCORE {killCount}";
        _bestScoreText.text = $" Your best score: {highScore}";
    }
}
