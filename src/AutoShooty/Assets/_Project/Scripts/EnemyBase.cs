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
        var dropper = GetComponent<PickupDropper>();
        var combatant = GetComponent<Combatant>();

        combatant.OnDeath += (comb1, comb2) => { dropper.TriggerDropRequest(); };
    }
}
