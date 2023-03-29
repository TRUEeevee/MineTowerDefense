using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TowerType{
    Archer,
    Melee,
    Grief
}

// We need to completely overhaul or replace this class me thinks.
public class TowerStats : MonoBehaviour
{
    private int _price;
    private int _range;
    private float _attackSpeed;
    private int _attackDamage;
    private int _pierce;
    private (int, int) _upgradePath; //tuple containing the state of the selected towers upgrades

    public int price { get {return _price; } private set { _price = value; } }
    public int range { get {return _range; } private set { _range = value; } }
    public float attackSpeed { get {return _attackSpeed; } private set { _attackSpeed = value; } }
    public int attackDamage { get {return _attackDamage; } private set { _attackDamage = value; } }
    public int pierce { get {return _pierce; } private set { _pierce = value; } }
    public (int,int) upgradePath { get { return _upgradePath; } private set { _upgradePath = value; } }

    public TowerType TowerType { get; internal set; }

    // extra misc stat? Archer: Projectile Speed -+-+-+- Melee: -+-+-+- Griefer: 

    // default constructor (should never be called)
    public TowerStats(): this(null) {}

    // constructor using tower type
    public TowerStats(TowerType towerType) {          
        switch (towerType) {
            case TowerType.Archer:
                _price = 150;
                _range = 4;
                _attackSpeed = 3;
                _attackDamage = 50;
                _pierce = 1;
                _upgradePath = (0, 0);
                break;
            case TowerType.Melee:
                _price = 100;
                _range = 2;
                _attackSpeed = 4;
                _attackDamage = 55;
                _pierce = 0;
                _upgradePath = (0, 0);
                break;
            case TowerType.Grief:
                _price = 175;
                _range = 4;
                _attackSpeed = 3;
                _attackDamage = 50;
                _pierce = 0;
                _upgradePath = (0, 0);
                break;
            default:
                _price = 0;
                _range = 0;
                _attackSpeed = 0;
                _attackDamage = 0;
                _pierce = 0;
                _upgradePath = (0, 0);
                break;
        }
    }

    // constructor using stats, mainly used for upgrade modules
    public TowerStats(int price, int range, float attackSpeed, int attackDamage, int pierce) {
        _price = price;
        _range = range;
        _attackSpeed = attackSpeed;
        _attackDamage = attackDamage;
        _pierce = pierce;
    }

    // copy constructor
    public TowerStats(TowerStats stats) {
        _price = stats._price;
        _range = stats._range;
        _attackSpeed = stats._attackSpeed;
        _attackDamage = stats._attackDamage;
        _pierce = stats._pierce;
    }

    public static TowerStats operator + (TowerStats curStats, TowerStats upStats) {
        TowerStats newStats = new TowerStats(curStats);
        newStats.updateStats(upStats);
        return newStats;
    }

    // given stats, update tower stats to add stats
    private void updateStats(TowerStats towerStats) {
        _range += towerStats._range;
        _attackSpeed += towerStats._attackSpeed;
        _attackDamage += towerStats._attackDamage;
        _pierce += towerStats._pierce;
    }

    // update tower stats to add specific bonuses
    public void updateStats(int price, int range, float attackSpeed, int attackDamage, int pierce) {
        _range += range;
        _attackSpeed += attackSpeed;
        _attackDamage += attackDamage;
        _pierce += pierce;
    }
}
