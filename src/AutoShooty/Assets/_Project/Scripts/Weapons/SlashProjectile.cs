using UnityEngine;
using QGame;
using TMPro;

public class SlashProjectile : QScript
{
    public Combatant Combatant { get; private set; }

    private void Awake()
    {
        Combatant = GetComponent<Combatant>();
    }

    public void Complete()
    {
        Destroy(gameObject);
    }
}
