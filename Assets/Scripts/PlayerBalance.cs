using System;
using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    public static PlayerBalance Instance;
    public int startingBalance = 100;
    public int currentBalance;
    public static Action UpdateBalanse;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        currentBalance = startingBalance;
        currentBalance = PlayerPrefs.GetInt("currentBalance", 100);
    }


    public void AddMoney(int amount)
    {
        currentBalance += amount;
        PlayerPrefs.SetInt("currentBalance", currentBalance);
        PlayerPrefs.Save();
        UpdateBalanse?.Invoke();
    }

    public bool SpendMoney(int amount)
    {
        if (currentBalance >= amount)
        {
            currentBalance -= amount;
            UpdateBalanse?.Invoke();
            PlayerPrefs.SetInt("currentBalance", currentBalance);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CanSpendMoney(int amount)
    {
        if (currentBalance >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetBalance()
    {
        return currentBalance;
    }

}
