using UnityEngine;
using QGame;
using System;

public class PickupsSpawner : QScript
{
    [SerializeField]
    private Pickup _xpPrefab;

    private void Awake()
    {
        
    }

    public void Register(PickupDropper dropper)
    {
        dropper.PickupDropRequested += OnPickupDropRequest;
    }

    private void OnPickupDropRequest(PickupDropper dropper)
    {
        if (dropper.PickupInfo.Type == PickupType.XP)
        {
            var pickup = Instantiate(_xpPrefab, dropper.Location, Quaternion.identity);
            pickup.Value = dropper.PickupInfo.Amount;
        }
    }
}
