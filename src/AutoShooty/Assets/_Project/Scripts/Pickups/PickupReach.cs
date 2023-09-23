using UnityEngine;
using QGame;

[RequireComponent(typeof(CircleCollider2D))]
public class PickupReach : QScript
{
    public float ReachDistance;
    private CircleCollider2D _collider;

    public int XpCount;

    private void OnTriggerEnter2D(Collider2D collision) => HandleCollision(collision);

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

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
        XpCount += pickup.Value;
        Destroy(pickup.gameObject);
    }
}
