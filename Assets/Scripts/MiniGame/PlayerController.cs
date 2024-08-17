using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Скорость перемещения игрока
    [SerializeField] private float screenEdgeBuffer = 0.1f; // Буфер по краям экрана для предотвращения выхода игрока за пределы экрана

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private float shootingInterval = 0.2f;
    private Vector2 _screenBounds;

    private void Start()
    {
        // Определяем границы экрана в мировых единицах
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void Update()
    {
        HandleInput();
        ClampPosition();
        HandleShooting();
    }

    private void HandleInput()
    {
        Vector3 targetPosition = Vector3.zero;

        // Проверяем, есть ли касание экрана
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Получаем первое касание
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
        }
        // Проверяем, есть ли движение мыши (для ПК)
        else if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        }

        // Перемещаем игрока к позиции касания
        if (targetPosition != Vector3.zero)
        {
            // Заменяем z-координату, чтобы не изменять высоту при перемещении
            targetPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void ClampPosition()
    {
        // Ограничиваем позицию игрока, чтобы не выходил за пределы экрана
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -_screenBounds.x + screenEdgeBuffer, _screenBounds.x - screenEdgeBuffer);
        transform.position = position;
    }

    private void HandleShooting()
    {
        if (Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + shootingInterval;
        }
    }

    private float nextShotTime;


    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        GameManager.Instance._miniGameMenu._createdObjects.Add(bullet);
    }

}
