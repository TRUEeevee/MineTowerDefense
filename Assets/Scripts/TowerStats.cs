using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerStats : MonoBehaviour
{
    // this script will be attached to each towers prefab and be set there.

    //each tower afterwards will have access to both reference and change its own stats, based on its upgrades, buffs, and enchants freely.

    [SerializeField]
    private int _range, _attackDamage, _pierce, _value;
    [SerializeField]
    private float _attackSpeed ;
    [SerializeField]
    private (int, int) _upgradePath; //tuple containing the state of the selected towers upgrades
    [SerializeField]
    private string towerName;

    public void IncreaseRange(int amount)//takes an amount and adds to the towers range
    {
        _range += amount;
    }
    public void IncreaseAttackSpeed(int amount)//takes an amount and adds to the towers attack speed
    {
        _attackSpeed += amount;
    }
    public void IncreaseAttackDamage(int amount)//takes an amount and adds to the towers damage
    {
        _attackDamage += amount;
    }
    public void IncreasePierce(int amount) //takes an amount and adds to the towers pierce
    {
        _pierce += amount;
    }
    public void UpdateUpgradePath((int, int) newPath) //takes a tuple and assigns that as the current upgrade path.
    {
        _upgradePath = newPath;
    }
    public void UpdateValue(int amount) //takes an amount and adds to the towers value
    {
        _value += amount;
    }
}
