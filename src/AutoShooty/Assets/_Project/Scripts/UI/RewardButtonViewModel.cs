using System;
using UnityEngine;
using UnityEngine.UI;

public class RewardButtonViewModel : ButtonWithText
{
    [SerializeField]
    private StatRewardOption _reward;
    public Action<StatRewardOption> OnChosen;

    public void Setup(StatRewardOption option, string buttonText, Color textColor)
    {
        _reward = option;
        Text.text = buttonText;
        Text.color = textColor;
    }

    private void Start()
    {
        Button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        OnChosen?.Invoke(_reward);
    }
}
