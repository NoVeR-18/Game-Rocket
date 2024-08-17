using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : Tab
{
    [SerializeField] private List<Skin> _skins; // Список всех доступных скинов
    [SerializeField] private Transform _skinsContainer; // Контейнер для размещения скинов в UI
    [SerializeField] private GameObject _skinItemPrefab; // Префаб UI-элемента скина


    private void Start()
    {
        // Создаем UI-элементы для каждого скина
        UpdateBalance();
        PlayerBalance.UpdateBalanse += UpdateBalance;
        _backButton?.onClick.AddListener(GameManager.Instance.CloseWindow);

        foreach (Skin skin in _skins)
        {
            GameObject skinItemObject = Instantiate(_skinItemPrefab, _skinsContainer);
            SkinItem skinItem = skinItemObject.GetComponent<SkinItem>();
            skinItem.Initialize(skin);
        }
        // Применяем выбранный скин на игрока при запуске

    }
    public void TryUploadSkin()
    {
        string selectedSkinID = PlayerPrefs.GetString("SelectedSkin", "1");
        if (!string.IsNullOrEmpty(selectedSkinID))
        {
            Skin selectedSkin = _skins.Find(skin => skin.SkinID == selectedSkinID);
            if (selectedSkin != null)
            {
                GameManager.Instance.ApplySkin(selectedSkin);
            }
        }
    }

}
