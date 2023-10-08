using UnityEngine;

public class GunProjectile : ProjectileBase
{
    [SerializeField]
    private float _lifetime;
    private float _elapsedLifetime;
    private float _currentSpeed;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        OnEveryUpdate += Move;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float speed)
    {
        _currentSpeed = speed;
    }

    private void Move()
    {
        _rigidBody.MovePosition(transform.position += transform.right * _currentSpeed * Time.deltaTime);
        _elapsedLifetime += Time.deltaTime;
        if(_elapsedLifetime > _lifetime)
            Destroy(gameObject);
    }
}