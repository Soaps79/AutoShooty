using UnityEngine;
using QGame;

public class SaberWeapon : WeaponBase
{
    [SerializeField]
    private SaberProjectile _prefabProjectile;
    [SerializeField]
    private ProjectileConfig _projectileConfig;

    public override string Id => "saber";

    protected override void Fire()
    {
        var proj = Instantiate(_prefabProjectile, transform.position, Quaternion.identity);
        SetScale(proj.transform);
        proj.Combatant.Initialize(_modifiers.GetDamageCalc());
        proj.Initialize(_projectileConfig, new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z));
    }
}
