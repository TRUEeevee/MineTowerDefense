using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// each object/instance of this class will be a single upgrade module, a list/array of them will be an upgrade path
public class ArcherUpgradeModule : MonoBehaviour
{
    GameManager gm;
    // once idle mechanics are made, prices wil change to multiple resources rather than currency
    int upgradePrice;
    TowerStats statUpgrades;
    float projectileSpeed;
    [SerializeField]
    Sprite left1, left2, left3, right1, right2, right3, unavailable;
    Sprite[] leftPathImages, rightPathImages;
    [SerializeField]
    GameObject leftButton, rightButton;
    Vector2 upgrade = new Vector2Int(1, 1);
    private void Awake() {
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    private void Start() {
        leftButton = gm.leftButton;
        rightButton = gm.rightButton;

        leftPathImages = new Sprite[] {unavailable, left1, left2, left3};
        rightPathImages = new Sprite[] {unavailable, right1, right2, right3};

    }

    public void updateButtons() {
        leftButton.GetComponent<Image>().sprite = leftPathImages[((int)upgrade.x)];
        rightButton.GetComponent<Image>().sprite = rightPathImages[((int)upgrade.y)];

        leftButton.GetComponent<Button>().interactable = true;
        rightButton.GetComponent<Button>().interactable = true;

        if (upgrade.x == 0 || upgrade.x == 4) {
            leftButton.GetComponent<Button>().interactable = false;
        }
        if (upgrade.y == 0 || upgrade.y == 4) {
            rightButton.GetComponent<Button>().interactable = false;
        }
    }

    public void upgradeLeft() {
        upgrade = upgrade + (upgrade.y == 1 ? new Vector2(1, -1) : new Vector2(1, 0));
        updateButtons();
    }

    public void upgradeRight() {
        upgrade = upgrade + (upgrade.x == 1 ? new Vector2(-1, 1) : new Vector2(0, 1));
        updateButtons();
    }

    

    








}




/*  Towers have 2 upgrade paths, consisting of (3**) upgrades

    Once one of the upgrade paths are selected, the other will be unavailable.

    Various resources are the cost of the upgrades obtained via idle path (until idle mechanics are made, only currency will be used for upgrading)
    In addition to these upgrade paths, enchantment(s) can be put onto the towers via lapis and exp (currency)

    See MineTowerDefense DMs for upgrade paths

    ** number of upgrades may change

    
























*/