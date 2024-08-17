using UnityEngine;

[System.Serializable]
public class Skin : MonoBehaviour
{
    public string SkinID; // ���������� ������������� �����
    public Sprite PreviewImage; // ����������� ������ �����
    public int Price; // ��������� �����
    public bool IsPurchased; // ��� �� ���� ������

    public Skin(string skinID, Sprite previewImage, int price)
    {
        SkinID = skinID;
        PreviewImage = previewImage;
        Price = price;
        IsPurchased = PlayerPrefs.GetInt(SkinID, 0) == 1;
    }
}
