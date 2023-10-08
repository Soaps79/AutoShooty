using UnityEngine;
using QGame;

public class SaberWeapon : WeaponBase
{
    [SerializeField]
    private ProjectileConfig _projectileConfig;

    public override string Id => "saber";

    protected override void Fire()
    {
        var proj = GetProjectile() as SaberProjectile;
        proj.Initialize(_projectileConfig, new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z));
    }
}
