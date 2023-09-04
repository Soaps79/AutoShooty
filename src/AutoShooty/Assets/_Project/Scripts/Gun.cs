using UnityEngine;
using QGame;
using static UnityEditor.PlayerSettings;

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
        var proj = Instantiate(_bullet, transform.position, Quaternion.identity);

        Vector3 mousePos = Input.mousePosition;
        Vector3 heading = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        heading = new Vector3(heading.x, heading.y, 0.0f).normalized;
        //newBullet.Direction = heading.normalized;

        //var angleVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //angleVector.z = 0;
        //angleVector.Normalize();
        float angle = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;
        //Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        //var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        proj.transform.eulerAngles = new Vector3(0, 0, angle - 90);
        proj.Initialize(heading);
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
