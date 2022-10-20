using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade
{
    // once idle mechanics are made, prices wil change to multiple resources rather than currency
    // Dictionary<string, int> upgradePrices = new Dictionary<string, int>();
    int upgradePrice;
    Dictionary<string, int> statUpgrade = new Dictionary<string, int>();
}



/*  Towers have 2 upgrade paths

    Once one of the upgrade paths are selected, the other will be unavailable.

    Various resources are the cost of the upgrades obtained via idle path (until idle mechanics are made, only currency will be used for upgrading)
    In addition to these upgrade paths, enchantment(s) can be put onto the towers via lapis and exp (currency)

    
























*/