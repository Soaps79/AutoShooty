using UnityEngine;
using QGame;
using System.Collections.Generic;
using System.Linq;
using System;

using Random = UnityEngine.Random;
using TMPro;

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
    private float _criticalChance;
    [SerializeField]
    private float _criticalMultiplier;

    public CombatantType Type;
    private HashSet<string> _setToAffect;

    private HashSet<string> _timedOutEntities = new HashSet<string>();

    private void Awake()
    {
        _currentHealth = _baseHealth;
        _setToAffect = Type.TypesToAffect.Select(i => i.Code).ToHashSet();
    }

    private void OnTriggerEnter2D(Collider2D collision) => HandleCollision(collision);
    private void OnTriggerStay2D(Collider2D collision) => HandleCollision(collision);

    public Action<Combatant, float, bool> OnDamageTaken;

    private void HandleCollision(Collider2D collider)
    {
        var combatant = collider.GetComponent<Combatant>();

        if (combatant == null 
            || !_setToAffect.Contains(combatant.Type.Code) 
            || _timedOutEntities.Contains(combatant.gameObject.name))
            return;

        ApplyDamage(combatant);

        _timedOutEntities.Add(combatant.gameObject.name);
        StopWatch.AddNode(combatant.gameObject.name, _hitIgnoreTime, true)
            .OnTick += () => { _timedOutEntities.Remove(combatant.gameObject.name); };
    }

    private void ApplyDamage(Combatant other)
    {
        var rand = Random.Range(0f, 1);
        if (rand < _criticalChance)
            other.TakeDamage(_damage + (_criticalMultiplier * _damage), true);
        else
            other.TakeDamage(_damage, false);
    }

    public void TakeDamage(float damage, bool isCritical)
    {
        _currentHealth -= damage;
        OnDamageTaken?.Invoke(this, damage, isCritical);
    }    
}
