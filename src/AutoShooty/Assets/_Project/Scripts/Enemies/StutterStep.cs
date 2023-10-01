using UnityEngine;
using QGame;

public class StutterStep : QScript
{
    [SerializeField]
    private float _moveLength;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _moveDelay;
    [SerializeField]
    private bool _isMoving;

    private Vector3 _movementDirection;
    private float _currentElapsed;

    private void Awake()
    {
        OnEveryUpdate += MoveIfMoving;
    }

    private void MoveIfMoving()
    {
        if(!_isMoving)
        {
            _currentElapsed += Time.deltaTime;
            if(_currentElapsed > _moveDelay)
            {
                _movementDirection = (GameManager.Player.transform.position - transform.position).normalized;
                _isMoving = true;
                _currentElapsed = 0;
            }
        }
        else
        {
            transform.Translate(_movementDirection * _moveSpeed * Time.deltaTime, Space.World);
            _currentElapsed += Time.deltaTime;
            if(_currentElapsed > _moveLength)
            {
                _isMoving = false;
                _currentElapsed = 0;
            }
        }
    }
}
