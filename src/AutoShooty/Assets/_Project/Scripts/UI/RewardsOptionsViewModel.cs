using UnityEngine;
using QGame;
using System;
using System.Collections.Generic;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class RewardsOptionsViewModel : QScript
{
    CanvasGroup _canvasGroup;

    public Action<StatRewardOption> OnRewardChosen;

    [SerializeField]
    private RewardButtonViewModel _optionOne;
    [SerializeField] 
    private RewardButtonViewModel _optionTwo;
    [SerializeField] 
    private RewardButtonViewModel _optionThree;

    public Color CommonColor;
    public Color UncommonColor;
    public Color RareColor;
    public Color LegendaryColor;

    [SerializeField]
    private float _fadeTime;

    public bool IsChoosing;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _optionOne.OnChosen += OnSelection;
        _optionTwo.OnChosen += OnSelection;
        _optionThree.OnChosen += OnSelection;
    }

    public void StartChoices(List<StatRewardOption> options)
    {
        if (options.Count != 3)
            throw new UnityException("Rewards UI init'd with not three options");

        IsChoosing = true;

        _optionOne.Setup(options[0], GetButtonText(options[0]), GetRarityColor(options[0].Rarity));
        _optionTwo.Setup(options[1], GetButtonText(options[1]), GetRarityColor(options[1].Rarity));
        _optionThree.Setup(options[2], GetButtonText(options[2]), GetRarityColor(options[2].Rarity));

        TurnOn();
    }

    private Color GetRarityColor(RewardRarity rarity)
    {
        switch (rarity)
        {
            case RewardRarity.Common:
                return CommonColor;
            case RewardRarity.Uncommon:
                return UncommonColor;
            case RewardRarity.Rare:
                return RareColor;
            case RewardRarity.Legendary:
                return LegendaryColor;
            default:
                return Color.white;
        }
    }

    private string GetButtonText(StatRewardOption option)
    {
        switch (option.Type)
        {
            case StatModifierType.Health:
                return $"Increase Health by {GetDisplayValue(option)}";
            case StatModifierType.MovementSpeed:
                return $"Increase Movement Speed by {GetDisplayValue(option)}";
            case StatModifierType.MoreDamage:
                return $"Increase Damage by {GetDisplayValue(option)}";
            case StatModifierType.IncreasedDamage:
                return $"Increase Damage by {GetDisplayValue(option)}%";
            case StatModifierType.CritChance:
                return $"Increase Critical Chance by {GetDisplayValue(option)}%";
            case StatModifierType.CritMultiplier:
                return $"Increase Critical Multiplier by {GetDisplayValue(option)}%";
            case StatModifierType.AreaOfEffect:
                return $"Increase Area of Effect by {GetDisplayValue(option)}%";
            case StatModifierType.Multicast:
                return $"Increase Multicast Chance by {GetDisplayValue(option)}%";
            case StatModifierType.IncreasedCastSpeed:
                return $"Increase Cast Frequency by {GetDisplayValue(option)}%";
            default:
                return "UNKNOWN TYPE";
        }
    }

    private float GetDisplayValue(StatRewardOption option)
    {
        return option.IsPercentage ? option.Amount * 100 : option.Amount;
    }

    private void OnSelection(StatRewardOption option)
    {
        OnRewardChosen?.Invoke(option);
        IsChoosing = false;
    }

    public void TurnOff()
    {
        _canvasGroup.DOFade(0, _fadeTime).SetUpdate(true).onComplete += () =>
        {
            gameObject.SetActive(false);
        };
    }

    private void TurnOn()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1.0f, _fadeTime).SetUpdate(true);
    }
}
