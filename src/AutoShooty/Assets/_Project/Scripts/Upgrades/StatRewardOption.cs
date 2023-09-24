using UnityEngine;
using QGame;

[CreateAssetMenu(menuName = "Scriptables/StatRewardOption")]
public class StatRewardOption : ScriptableObject
{
    public StatModifierType Type;
    public int Value;

    private void Awake()
    {
        
    }
}
