using UnityEngine;
using QGame;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class SaberProjectile : QScript
{
    [SerializeField]
    private Vector3 _origin;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _angle;
    [SerializeField]
    private float _spawnScale;
    [SerializeField]
    private float _spawnScaleTime;

    [SerializeField]
    private float _localRotationSpeed;

    private void Awake()
    {
        OnEveryUpdate += Spiral;
        OnEveryUpdate += Spin;
    }

    public void Initialize(ProjectileConfig config, Vector3 position)
    {
        _origin = position;
        _speed = config.Speed;
        InitializeSpawnScaling();

        StopWatch.AddNode("t", config.Lifetime, true).OnTick += () => { Destroy(gameObject); };
    }

    private void InitializeSpawnScaling()
    {
        var intended = transform.localScale;
        var start = intended * _spawnScale;
        transform.localScale = start;
        transform.DOScale(intended, _spawnScaleTime);
    }

    private void Spiral()
    {
        Vector3 direction = _origin - transform.position;
        direction = Quaternion.Euler(0, 0, _angle) * direction;
        float distanceThisFrame = _speed * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void Spin()
    {
        transform.Rotate(0, 0, _localRotationSpeed * Time.deltaTime);
    }
}
