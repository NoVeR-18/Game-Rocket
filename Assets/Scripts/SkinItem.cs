using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinItem : MonoBehaviour
{
    [SerializeField] private Image _skinPreview; // Превью скина
    [SerializeField] private TextMeshProUGUI _priceText; // Текст цены
    [SerializeField] private Button _selectButton; // Кнопка выбора

    private Skin _skin;

    public void Initialize(Skin skin)
    {
        _skin = skin;

        // Обновляем интерфейс
        _skinPreview.sprite = skin.PreviewImage;

        if (_skin.IsPurchased)
        {
            _priceText.text = "Select";

            _selectButton.onClick.RemoveAllListeners();
            _selectButton.onClick.AddListener(SelectSkin);
            //_buyButton.interactable = false;
            //_selectButton.interactable = true;
        }
        else
        {
            _priceText.text = GameManager.FormatNumber(skin.Price);
            //_buyButton.interactable = PlayerBalance.Instance.CanSpendMoney(skin.Price);
            _selectButton.onClick.RemoveAllListeners();
            _selectButton.onClick.AddListener(BuySkin);
        }

        // Привязываем события
        _selectButton.onClick.AddListener(BuySkin);
    }

    private void BuySkin()
    {
        if (PlayerBalance.Instance.CanSpendMoney(_skin.Price))
        {
            PlayerBalance.Instance.SpendMoney(_skin.Price);
            _skin.IsPurchased = true;

            PlayerPrefs.SetInt(_skin.SkinID, 1);
            PlayerPrefs.Save();

            // Обновляем UI после покупки
            _priceText.text = "Select";
            _selectButton.onClick.RemoveAllListeners();
            _selectButton.onClick.AddListener(SelectSkin);
        }
    }

    private void SelectSkin()
    {
        if (_skin.IsPurchased)
        {
            PlayerPrefs.SetString("SelectedSkin", _skin.SkinID);
            PlayerPrefs.Save();

            GameManager.Instance.ApplySkin(_skin);

            Debug.Log($"Скин {_skin.SkinID} выбран.");
        }
    }
}
