using UnityEngine;
using QGame;
using UnityEngine.EventSystems;

public class Bullet : QScript
{
    [SerializeField]
    float _baseSpeed;

    Vector3 _currentDirection;
    float _currentSpeed;

    private void Awake()
    {
        OnEveryUpdate += Move;
        _currentSpeed = _baseSpeed;
    }

    public void Initialize(Vector3 direction)
    {
        _currentDirection = direction;
    }

    private void Move()
    {
        transform.position += transform.up * _currentSpeed;
        //transform.Translate(transform.up * _currentSpeed);
    }
}