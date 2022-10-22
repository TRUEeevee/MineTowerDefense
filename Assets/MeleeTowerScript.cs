using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTowerScript : MonoBehaviour
{
    [SerializeField]
    private TowerScript towerScript;

    [SerializeField]
    private GameManager gm;

    [SerializeField]
    private GameObject arrow;
    [Header("Tower Stats")]

    [SerializeField]
    TowerStats _stats;

    // [SerializeField]
    // [Tooltip("Damage of primary attack/projectile")]
    // private int attackDamage;

    // [SerializeField]
    // [Tooltip("How many attacks per second tower can perform")]
    // private float attackSpeed;

    // [SerializeField]
    // [Tooltip("How many enemies the projectile goes through")]
    // private int pierceNum;
    // [SerializeField]
    // [Tooltip("Detection range of tower")]
    // private int towerRange;

    [SerializeField]
    [Tooltip("How fast the arrow/projectile moves")]
    private float projectileSpeed;

    private GameObject lastTowerClicked;

    private GameObject projectileParent;

    private void Awake() {
        gm = FindObjectOfType<GameManager>();
        // projectileParent = GameObject.Find("ProjectileParent");
        GetComponent<TowerScript>().stats = new TowerStats(TowerType.Melee);
        _stats = GetComponent<TowerScript>().stats;
    }
    void Start()
    {
        towerScript = GetComponent<TowerScript>();
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {

        WaitForSeconds wait = new WaitForSeconds(1 / _stats.attackSpeed);
        
        while (true)
        {
            if ((LayerMask.GetMask("Tower") & 1 << gameObject.layer) == 1 << gameObject.layer && towerScript.CanSeeEnemy && towerScript.furthestEnemy) {
                Attack();
                // GetComponent<Animator>().Play("ArcherAttack");
            }
                

            yield return wait;
        }
    }

    private void Attack()
    {
        // GameObject projectile = Instantiate(arrow, transform.position, Quaternion.identity, projectileParent.transform);
        // ArrowScript projectileScript = projectile.GetComponent<ArrowScript>();
        // projectileScript.setValues(towerScript, _stats, projectileSpeed);

    }

    void Update() {
        
    }

    private void OnMouseDown() {
        if (!(gm.currentState == GameState.Paused)) {
            if ((LayerMask.GetMask("Tower") & 1 << gameObject.layer) == 1 << gameObject.layer) {
                GameObject circle = transform.GetChild(0).gameObject;
                if (circle.activeSelf) {
                    gm.unclickTower();
                } else {
                    circle.SetActive(true);
                    Invoke("clicked", 0.1f);
                }   
            }
        }
    }

    private void clicked() {
        gm.lastClickedTower = this.gameObject;
    }

}
