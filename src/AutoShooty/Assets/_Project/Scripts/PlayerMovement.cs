using UnityEngine;
using QGame;

public enum MovementDirection
{
    None, Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft
}

public class PlayerMovement : QScript
{
    [SerializeField]
    float _baseSpeed;
    [SerializeField]
    float _speedModifier;

    MovementDirection _currentDirection;

    private void Awake()
    {
        OnEveryUpdate += CheckMovementKeys;
        OnEveryUpdate += Move;
    }

    private void CheckMovementKeys()
    {
        if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
            _currentDirection = MovementDirection.None;

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            _currentDirection = MovementDirection.UpLeft;
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            _currentDirection = MovementDirection.DownLeft;
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            _currentDirection = MovementDirection.UpRight;
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            _currentDirection= MovementDirection.DownRight;
        else if (Input.GetKey(KeyCode.A))
            _currentDirection = MovementDirection.Left;
        else if (Input.GetKey(KeyCode.W))
            _currentDirection = MovementDirection.Up;
        else if (Input.GetKey(KeyCode.D))
            _currentDirection = MovementDirection.Right;
        else if (Input.GetKey(KeyCode.S))
            _currentDirection = MovementDirection.Down;
        else
            _currentDirection = MovementDirection.None;
    }

    private void Move()
    {
        if (_currentDirection == MovementDirection.None)
            return;

        Vector3 movement;

        switch (_currentDirection)
        {
            case MovementDirection.UpLeft:
                movement = new Vector3(-1, 1, 0);
                break;
            case MovementDirection.Up:
                movement = new Vector3(0, 1, 0);
                break;
            case MovementDirection.UpRight:
                movement = new Vector3(1, 1, 0);
                break;
            case MovementDirection.Right:
                movement = new Vector3(1, 0, 0);
                break;
            case MovementDirection.DownRight:
                movement = new Vector3(1, -1, 0);
                break;
            case MovementDirection.Down:
                movement = new Vector3(0, -1, 0);
                break;
            case MovementDirection.DownLeft:
                movement = new Vector3(-1, -1, 0);
                break;
            case MovementDirection.Left:
                movement = new Vector3(-1, 0, 0);
                break;
            default:
                movement = new Vector3(0, 0, 0);
                break;
        }
        
        transform.Translate(movement * (_baseSpeed * (1 + _speedModifier)));
    }
}
