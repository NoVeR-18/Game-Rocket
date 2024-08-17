using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : Tab
{
    [SerializeField] private List<Skin> _skins; // ������ ���� ��������� ������
    [SerializeField] private Transform _skinsContainer; // ��������� ��� ���������� ������ � UI
    [SerializeField] private GameObject _skinItemPrefab; // ������ UI-�������� �����


    private void Start()
    {
        // ������� UI-�������� ��� ������� �����
        UpdateBalance();
        PlayerBalance.UpdateBalanse += UpdateBalance;
        _backButton?.onClick.AddListener(GameManager.Instance.CloseWindow);

        foreach (Skin skin in _skins)
        {
            GameObject skinItemObject = Instantiate(_skinItemPrefab, _skinsContainer);
            SkinItem skinItem = skinItemObject.GetComponent<SkinItem>();
            skinItem.Initialize(skin);
        }
        // ��������� ��������� ���� �� ������ ��� �������

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
