using UnityEngine;
using QGame;
using System.Collections.Generic;

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

    public List<StatRewardOption> Options;
    public RewardsOptionsViewModel OptionsViewModel;

    private void Awake()
    {
        OptionsViewModel.OnRewardChosen += OnRewardChosen;
        OnNextUpdate += () => OptionsViewModel.Initialize(Options);
    }

    public void OnXpGain(int amount)
    {
        var needed = _xpForNextLevel - _currentXp;
        if (amount >= needed)
        {
            var remaining = amount - needed;
            _accruedXp += needed;
            OnXpGain(remaining);
            LevelUp();
        }
        else
        {
            _currentXp += amount;
            _accruedXp += amount;
        }
    }

    private void LevelUp()
    {
        _accruedLevel++;
        _currentXp = 0;
        Time.timeScale = 0;
        OptionsViewModel.TurnOn();
    }

    private void OnRewardChosen(StatRewardOption option)
    {
        Locator.ModifierDistributor.HandleModifier(
            new StatModifier { ConsumerId = GameManager.GlobalName, Type = option.Type, Amount = option.Value });
        Time.timeScale = 1;
    }
}
