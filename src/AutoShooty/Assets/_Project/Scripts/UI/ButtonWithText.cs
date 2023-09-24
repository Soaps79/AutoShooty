using UnityEngine;
using QGame;
using UnityEngine.UI;
using TMPro;

public class ButtonWithText : QScript
{
    public Button Button { get; private set; }
    public TMP_Text Text { get; private set; }

    private void Awake()
    {
        Button = GetComponent<Button>();
        Text = GetComponentInChildren<TMP_Text>();

        if (Button == null || Text == null)
            throw new UnityException("ButtonWithText is missing button or text child");
    }
}
