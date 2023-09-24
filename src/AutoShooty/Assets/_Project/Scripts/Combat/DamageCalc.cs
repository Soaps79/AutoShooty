using UnityEngine;

public class DamageCalc
{
    public float Damage { get; private set; }
    public float CritChance { get; private set; }
    public float CritMultiplier { get; private set; }

    public DamageCalc(float damage, float critChance, float critMultiplier)
    {
        Damage = damage;
        CritChance = critChance;
        CritMultiplier = critMultiplier;
    }

    public (float damage, bool isCritical) Roll()
    {
        var critRoll = Random.value;
        return critRoll < CritChance ? (Damage + Damage * CritMultiplier, true) : (Damage, false);
    }
}
