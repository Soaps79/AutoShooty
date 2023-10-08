using UnityEngine;
using QGame;
using System.Collections.Generic;
using System;

[Serializable]
public class WeaponStatBase
{
    public float Damage;
    public float CritChance;
    public float CritMultiplier;
    public float IncreasedCastSpeed;
    public float AreaOfEffect;
    public float Multicast;

    public float BaseCastDelay;
}

public abstract class WeaponBase : QScript
{
    public abstract string Id { get; }
    [SerializeField]
    private WeaponStatBase _baseStats;
    protected StatModifierHolder _modifiers;
    private Stat _aoeModifier;
    
    private float _baseCastDelay;
    private float _elapsedSinceLastFire;
    private float _nextFire;

    public void Initialize(StatModifierHolder parent)
    {
        _modifiers = StatModifierHolder.GenerateWeaponStats(Id);

        SetStatsToBase();
        SetParents(parent);
        Locator.ModifierDistributor.Register(_modifiers);

        RestartTimer();
        OnEveryUpdate += UpdateTimer;
    }

    protected virtual void OnInitialize() { }

    protected void SetScale(Transform transform)
    {
        transform.localScale = transform.localScale + _aoeModifier.CurrentValue * transform.localScale;
    }

    private void UpdateTimer()
    {
        _elapsedSinceLastFire += Time.deltaTime;

        if (_elapsedSinceLastFire >= _nextFire)
        {
            Fire();
            RestartTimer(_elapsedSinceLastFire - _nextFire);
        }
    }

    protected abstract void Fire();

    private void RestartTimer(float carriedOver = 0)
    {
        _elapsedSinceLastFire = carriedOver;
        var result = _baseCastDelay - _baseCastDelay * _modifiers[StatModifierType.IncreasedCastSpeed].CurrentValue;
        _nextFire = result > 0 ? result : 0;
    }

// This is where the design decisions re: how global vs local stats are handled
    // Register the parent on the stats which will combine
    private void SetParents(StatModifierHolder parent)
    {
        _aoeModifier = parent[StatModifierType.AreaOfEffect];
        if (_aoeModifier == null)
            throw new UnityException($"Weapon {Id} given parent with no aoe modifier");

        _modifiers[StatModifierType.IncreasedDamage].Parent = parent[StatModifierType.IncreasedDamage];
        _modifiers[StatModifierType.CritChance].Parent = parent[StatModifierType.CritChance];
        _modifiers[StatModifierType.CritMultiplier].Parent = parent[StatModifierType.CritMultiplier];
        _modifiers[StatModifierType.IncreasedCastSpeed].Parent = parent[StatModifierType.IncreasedCastSpeed];
        _modifiers[StatModifierType.AreaOfEffect].Parent = parent[StatModifierType.AreaOfEffect];
        _modifiers[StatModifierType.Multicast].Parent = parent[StatModifierType.Multicast];
    }

    private void SetStatsToBase()
    {
        _baseCastDelay = _baseStats.BaseCastDelay;
        _modifiers[StatModifierType.MoreDamage].LocalValue = _baseStats.Damage;
        _modifiers[StatModifierType.CritChance].LocalValue = _baseStats.CritChance;
        _modifiers[StatModifierType.CritMultiplier].LocalValue = _baseStats.CritMultiplier;
        _modifiers[StatModifierType.IncreasedCastSpeed].LocalValue = _baseStats.IncreasedCastSpeed;
        _modifiers[StatModifierType.AreaOfEffect].LocalValue = _baseStats.AreaOfEffect;
        _modifiers[StatModifierType.Multicast].LocalValue = _baseStats.Multicast;

    }
}
