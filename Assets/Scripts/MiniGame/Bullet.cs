using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y > Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameManager.Instance._miniGameMenu.EnemyKilled();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameManager.Instance._miniGameMenu._createdObjects.Remove(gameObject);
    }
}
