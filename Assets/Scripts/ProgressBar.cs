using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image _line;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TextMeshProUGUI _percent;
    [SerializeField]
    private float _maxProgress = 1000000;

    [SerializeField]
    private Image _background;
    [SerializeField]
    private Sprite[] _backgroundImages;
    [SerializeField]
    private Image _lowerBarImage;
    [SerializeField]
    private Sprite[] _loverBars;

    private float progress = 0;
    private int currentBackgroundIndex = 0;
    private int currentLowerBarIndex = 0;

    private void Start()
    {
        progress = PlayerPrefs.GetFloat("ProgressBar", progress);
        currentBackgroundIndex = PlayerPrefs.GetInt("BackgroundIndex", 0);
        currentLowerBarIndex = PlayerPrefs.GetInt("LowerBarIndex", 0);
        _background.sprite = _backgroundImages[currentBackgroundIndex];
        _lowerBarImage.sprite = _backgroundImages[currentBackgroundIndex];
        SetProgress();
    }

    public void SetProgress(float value = 0)
    {
        progress += value;
        PlayerPrefs.SetFloat("ProgressBar", progress);
        PlayerPrefs.Save();

        var progressToUi = Mathf.Clamp01(progress / _maxProgress);

        RectTransform bgRectTransform = _line.rectTransform;
        RectTransform imgRectTransform = _icon.rectTransform;

        float newY = Mathf.Lerp(-bgRectTransform.rect.height / 2 + imgRectTransform.rect.height / 2, bgRectTransform.rect.height / 2 - imgRectTransform.rect.height / 2, progressToUi);

        imgRectTransform.anchoredPosition = new Vector2(imgRectTransform.anchoredPosition.x, newY);

        float percentage = progressToUi * 100f;
        _percent.text = percentage.ToString("F2") + "%";

        // Проверка и смена фона
        CheckAndChangeBackground(progressToUi);
    }

    private void CheckAndChangeBackground(float progressToUi)
    {
        int backgroundStage = Mathf.FloorToInt(progressToUi * _backgroundImages.Length);

        if (backgroundStage > currentBackgroundIndex && backgroundStage < _backgroundImages.Length)
        {
            currentBackgroundIndex = backgroundStage;
            currentLowerBarIndex = backgroundStage;
            _background.sprite = _backgroundImages[currentBackgroundIndex];
            _lowerBarImage.sprite = _backgroundImages[currentLowerBarIndex];
            PlayerPrefs.SetInt("BackgroundIndex", currentBackgroundIndex);
            PlayerPrefs.SetInt("LowerBarIndex", currentBackgroundIndex);
            PlayerPrefs.Save();
        }

        if (progressToUi >= 1f)
        {
            currentBackgroundIndex = 0;
            _background.sprite = _backgroundImages[currentBackgroundIndex];
            _lowerBarImage.sprite = _backgroundImages[currentBackgroundIndex];
            PlayerPrefs.SetInt("BackgroundIndex", currentBackgroundIndex);
            PlayerPrefs.Save();
            progress = 0;
            PlayerPrefs.SetFloat("ProgressBar", progress);
            PlayerPrefs.Save();
        }
    }
}
