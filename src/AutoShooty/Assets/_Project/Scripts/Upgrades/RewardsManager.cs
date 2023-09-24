using UnityEngine;
using QGame;
using System.Collections.Generic;
using System;

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
        OptionsViewModel.Initialize(Options);
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
        OptionsViewModel.TurnOn();
    }

    private void OnRewardChosen(StatRewardOption option)
    {
        Debug.Log($"reward chosen {option.name}");
    }
}
