using UnityEngine;
using QGame;
using DG.Tweening;

public class MagnetablePickup : Pickup
{
    public float MoveSpeed;

    private void Awake()
    {
        
    }

    public void BeginAttraction(Vector3 magnetPos)
    {
        OnEveryUpdate += Move;
    }

    private void Move()
    {
        var dir = (GameManager.Player.transform.position - transform.position).normalized;
        transform.Translate(dir * MoveSpeed * Time.deltaTime, Space.World);
    }
}
