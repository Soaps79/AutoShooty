using UnityEngine;
using QGame;

public class SlashWeapon : WeaponBase
{
    [SerializeField]
    private float distanceFromCenter;

    [SerializeField]
    private SlashProjectile prefab;

    public override string Id => "slash";

    protected override void Fire()
    {
        //var direction = GetDirectionToMouse();

        Vector3 mousePos = Input.mousePosition;
        Vector3 heading = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        heading = new Vector3(heading.x, heading.y, 0.0f).normalized;

        var spawnLoc = transform.position + (heading * distanceFromCenter);

        var proj = Instantiate(prefab, spawnLoc, Quaternion.identity);
        proj.Combatant.Initialize(_modifiers.GetDamageCalc());
        float angle = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;
        proj.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Awake()
    {
        
    }
}
