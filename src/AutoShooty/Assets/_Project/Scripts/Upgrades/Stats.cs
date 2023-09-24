public enum StatModifierType
{
    Health,
    MovementSpeed,
    MoreDamage,
    IncreasedDamage,
    CritChance,
    CritMultiplier,
    AreaOfEffect,
    Multicast
}

public class Stat
{
    public float LocalValue;
    public Stat Parent;
    public float CurrentValue => LocalValue + Parent.CurrentValue;
}

public class StatModifier
{
    public StatModifierType Type;
    public float Amount;
    public string ConsumerId;
}
