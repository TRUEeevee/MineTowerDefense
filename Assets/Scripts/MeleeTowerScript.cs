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

    [SerializeField]
    bool singleTarget = true;

    private GameObject lastTowerClicked;

    private void Awake() {
        gm = FindObjectOfType<GameManager>();
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
                if (singleTarget)
                    SingleAttack();
                else
                    MultiAttack();
                // GetComponent<Animator>().Play("ArcherAttack");
            }
                

            yield return wait;
        }
    }

    private void SingleAttack() {
        Attack(1);
    }

    private void MultiAttack() {
        Attack(3);
    }

    private void Attack(int num) {
        // towerScript.furthestEnemy;
        // Physics2D.BoxCast(towerScript.furthestEnemy.transform.position, )
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
