using UnityEngine;
using QGame;

[CreateAssetMenu(menuName = "Scriptables/StatRewardOption")]
public class StatRewardOption : ScriptableObject
{
    public StatModifierType Type;
    public float Value;
    public bool IsPercentage;

    private void Awake()
    {
        
    }
}