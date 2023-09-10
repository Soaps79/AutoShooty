using UnityEngine;
using QGame;
using Messaging;

public class  EnemySpawnedMessageArgs : MessageArgs
{
    public EnemyBase Enemy;
}

public class EnemyBase : QScript
{
    public const string MessageName = "EnemySpawned";

    private void Start()
    {
        Locator.MessageHub.QueueMessage(MessageName, new EnemySpawnedMessageArgs { Enemy = this });
    }
}
