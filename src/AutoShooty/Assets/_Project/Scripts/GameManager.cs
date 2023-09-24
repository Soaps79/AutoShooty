using UnityEngine;
using QGame;
using Messaging;
using System.Linq;
using System.Collections.Generic;

public class GameManager : QScript
{
    public List<EnemyBase> StaticEnemies;
    public GameObject PlayerObject;
    public static GameObject Player;

    public PickupReach PickupReach;
    public RewardsManager RewardsManager;

    public static StatModifierHolder GlobalStats { get; private set; }
    public static string GlobalName => "global";

    private void Awake()
    {
        ServiceInitializer.Initialize();

        GlobalStats = StatModifierHolder.GenerateWeaponStats(GlobalName);
        Locator.ModifierDistributor.Register(GlobalStats);

        var messageHub = Locator.MessageHub as MessageHub;
        if (messageHub == null)
            throw new UnityException("MessageHub could not be found");
        OnEveryUpdate += () => messageHub.Update();

        Player = PlayerObject;

        // gross, but they have no real reason/way to meet otherwise yet
        PickupReach.OnXpGain += RewardsManager.OnXpGain;

        if (StaticEnemies.Any())
        {
            StaticEnemies.ForEach(
                e => Locator.MessageHub.QueueMessage(EnemyBase.MessageName,
                new EnemySpawnedMessageArgs { Enemy = e }));
        }
    }
}
