using UnityEngine;
using QGame;
using System;

[Serializable]
public enum RewardType
{
    Stat,
    Weapon
}

public abstract class RewardTemplate : ScriptableObject
{
    public abstract RewardType RewardType { get; }
}
