using UnityEngine;

[System.Serializable]
public class Skin : MonoBehaviour
{
    public string SkinID; // Уникальный идентификатор скина
    public Sprite PreviewImage; // Изображение превью скина
    public int Price; // Стоимость скина
    public bool IsPurchased; // Был ли скин куплен

    public Skin(string skinID, Sprite previewImage, int price)
    {
        SkinID = skinID;
        PreviewImage = previewImage;
        Price = price;
        IsPurchased = PlayerPrefs.GetInt(SkinID, 0) == 1;
    }
}
