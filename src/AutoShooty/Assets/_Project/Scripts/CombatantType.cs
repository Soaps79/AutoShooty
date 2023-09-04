using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/CombatantType")]
public class CombatantType : ScriptableObject
{
    public string Name;
    public string Code;
    public List<CombatantType> TypesToAffect;
}
