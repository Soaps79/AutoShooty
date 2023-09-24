using UnityEngine;
using QGame;
using System.Collections.Generic;

public class WeaponCaddy : QScript
{
    [SerializeField]
    private List<WeaponBase> _startingWeapons;
    [SerializeField]
    private List<WeaponBase> _currentWeapons;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _currentWeapons = new List<WeaponBase>();
        foreach (var prefab in _startingWeapons)
        {
            var weapon = Instantiate(prefab, transform, false);
            RegisterNewWeapon(weapon);
        }
    }

    public void RegisterNewWeapon(WeaponBase weapon)
    {
        weapon.Initialize(GameManager.GlobalStats);
        _currentWeapons.Add(weapon);
    }    
}
