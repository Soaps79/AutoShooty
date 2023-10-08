using UnityEngine;

public class GunWeapon : WeaponBase
{
    [SerializeField]
    private float _bulletSpeed;

    public override string Id => "gun";

    protected override void Fire()
    {
        FireTowardsMousePointer();
    }

    private void FireTowardsMousePointer()
    {
        var proj = GetProjectileFacingMousePointer(transform.position) as GunProjectile;
        proj.Initialize(_bulletSpeed);
    }

    // random direction
    //private void Fire()
    //{
    //    var rads = Random.value * Mathf.PI * 2;
    //    Debug.Log(rads);
    //    var direction = new Vector3(Mathf.Cos(rads), Mathf.Sin(rads), 0);
    //    var proj = Instantiate(_bullet, transform.position, Quaternion.identity);

    //    proj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (rads * Mathf.Rad2Deg) - 90));
    //    var rigidBody = proj.GetComponent<Rigidbody2D>();
    //    rigidBody.AddForce(direction * _bulletSpeed);
    //}
}
