using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TowerType{
    Archer,
    Sword,
    Grief
}

// Class that defines stats for towers
public class TowerStats
{
    private int _price { get {return _price; } set { _price = value; } }
    private int _range { get {return _range; } set { _range = value; } }
    private float _attackSpeed { get {return _attackSpeed; } set { _attackSpeed = value; } }
    private int _attackDamage { get {return _attackDamage; } set { _attackDamage = value; } }
    private int _pierce { get {return _pierce; } set { _pierce = value; } }

    public TowerStats(TowerType towerType) {
        switch (towerType) {
            case TowerType.Archer:
                _price = 150;
                _range = 4;
                _attackSpeed = 3;
                _attackDamage = 50;
                _pierce = 1;
                break;
            case TowerType.Sword:
                _price = 100;
                _range = 1;
                _attackSpeed = 5;
                _attackDamage = 35;
                _pierce = 0;
                break;
            case TowerType.Grief:
                _price = 175;
                _range = 4;
                _attackSpeed = 3;
                _attackDamage = 50;
                _pierce = 0;
                break;
            default:
                _price = 0;
                _range = 0;
                _attackSpeed = 0;
                _attackDamage = 0;
                _pierce = 0;
                break;
        }
    }

    public TowerStats(int price, int range, float attackSpeed, int attackDamage, int pierce) {
        _price = price;
        _range = range;
        _attackSpeed = attackSpeed;
        _attackDamage = attackDamage;
        _pierce = pierce;
    }

    public void updateStats(TowerStats towerStats) {
        _range += towerStats._range;
        _attackSpeed += towerStats._attackSpeed;
        _attackDamage += towerStats._attackDamage;
        _pierce += towerStats._pierce;
    }





}
