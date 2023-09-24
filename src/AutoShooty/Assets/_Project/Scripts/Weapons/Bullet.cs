using UnityEngine;
using QGame;
using UnityEngine.EventSystems;

public class Bullet : QScript
{
    [SerializeField]
    private float _lifetime;
    private float _elapsedLifetime;
    private float _currentSpeed;
    private Rigidbody2D _rigidBody;
    public Combatant Combatant { get; private set; }

    private void Awake()
    {
        OnEveryUpdate += Move;
        _rigidBody = GetComponent<Rigidbody2D>();
        Combatant = GetComponent<Combatant>();
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