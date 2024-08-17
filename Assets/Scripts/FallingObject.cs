using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 2;
    public int coast;

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.Self);
        if (transform.position.y < -Camera.main.orthographicSize - transform.localScale.y)
        {
            Destroy(gameObject);
        }
    }
    void OnMouseDown()
    {
        GameManager.Instance._gameUi.SpendEnergy(this);
    }
    private void OnDestroy()
    {
        GameManager.Instance._spawner._asteroids.Remove(gameObject);
    }
}
