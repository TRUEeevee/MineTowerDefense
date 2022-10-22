using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// each object/instance of this class will be a single upgrade module, a list/array of them will be an upgrade path
public class ArcherUpgradeModule : MonoBehaviour
{
    // once idle mechanics are made, prices wil change to multiple resources rather than currency
    // Dictionary<string, int> upgradePrices = new Dictionary<string, int>(); or its own class?
    int upgradePrice;
    TowerStats statUpgrades;
    float projectileSpeed;
}




/*  Towers have 2 upgrade paths

    Once one of the upgrade paths are selected, the other will be unavailable.

    Various resources are the cost of the upgrades obtained via idle path (until idle mechanics are made, only currency will be used for upgrading)
    In addition to these upgrade paths, enchantment(s) can be put onto the towers via lapis and exp (currency)

    See MineTowerDefense DMs for upgrade paths

    
























*/