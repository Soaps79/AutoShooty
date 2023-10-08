using UnityEngine;
using QGame;

[RequireComponent(typeof(Combatant))]
public class ProjectileBase : QScript
{
    public Combatant Combatant { get; private set; }

    public void InitCombatant()
    {
        Combatant = gameObject.GetComponent<Combatant>();
    }
}
