using UnityEngine;
using QGame;
using Messaging;

public class GameManager : QScript
{
    public Combatant Enemy;
    public GameObject PlayerObject;

    public static GameObject Player;

    private void Awake()
    {
        ServiceInitializer.Initialize();

        var messageHub = Locator.MessageHub as MessageHub;
        if (messageHub == null)
            throw new UnityException("MessageHub could not be found");
        OnEveryUpdate += () => messageHub.Update();

        Player = PlayerObject;
    }
}
