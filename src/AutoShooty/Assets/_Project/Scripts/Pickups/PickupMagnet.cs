using UnityEngine;
using QGame;
using System;

[RequireComponent(typeof(CircleCollider2D))]
public class PickupMagnet : QScript
{
    CircleCollider2D _collider;

    private void OnTriggerEnter2D(Collider2D collision) => HandleCollision(collision);

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        // set radius
    }

    private void HandleCollision(Collider2D collision)
    {
        var pickup = collision.GetComponent<MagnetablePickup>();
        if(pickup != null) 
        {
            pickup.BeginAttraction(transform.position);
        }
    }
}
