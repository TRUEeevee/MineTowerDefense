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
    // bool singleTarget = true;

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
                Attack(_stats.pierce);
                // GetComponent<Animator>().Play("ArcherAttack");
            }
                

            yield return wait;
        }
    }

    // private void SingleAttack() {
    //     Attack(_stats.pierce);
    // }

    // private void MultiAttack() {
    //     Attack(_stats.pierce);
    // }

    private void Attack(int num) {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Enemy"));
        Physics2D.CircleCast(transform.position, _stats.range, Vector2.zero, filter, hits, 0);
        Vector2 furthestVector = (towerScript.furthestEnemy.transform.position - transform.position).normalized;
        hits.Sort((b, a) => a.collider.gameObject.GetComponent<EnemyScript>().GetDistance().CompareTo(b.collider.gameObject.GetComponent<EnemyScript>().GetDistance()));
        print(hits.Count);
        foreach (RaycastHit2D hit in hits) {
            if (Vector2.Dot(furthestVector, (hit.collider.transform.position - transform.position).normalized) > 0.5f) {
                hit.collider.gameObject.GetComponent<EnemyScript>().TakeDamage(_stats.attackDamage);
                num--;
                if (num < 0)
                    break;

            }
        }
    }

    private void OnDrawGizmos() {
        
        if (towerScript.furthestEnemy) {
            Vector2 enemyVector = (towerScript.furthestEnemy.transform.position - transform.position).normalized;
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position, enemyVector * _stats.range);
            Gizmos.color = Color.green;
            Vector2 A = Quaternion.AngleAxis(-60, Vector3.forward) * enemyVector;
            Vector2 B = Quaternion.AngleAxis(60, Vector3.forward) * enemyVector;
            Gizmos.DrawRay(transform.position, A * _stats.range);
            Gizmos.DrawRay(transform.position, B * _stats.range);
        }
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
                    // get melee upgrade script
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
