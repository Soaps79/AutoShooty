using UnityEngine;
using QGame;
using TMPro;
using DG.Tweening;

public class DamageTextGenerator : QScript
{
    [SerializeField]
    private TMP_Text _normalPrefab;
    [SerializeField]
    private TMP_Text _criticalPrefab;
    [SerializeField]
    private float _moveTime;
    [SerializeField]
    private float _moveDistance;

    public void HandleEnemyCreated(Combatant combatant)
    {
        combatant.OnDamageTaken += HandleDamageTaken;
    }

    private void HandleDamageTaken(Combatant combatant, float damage, bool isCritical)
    {
        var text = Instantiate(isCritical ? _criticalPrefab : _normalPrefab);
        text.transform.position = new Vector3(combatant.transform.position.x, combatant.transform.position.y, 1);
        text.text = damage.ToString("N0");
        text.transform.DOMoveY(combatant.transform.position.y + _moveDistance, _moveTime)
            .onComplete += () => { Destroy(text.gameObject); };
    }
}
