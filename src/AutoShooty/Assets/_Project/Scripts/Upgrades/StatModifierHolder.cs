using System;
using System.Collections.Generic;

public class StatModifierHolder
{
    public string Name { get; private set; }
    private readonly Dictionary<StatModifierType, Stat> _statTable 
        = new Dictionary<StatModifierType, Stat>();

    public StatModifierHolder(string name)
    {
        Name = name;
    }

    public Stat this[StatModifierType key]
    {
        get => _statTable.ContainsKey(key) ? _statTable[key] : null;
        set => _statTable[key] = value;
    }

    public DamageCalc GetDamageCalc()
    {
        return new DamageCalc(
            _statTable[StatModifierType.MoreDamage].CurrentValue,
            _statTable[StatModifierType.CritChance].CurrentValue,
            _statTable[StatModifierType.CritMultiplier].CurrentValue);
    }

    public static StatModifierHolder GenerateWeaponStats(string name)
    {
        var dict = new StatModifierHolder(name);
        dict[StatModifierType.MoreDamage] = new Stat();
        dict[StatModifierType.IncreasedDamage] = new Stat();
        dict[StatModifierType.Multicast] = new Stat();
        dict[StatModifierType.AreaOfEffect] = new Stat();
        dict[StatModifierType.CritChance] = new Stat();
        dict[StatModifierType.CritMultiplier] = new Stat();
        dict[StatModifierType.IncreasedCastSpeed] = new Stat();
        dict[StatModifierType.Multicast] = new Stat();
        return dict;
    }

    public static StatModifierHolder GenerateAllStats(string name)
    {
        var dict = new StatModifierHolder(name);

        foreach (StatModifierType enumValue in Enum.GetValues(typeof(StatModifierType)))
        {
            dict[enumValue] = new Stat();
        }

        return dict;
    }
}