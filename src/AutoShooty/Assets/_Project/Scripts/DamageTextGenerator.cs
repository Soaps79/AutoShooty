using UnityEngine;
using QGame;
using TMPro;
using DG.Tweening;
using Messaging;

public class DamageTextGenerator : QScript, IMessageListener
{
    [SerializeField]
    private TMP_Text _normalPrefab;
    [SerializeField]
    private TMP_Text _criticalPrefab;
    [SerializeField]
    private float _moveTime;
    [SerializeField]
    private float _moveDistance;

    public string Name => throw new System.NotImplementedException();

    private void Start()
    {
        Locator.MessageHub.AddListener(this, EnemyBase.MessageName);
    }

    public void HandleEnemyCreated(Combatant combatant)
    {
        combatant.OnDamageTaken += HandleDamageTaken;
    }

    private void HandleDamageTaken(Combatant combatant, float damage, bool isCritical)
    {
        var sprite = combatant.GetComponent<SpriteRenderer>();
        var startHeight = sprite.bounds.max.y;

        var text = Instantiate(isCritical ? _criticalPrefab : _normalPrefab);
        text.transform.position = new Vector3(combatant.transform.position.x, startHeight, 1);
        text.text = damage.ToString("N0");
        text.transform.DOMoveY(startHeight + _moveDistance, _moveTime)
            .onComplete += () => { Destroy(text.gameObject); };
    }

    public void HandleMessage(string type, MessageArgs args)
    {
        if(type == EnemyBase.MessageName && args is EnemySpawnedMessageArgs a)
        {
            HandleEnemyCreated(a.Enemy.GetComponent<Combatant>()); 
        }
    }
}
