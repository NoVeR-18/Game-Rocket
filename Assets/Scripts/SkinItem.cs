using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinItem : MonoBehaviour
{
    [SerializeField] private Image _skinPreview; // ������ �����
    [SerializeField] private TextMeshProUGUI _priceText; // ����� ����
    [SerializeField] private Button _selectButton; // ������ ������

    private Skin _skin;

    public void Initialize(Skin skin)
    {
        _skin = skin;

        // ��������� ���������
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

        // ����������� �������
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

            // ��������� UI ����� �������
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

            Debug.Log($"���� {_skin.SkinID} ������.");
        }
    }
}
