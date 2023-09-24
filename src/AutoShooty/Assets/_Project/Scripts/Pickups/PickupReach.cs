using UnityEngine;
using QGame;
using System;

[RequireComponent(typeof(CircleCollider2D))]
public class PickupReach : QScript
{
    public float ReachDistance;

    private void OnTriggerEnter2D(Collider2D collision) => HandleCollision(collision);

    public Action<int> OnXpGain;

    private void HandleCollision(Collider2D collision)
    {
        var pickup = collision.GetComponent<Pickup>();
        if(pickup != null) 
        {
            HandlePickup(pickup);
        }
    }

    private void HandlePickup(Pickup pickup)
    {
        OnXpGain?.Invoke(pickup.Value);
        Destroy(pickup.gameObject);
    }
}
