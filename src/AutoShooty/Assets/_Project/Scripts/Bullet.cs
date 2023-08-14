using UnityEngine;
using QGame;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : QScript
{
    [SerializeField]
    float _baseSpeed;

    Vector3 _currentDirection;
    float _currentSpeed;

    private void Awake()
    {
        //OnEveryUpdate += Move;
        //_currentSpeed = _baseSpeed;
    }

    private void Move()
    {
        //transform.Translate(_currentDirection * _currentSpeed);
    }
}