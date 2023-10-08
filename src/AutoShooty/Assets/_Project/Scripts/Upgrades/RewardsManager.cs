using UnityEngine;
using QGame;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using System.Linq;

[Serializable]
public enum RewardRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}

[Serializable]
public class StatRewardOption
{
    public StatModifierType Type;
    public RewardRarity Rarity;
    public float Amount;
    public string TargetId;
    public bool IsPercentage;
}

public class RewardsManager : QScript
{
    [SerializeField]
    private int _currentXp;
    [SerializeField]
    private int _xpForNextLevel;
    [SerializeField]
    private int _accruedXp;
    [SerializeField]
    private int _accruedLevel;

    [SerializeField]
    private StatModifierType _alwaysOfferType;
    [SerializeField]
    private bool _alwaysOfferEnabled;

    public List<StatRewardTemplate> AllTemplates;

    public RewardsOptionsViewModel OptionsViewModel;

    [SerializeField]
    private int _queuedLevelUps;

    private void Awake()
    {
        OptionsViewModel.OnRewardChosen += OnRewardChosen;
    }

    public void OnXpGain(int amount)
    {
        var needed = _xpForNextLevel - _currentXp;
        if (amount >= needed)
        {
            var remaining = amount - needed;
            _accruedXp += needed;
            if(amount > needed)
                OnXpGain(remaining);
            LevelUp();
        }
        else
        {
            _currentXp += amount;
            _accruedXp += amount;
        }
    }

    public void LevelUp()
    {
        if (!OptionsViewModel.IsChoosing)
        {
            _accruedLevel++;
            _currentXp = 0;
            Time.timeScale = 0;
            OptionsViewModel.StartChoices(ChooseOptions());
        }
        else
        {
            _queuedLevelUps++;
        }
    }

    private List<StatRewardOption> ChooseOptions()
    {
        var usedTypes = new HashSet<int>();
        var result = new List<StatRewardOption>();

        for (int i = 0; i < 3; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, AllTemplates.Count);
            }
            while (usedTypes.Contains(index));

            usedTypes.Add(index);
            var choice = ChooseRarity(AllTemplates[index]);
            result.Add(choice);
        }

        if(_alwaysOfferEnabled)
        {
            var template = AllTemplates.FirstOrDefault(i => i.ModifierType == _alwaysOfferType);
            if (template == null)
                throw new UnityException($"RewardsManager requested to always offer unknown type {_alwaysOfferType}");
            result[2] = ChooseRarity(template);
        }

        return result;                
    }

    private StatRewardOption ChooseRarity(StatRewardTemplate template)
    {
        var result = new StatRewardOption { 
            Type = template.ModifierType, 
            TargetId = GameManager.GlobalName,
            IsPercentage = template.IsPercentage};

        var roll = Random.value;
        switch (roll)
        {
            case > .95f:
                result.Amount = template.LegendaryAmount;
                result.Rarity = RewardRarity.Legendary;
                break;
            case > .80f:
                result.Amount = template.RareAmount;
                result.Rarity = RewardRarity.Rare;
                break;
            case > .60f:
                result.Amount = template.UncommonAmount;
                result.Rarity = RewardRarity.Uncommon;
                break;
            default:
                result.Amount = template.CommonAmount;
                result.Rarity = RewardRarity.Common;
                break;
        }
        return result;
    }

    private void OnRewardChosen(StatRewardOption option)
    {
        Locator.ModifierDistributor.HandleModifier(
            new StatModifier { ConsumerId = option.TargetId, Type = option.Type, Amount = option.Amount });

        if(_queuedLevelUps > 0)
        {
            _queuedLevelUps--;
            LevelUp();
        }
        else
        {
            OptionsViewModel.TurnOff();
            Time.timeScale = 1;
        }
    }
}
