using UnityEngine;
using QGame;
using UnityEngine.EventSystems;

public class Bullet : QScript
{
    [SerializeField]
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
        _rigidBody.MovePosition(transform.position += transform.up * _currentSpeed * Time.deltaTime);
        //transform.position += transform.up * _currentSpeed * Time.deltaTime;
    }
}