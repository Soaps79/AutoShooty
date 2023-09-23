using UnityEngine;
using QGame;
using System;

public class PickupDropper : QScript
{
    public PickupInfo PickupInfo;
    public Vector3 Location => transform.position;
    public Action<PickupDropper> PickupDropRequested { get; set; }

    private void Awake()
    {
        
    }

    public void TriggerDropRequest()
    {
        PickupDropRequested?.Invoke(this);
    }
}
