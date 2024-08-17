using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayConteiner : MonoBehaviour
{
    [SerializeField]
    private Image _collectedImage;
    [SerializeField]
    private TextMeshProUGUI _awardText;
    public int AwardAmount = 1000;
    private void Start()
    {
        _collectedImage.gameObject.SetActive(false);
        _awardText.text = GameManager.FormatNumber(AwardAmount);
    }

    public void CollectConteiner()
    {
        _collectedImage.gameObject.SetActive(true);

    }


}
