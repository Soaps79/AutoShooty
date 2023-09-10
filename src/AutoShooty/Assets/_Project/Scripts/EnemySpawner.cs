using UnityEngine;
using QGame;
using DG.Tweening;

public class EnemySpawner : QScript
{
    [SerializeField]
    private float _spawnDelay;
    [SerializeField]
    private int _minSpawn;
    [SerializeField]
    private int _maxSpawn;
    [SerializeField] 
    private int _spawnCountIncrement;

    private float _elapsedDelay;

    [SerializeField]
    private EnemyBase _enemyPrefab;

    [SerializeField]
    private CameraExtents _cameraExtents;


    private void Awake()
    {
        OnEveryUpdate += CheckForEnemySpawn;
    }

    private void CheckForEnemySpawn()
    {
        _elapsedDelay += Time.deltaTime;
        if(_elapsedDelay >= _spawnDelay)
        {
            SpawnEnemies();
            _minSpawn += _spawnCountIncrement;
            _maxSpawn += _spawnCountIncrement;
            _elapsedDelay = 0;
        }
    }

    private void SpawnEnemies()
    {
        var count = Random.Range(_minSpawn, _maxSpawn);
        //Camera.main.
        for (int i = 0; i < count; i++)
        {
            var ltrb = Random.Range(1, 4);
            Vector3 spawnPos;

            switch (ltrb)
            {
                case 1: // left
                    spawnPos = new Vector3(_cameraExtents.TopLeft.x, Random.Range(_cameraExtents.TopLeft.y, _cameraExtents.BottomLeft.y), 1);
                    break;
                case 2: // top
                    spawnPos = new Vector3(Random.Range(_cameraExtents.TopLeft.x, _cameraExtents.TopRight.x), _cameraExtents.TopLeft.y, 1);
                    break;
                case 3: // right
                    spawnPos = new Vector3(_cameraExtents.TopRight.x, Random.Range(_cameraExtents.TopRight.y, _cameraExtents.BottomRight.y), 1);
                    break;
                case 4: // bottom
                    spawnPos = new Vector3(Random.Range(_cameraExtents.BottomLeft.x, _cameraExtents.BottomRight.x), _cameraExtents.BottomLeft.y, 1);
                    break;
                default:
                    spawnPos = new Vector3(1, 1, 1);
                    break;
            }

            var enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            Locator.MessageHub.QueueMessage(EnemyBase.MessageName, new EnemySpawnedMessageArgs { Enemy = enemy });
        }
    }
}
