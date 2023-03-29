using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeScript : MonoBehaviour
{
    [SerializeField] GameObject selectedTower;
    [SerializeField] Animator anim;
    [SerializeField] private GameObject upgrade1, upgrade2;

    private TowerStats stats;

    [SerializeField]
    private Sprite[] archerLeftIcons, archerRightIcons, meleeLeftIcons,meleeRightIcons, griefLeftIcons, griefRightIcons;

    public Sprite[][] archerIcons, meleeIcons, griefIcons;
    //private Sprite[][] archerIcons, meleeIcons, griefIcons;

    // open and assemble
    // treat clicks on another tower immediately
    // treat clicks on background as a close statement
    // closes

    private void Awake()
    {
        archerIcons = new Sprite[][] {archerLeftIcons, archerRightIcons};
        meleeIcons = new Sprite[][] {meleeLeftIcons, meleeRightIcons};
        griefIcons = new Sprite[][] {griefLeftIcons, griefRightIcons};            //Icons should be more than sprites. Im thinking an object containing the price of the upgrade, the actual content of the upgrade
                                                                                  //and the description + icon for each individual slot. Then we can do the same thing we're doing here with that object type instead
                                                                                  //of sprites.
    }
    public void OpenUI(GameObject tower) //call by referencing the gameobject that is selected
    {
        //Grab information from the tower
        selectedTower = tower;
        stats = tower.GetComponent<TowerStats>(); 

        SetInformation();

        //play Open animation
        anim.SetBool("Open", true);

        //Keep track of what tower is selected.
    }

    public void CloseUI()
    {
        anim.SetBool("Open", false);  
    }

    private void SetInformation()    //responsible for giving the UI the right information
    {
        switch (stats.TowerType)   //Set Icons, prices, descriptions based on tower type.
        {
            case TowerType.Archer:
                upgrade1.GetComponentInChildren<Image>().sprite = archerLeftIcons[stats.upgradePath.Item1];
                upgrade2.GetComponentInChildren<Image>().sprite = archerRightIcons[stats.upgradePath.Item2];
                break;

            case TowerType.Melee:
                upgrade1.GetComponentInChildren<Image>().sprite = meleeLeftIcons[stats.upgradePath.Item1];
                upgrade2.GetComponentInChildren<Image>().sprite = meleeRightIcons[stats.upgradePath.Item2];
                break;

            case TowerType.Grief:
                upgrade1.GetComponentInChildren<Image>().sprite = griefLeftIcons[stats.upgradePath.Item1];
                upgrade2.GetComponentInChildren<Image>().sprite = griefRightIcons[stats.upgradePath.Item2];
                break;
        }
    }
}
