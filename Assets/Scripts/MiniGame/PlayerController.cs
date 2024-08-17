using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // �������� ����������� ������
    [SerializeField] private float screenEdgeBuffer = 0.1f; // ����� �� ����� ������ ��� �������������� ������ ������ �� ������� ������

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private float shootingInterval = 0.2f;
    private Vector2 _screenBounds;

    private void Start()
    {
        // ���������� ������� ������ � ������� ��������
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

        // ���������, ���� �� ������� ������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // �������� ������ �������
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
        }
        // ���������, ���� �� �������� ���� (��� ��)
        else if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        }

        // ���������� ������ � ������� �������
        if (targetPosition != Vector3.zero)
        {
            // �������� z-����������, ����� �� �������� ������ ��� �����������
            targetPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void ClampPosition()
    {
        // ������������ ������� ������, ����� �� ������� �� ������� ������
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
