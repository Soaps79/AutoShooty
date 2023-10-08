using UnityEngine;
using QGame;
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
    public float MinCastDelay;
}

public abstract class WeaponBase : QScript
{
    public abstract string Id { get; }
    [SerializeField]
    private WeaponStatBase _baseStats;
    protected StatModifierHolder _modifiers;
    private Stat _aoeModifier;

    [SerializeField]
    private ProjectileBase _projectilePrefab;
    
    private float _baseCastDelay;
    private float _minCastDelay;
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

        OnInitialize();
    }

    protected virtual void OnInitialize() { }


    #region projectile spawning
    // Spawn scaled projectile at current weapon position
    protected ProjectileBase GetProjectile()
    {
        var proj = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        proj.InitCombatant();
        SetScale(proj.transform);
        proj.Combatant.Initialize(_modifiers.GetDamageCalc());
        return proj;
    }

    // Spawn scaled projectile at specified position
    protected ProjectileBase GetProjectile(Vector3 position)
    {
        var proj = Instantiate(_projectilePrefab, position, Quaternion.identity);
        proj.InitCombatant();
        SetScale(proj.transform);
        proj.Combatant.Initialize(_modifiers.GetDamageCalc());
        return proj;
    }

    // Spawn scaled projectile at specified position with rotation
    protected ProjectileBase GetProjectile(Vector3 position, Vector3 heading)
    {
        var proj = GetProjectile(position);
        float angle = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;
        proj.transform.eulerAngles = new Vector3(0, 0, angle);
        return proj;
    }

    // Spawn scaled projectile at position facing mouse pointer
    protected ProjectileBase GetProjectileFacingMousePointer(Vector3 position)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 heading = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        heading = new Vector3(heading.x, heading.y, 0.0f).normalized;
        var proj = GetProjectile(position, heading);
        return proj;
    }

    protected void SetScale(Transform transform)
    {
        transform.localScale = transform.localScale + _aoeModifier.CurrentValue * transform.localScale;
    }
    #endregion

    #region fire timing
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
        var increasedCastSpeed = _modifiers[StatModifierType.IncreasedCastSpeed].CurrentValue;
        var result = _baseCastDelay - (_baseCastDelay * increasedCastSpeed) / 2;

        if (result < _minCastDelay)
        {
            Debug.Log($"Cast speed less than allowed for weapon: {Id}");
        }

        _elapsedSinceLastFire = carriedOver;
        _nextFire = result >= _minCastDelay ? result : _minCastDelay;
    }
    #endregion

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
        _minCastDelay = _baseStats.MinCastDelay;
        _modifiers[StatModifierType.MoreDamage].LocalValue = _baseStats.Damage;
        _modifiers[StatModifierType.CritChance].LocalValue = _baseStats.CritChance;
        _modifiers[StatModifierType.CritMultiplier].LocalValue = _baseStats.CritMultiplier;
        _modifiers[StatModifierType.IncreasedCastSpeed].LocalValue = _baseStats.IncreasedCastSpeed;
        _modifiers[StatModifierType.AreaOfEffect].LocalValue = _baseStats.AreaOfEffect;
        _modifiers[StatModifierType.Multicast].LocalValue = _baseStats.Multicast;

    }
}
