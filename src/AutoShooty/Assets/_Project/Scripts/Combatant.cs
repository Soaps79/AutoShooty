using UnityEngine;
using QGame;
using System.Collections.Generic;
using System.Linq;
using System;

using Random = UnityEngine.Random;

public class Combatant : QScript
{
    [SerializeField]
    private float _baseHealth;
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _hitIgnoreTime;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private bool _isInvincible;

    [SerializeField]
    private float _criticalChance;
    [SerializeField]
    private float _criticalMultiplier;

    public CombatantType Type;
    private HashSet<string> _setToAffect;

    private HashSet<string> _timedOutEntities = new HashSet<string>();

    public Action<Combatant, float, bool> OnDamageTaken;
    public Action<Combatant, Combatant> OnDeath;

    private void Awake()
    {
        _currentHealth = _baseHealth;
        _setToAffect = Type.TypesToAffect.Select(i => i.Code).ToHashSet();
    }

    private void OnTriggerEnter2D(Collider2D collision) => HandleCollision(collision);
    private void OnTriggerStay2D(Collider2D collision) => HandleCollision(collision);

    private void HandleCollision(Collider2D collider)
    {
        var combatant = collider.GetComponent<Combatant>();

        if (combatant == null 
            || !_setToAffect.Contains(combatant.Type.Code) 
            || _timedOutEntities.Contains(combatant.gameObject.name))
            return;

        ApplyDamage(combatant);

        var name = combatant.gameObject.name;
        _timedOutEntities.Add(name);
        StopWatch.AddNode(name, _hitIgnoreTime, true)
            .OnTick += () => { _timedOutEntities.Remove(name); };
    }

    private void ApplyDamage(Combatant other)
    {
        var rand = Random.Range(0f, 1);
        if (rand < _criticalChance)
            other.TakeDamage(_damage + (_criticalMultiplier * _damage), true, this);
        else
            other.TakeDamage(_damage, false, this);
    }

    public void TakeDamage(float damage, bool isCritical, Combatant other)
    {
        _currentHealth -= damage;
        OnDamageTaken?.Invoke(this, damage, isCritical);

        if(!_isInvincible && _currentHealth <= 0f)
        {
            OnDeath?.Invoke(this, other);
            Destroy(gameObject);
        }
    }    
}
