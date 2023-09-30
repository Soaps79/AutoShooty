using System;

public enum StatModifierType
{
    Health,
    MovementSpeed,
    MoreDamage,
    IncreasedDamage,
    CritChance,
    CritMultiplier,
    AreaOfEffect,
    Multicast,
    IncreasedCastSpeed
}

public class Stat
{
    public float LocalValue;
    public Stat Parent;
    public float CurrentValue => Parent != null 
        ? LocalValue + Parent.CurrentValue : LocalValue;
}

public class StatModifier
{
    public StatModifierType Type;
    public float Amount;
    public string ConsumerId;
}

[Serializable]
public class StatAmount
{
    public StatModifierType Type;
    public float Amount;
}
