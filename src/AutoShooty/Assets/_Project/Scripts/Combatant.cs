using UnityEngine;
using QGame;
using System.Collections.Generic;

public class Combatant : QScript, ICombatant
{
    [SerializeField]
    private float _baseHealth;
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _hitIgnoreTime;
    [SerializeField]
    private float _damage;

    private HashSet<string> _timedOutEntities = new HashSet<string>();

    private void Awake()
    {
        _currentHealth = _baseHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var combatant = collision.collider.GetComponent<Combatant>();

        if (combatant == null || _timedOutEntities.Contains(combatant.gameObject.name))
            return;

        combatant.TakeDamage(_damage);
        _timedOutEntities.Add(combatant.gameObject.name);
        StopWatch.AddNode(combatant.gameObject.name, _hitIgnoreTime, true)
            .OnTick += () => { _timedOutEntities.Remove(combatant.gameObject.name); };
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }    
}
