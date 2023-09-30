using UnityEngine;
using QGame;
using System.Collections.Generic;
using System;

public class StatModifierDistributor
{
    private readonly Dictionary<string, StatModifierHolder> _subscribers 
        = new Dictionary<string, StatModifierHolder>();

    public void Register(StatModifierHolder holder)
    {
        if (!_subscribers.ContainsKey(holder.Name))
        {
            _subscribers.Add(holder.Name, holder);
            return;
        }

        Debug.Log($"Registering already known modifiers subscriber: {holder.Name}");
        _subscribers[holder.Name] = holder;
    }

    public void HandleModifier(StatModifier modifier)
    {
        HandleModifier(modifier.Type, modifier.ConsumerId, modifier.Amount);
    }

    public void HandleModifier(StatModifierType type, string id, float amount)
    {
        try
        {
            if(_subscribers[id][type] != null)
            {
                _subscribers[id][type].LocalValue += amount;
                _subscribers[id].AlertUpdate();
            }
        }
        catch
        {
            Debug.Log($"Modifier requested for missing stat {type} on {id}");
        }
    }
}
