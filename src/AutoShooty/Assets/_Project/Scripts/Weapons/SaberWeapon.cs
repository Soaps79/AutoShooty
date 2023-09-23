using UnityEngine;
using QGame;

public class SaberWeapon : QScript
{
    [SerializeField]
    private SaberProjectile _prefabProjectile;
    [SerializeField]
    private float _baseDelay;
    [SerializeField]
    private ProjectileConfig _projectileConfig;

    private void Awake()
    {
        StopWatch.AddNode("fire", _baseDelay).OnTick += Fire;
    }

    private void Fire()
    {
        var proj = Instantiate(_prefabProjectile, transform.position, Quaternion.identity);
        proj.Initialize(_projectileConfig, new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z));
    }
}
