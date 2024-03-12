using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerPlacer : MonoBehaviour
{

    //Trigger the tower placement method on button press

    // Reference to game manager
    private GameManager gm;
    // Referene to map controller
    private MapController mc;

    [SerializeField]
    [Tooltip("Price to buy tower")]
    // used to determine if button should be 
    private int price;
    // Colors for range indicator when placing tower
    private Color redTransparent = new Color(1.0f, 0.5f, 0.5f, 0.5f);
    private Color whiteTransparent = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    // Reference to button clicked
    [SerializeField]
    private GameObject archerButton, meleeButton, grieferButton, towerHub;
    private GameObject towerButton;

    [SerializeField]
    public TowerType towerType;

    public void GetUpgradeInfo()
    {
        // might retire
    }

    private enum PlaceCode {
        Placed,
        Continue,
        Delete
    }
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>(); //caching access to the game manager to reference things such as current money, difficulty (which should affect prices)
        mc = FindObjectOfType<MapController>();
        /*
        archerButton = GameObject.Find("ArcherTowerButton"); 
        meleeButton = GameObject.Find("MeleeTowerButton");
        grieferButton = GameObject.Find("GrieferTowerButton");
        */
    }

    public void TowerButtonPress(GameObject _towerType)  //called when the towers icon buttons are pressed. Should receive from the button pressed the prefab for tower instanciation.
    {
        switch (_towerType.tag) {
            case "BowTower":
                towerButton = archerButton;
                towerType = TowerType.Archer;
                break;
            case "MeleeTower":
                towerButton = meleeButton;
                towerType = TowerType.Melee;
                break;
            case "GrieferTower":
                towerButton = grieferButton;
                towerType = TowerType.Grief;
                break;
        }
        GameObject towerToPlace = Instantiate(_towerType, Input.mousePosition, Quaternion.identity);
        gm.placing = true;
        // archerButton.GetComponent<Button>().enabled = false;
        // meleeButton.GetComponent<Button>().enabled = false;
        // grieferButton.GetComponent<Button>().enabled = false;

        towerHub.SetActive(false);
        /*
        archerButton.SetActive(false);
        meleeButton.SetActive(false);
        grieferButton.SetActive(false);
        */
        StartCoroutine(FollowMouse(towerToPlace));
    }

    private IEnumerator FollowMouse(GameObject tower) {
        while (true) {
            yield return null;
            switch (FollowAndPlace(tower)) {
                case PlaceCode.Continue:
                    break;
                case PlaceCode.Delete:
                    Destroy(tower);
                    gm.placing = false;
                    towerHub.SetActive(true);
                    /*
                    archerButton.SetActive(true);
                    meleeButton.SetActive(true);
                    grieferButton.SetActive(true);
                    */
                    yield break;
                case PlaceCode.Placed:
                    Transform childCache = tower.transform.GetChild(0);

                    childCache.GetComponent<SpriteRenderer>().color = whiteTransparent;
                    childCache.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    childCache.gameObject.SetActive(false);
                    // gm.SubMoney(tower.GetComponent<TowerScript>().GetPrice()); // HAD TO COMMENT TO FIX MERGE CONFLICT, GETPRICE FUNCTION DOES NOT EXIST
                    tower.layer = LayerMask.NameToLayer("Tower");
                    gm.placing = false;
                    towerHub.SetActive(true);
                    tower.GetComponent<TowerScript>().SetStats(towerType);
                    /*
                    archerButton.SetActive(true);
                    meleeButton.SetActive(true);
                    grieferButton.SetActive(true);
                    */
                    yield break;
            }
            
        }
    }

    private PlaceCode FollowAndPlace(GameObject tower) {
        Vector3 mousePOS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePOS.z = 0;
        tower.transform.position = mousePOS;
        if (Input.GetKeyDown(KeyCode.Escape)) {
            return PlaceCode.Delete;
        }
        if (mc.CanPlaceTower(tower.transform)) {
            tower.transform.GetChild(0).GetComponent<SpriteRenderer>().color = whiteTransparent;
            if (Input.GetMouseButtonDown(0)) {
                
                return PlaceCode.Placed;
            }
            return PlaceCode.Continue;
            
        } else {
            tower.transform.GetChild(0).GetComponent<SpriteRenderer>().color = redTransparent;
            return PlaceCode.Continue;
        }
    }
    // Method to be called when the tower is clicked on. 
    public void Selected()//This method will:
    {
        //* set a trigger for the UpgradeUI to open

        //* pass information to the UI                                                                  //Im thinking we do this by calling a method on the UI script that passes this gameobject as reference
                                                                                                        //this should also allow the UI to call methods and change the tower directly with its upgrades.
        //* 
    }

}
