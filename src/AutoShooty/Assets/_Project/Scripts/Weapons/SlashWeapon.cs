using UnityEngine;
using QGame;

public class SlashWeapon : WeaponBase
{
    [SerializeField]
    private float distanceFromCenter;

    public override string Id => "slash";

    protected override void Fire()
    {
        var heading = GetDirectionToMouse();
        var spawnLoc = transform.position + (heading * distanceFromCenter);
        GetProjectileFacingMousePointer(spawnLoc);        
    }

    private void Awake()
    {
        
    }
}
