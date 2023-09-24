using UnityEngine;
using QGame;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class RewardsOptionsViewModel : QScript
{
    CanvasGroup _canvasGroup;

    public Action<StatRewardOption> OnRewardChosen;

    [SerializeField]
    private ButtonWithText _optionOne;
    [SerializeField] 
    private ButtonWithText _optionTwo;
    [SerializeField] 
    private ButtonWithText _optionThree;

    [SerializeField]
    private float _fadeTime;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(List<StatRewardOption> options)
    {
        if (options.Count != 3)
            throw new UnityException("Rewards UI init'd with not three options");
        
        _optionOne.Text.text = GetButtonText(options[0]);
        _optionOne.Button.onClick.AddListener(() => { OnSelection(options[0]);});
        _optionTwo.Text.text = GetButtonText(options[1]);
        _optionTwo.Button.onClick.AddListener(() => { OnSelection(options[1]); });
        _optionThree.Text.text = GetButtonText(options[2]);
        _optionThree.Button.onClick.AddListener(() => { OnSelection(options[2]); });
    }

    private string GetButtonText(StatRewardOption option)
    {
        switch (option.Type)
        {
            case StatModifierType.Health:
                return $"Increase Health by {option.Value}";
            case StatModifierType.MovementSpeed:
                return $"Increase Movement Speed by {option.Value}";
            case StatModifierType.MoreDamage:
                return $"Increase Damage by {option.Value}";
            case StatModifierType.IncreasedDamage:
                return $"Increase Damage by {option.Value}%";
            case StatModifierType.CritChance:
                return $"Increase Critical Chance by {option.Value}%";
            case StatModifierType.CritMultiplier:
                return $"Increase Critical Multiplier by {option.Value}%";
            case StatModifierType.AreaOfEffect:
                return $"Increase Are of Effect by {option.Value}%";
            case StatModifierType.Multicast:
                return $"Increase Multicast Chance by {option.Value}%";
            default:
                return "";
        }
    }

    private void OnSelection(StatRewardOption option)
    {
        OnRewardChosen?.Invoke(option);
        _canvasGroup.DOFade(0, _fadeTime).onComplete += () => gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1.0f, _fadeTime);
    }
}
