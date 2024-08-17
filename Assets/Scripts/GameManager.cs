using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameUi _gameUi;
    public Spawner _spawner;
    public SettingMenu _settingMenu;
    public DailyMenu _dailyMenu;
    public BoostMenu _boostMenu;
    public SnipdropMenu _snipdropMenu;
    public ShopMenu _shopMenu;
    public MiniGameMenu _miniGameMenu;

    [Header("Navigation Buttons")]
    [SerializeField] private Button _boostButton;
    [SerializeField] private Button _dailyButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _fortuneButton;
    [SerializeField] private Button _miniGameButton;

    private Tab _currentOpenedTab;

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
    }
    private void Start()
    {
        OpenWindow(_gameUi);
        _settingButton.onClick.AddListener(() => OpenWindow(_settingMenu));
        _dailyButton.onClick.AddListener(() => OpenWindow(_dailyMenu));
        _boostButton.onClick.AddListener(() => OpenWindow(_boostMenu));
        _fortuneButton.onClick.AddListener(() => OpenWindow(_snipdropMenu));
        _shopButton.onClick.AddListener(() => OpenWindow(_shopMenu));
        _miniGameButton.onClick.AddListener(() => OpenWindow(_miniGameMenu));
    }

    public void OpenWindow(Tab tab)
    {
        _currentOpenedTab?.CloseTab();
        _currentOpenedTab = tab;
        _currentOpenedTab.OpenTab();
    }
    public void CloseWindow()
    {
        OpenWindow(_gameUi);
    }
    public static string FormatNumber(int number)
    {
        if (number >= 1000000)
        {
            return $"{number / 1000000.0:0.##}M"; // ‘орматирует миллионы
        }
        if (number >= 1000)
        {
            return $"{number / 1000.0:0.##}K"; // ‘орматирует тыс€чи
        }
        return number.ToString(); // ƒл€ чисел меньше тыс€чи
    }
    public void ApplySkin(Skin skin)
    {
        // «десь добавьте код дл€ применени€ скина на игрока
        // Ќапример, замена спрайта или текстуры на игровом объекте игрока
        _gameUi.SetSkin(skin.PreviewImage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _currentOpenedTab != _gameUi)
        {
            CloseWindow();
        }
    }
}
