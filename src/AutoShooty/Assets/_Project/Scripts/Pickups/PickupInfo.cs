
using System;

[Serializable]
public enum PickupType
{
    XP
}

[Serializable]
public class PickupInfo
{
    public PickupType Type;
    public int Amount;
}