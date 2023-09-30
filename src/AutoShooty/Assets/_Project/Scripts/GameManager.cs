using UnityEngine;
using QGame;
using Messaging;
using System.Linq;
using System.Collections.Generic;
using Cinemachine;

public class GameManager : QScript
{
    public List<EnemyBase> StaticEnemies;

    
    public RewardsManager RewardsManager;

    public static StatModifierHolder GlobalStats { get; private set; }
    public static string GlobalName => "global";

    [SerializeField]
    private CinemachineVirtualCamera _camera;
    [SerializeField]
    private PickupReach _pickupReach;
    [SerializeField]
    private PlayerAvatar _playerPrefab;
    public static PlayerAvatar Player { get; private set; }

    private void Awake()
    {
        ServiceInitializer.Initialize();

        GlobalStats = StatModifierHolder.GenerateWeaponStats(GlobalName);
        Locator.ModifierDistributor.Register(GlobalStats);

        CreatePlayer();
        RegisterMessageHub();

        // gross, but they have no real reason/way to meet otherwise yet
        _pickupReach.OnXpGain += RewardsManager.OnXpGain;

        if (StaticEnemies.Any())
        {
            StaticEnemies.ForEach(
                e => Locator.MessageHub.QueueMessage(EnemyBase.MessageName,
                new EnemySpawnedMessageArgs { Enemy = e }));
        }
    }

    private void RegisterMessageHub()
    {
        var messageHub = Locator.MessageHub as MessageHub
                    ?? throw new UnityException("MessageHub could not be found");
        OnEveryUpdate += () => messageHub.Update();
    }

    private void CreatePlayer()
    {
        Player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        Player.name = "player";
        _pickupReach = Player.PickupReach;
        _camera.Follow = Player.transform;
    }
}
