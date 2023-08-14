using UnityEngine;
using QGame;

public class Gun : QScript
{
    [SerializeField]
    private float _baseDelay;
    [SerializeField]
    private Bullet _bullet;
    [SerializeField]
    private float _bulletSpeed;

    private void Awake()
    {
        StopWatch.AddNode("fire", _baseDelay).OnTick += Fire;
    }

    private void Fire()
    {
        //var direction = CreateRandomDirection();
        var rads = Random.value * Mathf.PI * 2;
        Debug.Log(rads);
        var direction = new Vector3(Mathf.Cos(rads), Mathf.Sin(rads), 0);
        var proj = Instantiate(_bullet, transform.position, Quaternion.identity);
        //proj.transform.rotation.SetFromToRotation(transform.position, transform.position +  direction);
        
        proj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (rads * Mathf.Rad2Deg) - 90));
        var rigidBody = proj.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(direction * _bulletSpeed);
    }

    private Vector3 CreateRandomDirection()
    {
        var rads = Random.value * Mathf.PI * 2;
        return new Vector3(Mathf.Cos(rads), Mathf.Sin(rads), 0);
    }
}
