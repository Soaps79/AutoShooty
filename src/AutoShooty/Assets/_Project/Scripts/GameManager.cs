using UnityEngine;
using QGame;

public class GameManager : QScript
{
    public Combatant Enemy;

    private void Awake()
    {
        // temp text setup
        var textGenerate = GetComponent<DamageTextGenerator>();
        textGenerate.HandleEnemyCreated(Enemy);
    }
}
