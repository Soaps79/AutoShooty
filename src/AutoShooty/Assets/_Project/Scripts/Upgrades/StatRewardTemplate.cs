using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/StatRewardOption")]
public class StatRewardTemplate : RewardTemplate
{
    public StatModifierType ModifierType;
    public bool IsPercentage;

    public float CommonAmount;
    public float UncommonAmount;
    public float RareAmount;
    public float LegendaryAmount;

    public override RewardType RewardType => RewardType.Stat;
}
