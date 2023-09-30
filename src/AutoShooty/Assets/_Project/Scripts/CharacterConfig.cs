using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptables/Character Config")]
public class CharacterConfig : ScriptableObject
{
    public PlayerAvatar Avatar;
    public List<WeaponBase> StartingWeapons;
    public List<StatAmount> StartingModifiers;
}
