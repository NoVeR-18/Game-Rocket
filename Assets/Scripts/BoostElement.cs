using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostElement : MonoBehaviour
{
    [SerializeField]
    private Button _upgradeButton;
    [SerializeField]
    private TextMeshProUGUI _costUpgrade;
    [SerializeField]
    private List<PointConteiner> _completedUpgrades;
    [SerializeField]
    private typeElement _typeElement;

    private int _rankUpgrades;
    private int _cost = 1000;

    private void Start()
    {
        _rankUpgrades = PlayerPrefs.GetInt($"{_typeElement}", 0);
        _cost = 1000 * (int)Mathf.Pow(2f, _rankUpgrades);
        _costUpgrade.text = GameManager.FormatNumber(_cost);
        for (int i = 0; i < _rankUpgrades; i++)
        {
            _completedUpgrades[i].CompleteUpgrade();
        }
        _upgradeButton.onClick.AddListener(() =>
        {
            switch (_typeElement)
            {
                case typeElement.Autopilot: OnIncreaseMaxEnergy(); break;
                case typeElement.Wings: OnIncreaseProfit(); break;
                case typeElement.Fuselage: OnIncreaseProfit(); break;
                case typeElement.Reactor: OnIncreaseMaxEnergy(); break;
            }
        });
    }


    private void OnIncreaseMaxEnergy()
    {
        if (PlayerBalance.Instance.CanSpendMoney(_cost))
        {
            PlayerBalance.Instance.SpendMoney(_cost);
            GameManager.Instance._gameUi.UpgradeMaxEnergy(500);
            _rankUpgrades++;
            _cost = 1000 * (int)Mathf.Pow(2f, _rankUpgrades);
            _costUpgrade.text = GameManager.FormatNumber(_cost);
            PlayerPrefs.SetInt($"{_typeElement}", _rankUpgrades);
            PlayerPrefs.Save();
            Debug.Log("maxenergy++");
        }
    }
    private void OnIncreaseProfit()
    {
        if (PlayerBalance.Instance.CanSpendMoney(_cost))
        {
            Debug.Log("coin++");
            PlayerBalance.Instance.SpendMoney(_cost);
            GameManager.Instance._gameUi.UpgradeIncreaseProfit();
            _rankUpgrades++;
            _cost = 1000 * (int)Mathf.Pow(2f, _rankUpgrades);
            _costUpgrade.text = GameManager.FormatNumber(_cost);
            PlayerPrefs.SetInt($"{_typeElement}", _rankUpgrades);
            PlayerPrefs.Save();
            // Добавьте код для увеличения прибыли
        }
    }
    public void FreeUpgrade()
    {
        _rankUpgrades++;
        _cost = 1000 * (int)Mathf.Pow(2f, _rankUpgrades);
        _costUpgrade.text = GameManager.FormatNumber(_cost);

        PlayerPrefs.SetInt($"{_typeElement}", _rankUpgrades);
        PlayerPrefs.Save();

        // Применяем соответствующее улучшение
        switch (_typeElement)
        {
            case typeElement.Autopilot:
            case typeElement.Reactor:
                GameManager.Instance._gameUi.UpgradeMaxEnergy(500);
                break;
            case typeElement.Wings:
            case typeElement.Fuselage:
                GameManager.Instance._gameUi.UpgradeIncreaseProfit();
                break;
        }

        // Обновляем визуальную часть, если это требуется
        _completedUpgrades[_rankUpgrades - 1].CompleteUpgrade();

        Debug.Log($"{_typeElement} улучшен бесплатно.");
    }
}

enum typeElement
{
    Wings,
    Fuselage,
    Reactor,
    Autopilot
}