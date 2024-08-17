using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    [SerializeField]
    protected Button _backButton;
    [SerializeField]
    protected TextMeshProUGUI _balance;
    protected void UpdateBalance()
    {
        _balance.text = string.Format("{0:N0}", PlayerBalance.Instance.currentBalance);
    }

    public void OpenTab()
    {
        gameObject.SetActive(true);
    }
    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
}
