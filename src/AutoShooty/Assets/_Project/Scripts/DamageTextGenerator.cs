using UnityEngine;
using QGame;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Combatant))]
public class DamageTextGenerator : QScript
{
    private Combatant _combatant;

    [SerializeField]
    private TMP_Text _normalPrefab;
    [SerializeField]
    private TMP_Text _criticalPrefab;
    [SerializeField]
    private float _moveTime;
    [SerializeField]
    private float _moveDistance;

    private void Awake()
    {
        _combatant = gameObject.GetComponent<Combatant>();
        _combatant.OnDamageTaken += HandleDamageTaken;
    }

    private void HandleDamageTaken(float damage, bool isCritical)
    {
        var text = Instantiate(isCritical ? _criticalPrefab : _normalPrefab);
        text.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        text.text = damage.ToString("N0");
        text.transform.DOMoveY(transform.position.y + _moveDistance, _moveTime)
            .onComplete += () => { Destroy(text.gameObject); };
    }
}
