using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType{
    None,
    Archer,
    Melee,
    Grief
}
public class NewTowerScript : MonoBehaviour
{
    public TowerStats towerStats;
    public TowerType towerType;
    


    [SerializeField]
    public GameObject furthestEnemy;
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private LayerMask obstacleLayer;
    [SerializeField]
    public bool CanSeeEnemy;
    [SerializeField]
    private Collider2D[] rangeCheck;

    // Archer specific variables
    // this is a reference to an empty game object in the hierarchy to store all projectiles generated to not clutter it up
    private GameObject projectileParent;
    
    // arrow prefab for archer tower
    [SerializeField]
    private GameObject arrow;

    // projectile speed of arrow
    [SerializeField]
    private float arrowSpeed;


    /*************************************************

    implement a switch to determine type of tower and what
    functions to call on ex: attack function

    scan through ArcherTowerScript, MeleeTowerScript, and 
    GrieferTowerScript and selectively copy neccesarry code
    snippets to combine everything into NewTowerScript

    ****************************************************/




    // Start is called before the first frame update
    void Start()
    {
        // saves enemy layer and starts coroutine to search for enemies
        enemyLayer = LayerMask.GetMask("Enemy");
        StartCoroutine(FOVCheck());

        // even if tower is not archer, set projectile parent
        projectileParent = GameObject.Find("ProjectileParent");
    }

    // This function will be called by the tower placer in order to set the type of the tower that is placed and will assign the stats accordingly
    // Until this function is called, the tower will not be functional
    public void SetStats(TowerType type) {
        towerType = type;
        // initially set to 1, 1 to show available upgrades, once upgraded will be either 2, 0 or 0, 2
        towerStats._upgradePath = (1, 1);
        switch (type) {
            case TowerType.Archer:
                towerStats.towerName = "Archer";
                towerStats._range = 4;
                towerStats._attackDamage = 50;
                towerStats._attackSpeed = 3;
                towerStats._pierce = 1;
                break;
            case TowerType.Melee:
                towerStats.towerName = "Melee";
                towerStats._range = 2;
                towerStats._attackDamage = 55;
                towerStats._attackSpeed = 4;
                towerStats._pierce = 0;
                break;
            case TowerType.Grief:
                towerStats.towerName = "Grief";
                towerStats._range = 4;
                towerStats._attackDamage = 50;
                towerStats._attackSpeed = 3;
                towerStats._pierce = 0;
                break;
        }
    }

    // Coroutine to search for enemies, timeout of .2 seconds to allow for code to run
    private IEnumerator FOVCheck()
    {
        // None is what the enumerator is set to by default, the coroutine will run only once SetStats() has been called by tower placing script
        if (towerType != TowerType.None){
            WaitForSeconds wait = new WaitForSeconds(0.2f);
            while (true)
            {
                yield return wait;
                FOV();
            }
        }
    }

    // Function coroutine uses to check for enemy within its range
    private void FOV() {
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, towerStats._range, enemyLayer);
        int furthestTargetIndex = 0;
        if (rangeCheck.Length > 0) {
            // assign target to enemy furthest along the path
            for (int i = 0; i < rangeCheck.Length; i++) {
                // nested ifs to determine which enemy out of all visible enemies is the furthest away
                if (enemyVisible(rangeCheck[i].gameObject.transform)) 
                {
                    if (rangeCheck[i].GetComponentInParent<EnemyScript>().GetDistance() > rangeCheck[furthestTargetIndex].GetComponentInParent<EnemyScript>().GetDistance())
                    {
                        furthestTargetIndex = i;
                    }
                }
            }
            // store furthest enemy found, perform calculations to rotate tower to face enemy
            furthestEnemy = rangeCheck[furthestTargetIndex].gameObject;
            Transform target = rangeCheck[furthestTargetIndex].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // if enemy is not behind an obstacle, set attack condition to true
            if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer)) {
                CanSeeEnemy = true;
            } else {
                CanSeeEnemy = false;
            }
        // if there are no enemies in range, set attack condition to false
        } else if (CanSeeEnemy) {
            CanSeeEnemy = false;
            furthestEnemy = null;
        }
    }

    private bool enemyVisible(Transform potentialTarget)    // *******I need this to store the position of its target when it fires relative to itself. The tower needs to be able to face one 
    {                                                       //        way or the other based on which way its firing.  ***********************************************************************
        Vector2 directionToTarget = (potentialTarget.position - transform.position).normalized;
        float distanceToTarget = Vector2.Distance(transform.position, potentialTarget.position);
        if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer)) {
            return true;
        } else {
            return false;
        }
    }

    private IEnumerator AttackRoutine()
    {

        WaitForSeconds wait = new WaitForSeconds(1 / towerStats._attackSpeed);

        while (true)
        {
            if ((LayerMask.GetMask("Tower") & 1 << gameObject.layer) == 1 << gameObject.layer && CanSeeEnemy && furthestEnemy)
            {
                Attack();
                // play animation
                // anim.speed = _stats.attackSpeed * 3;
                // GetComponent<Animator>().Play("ArcherAttack");
            }

            yield return wait;
        }
    }

    private void Attack() {
        switch (towerType) {
            case TowerType.Melee:
                List<RaycastHit2D> hits = new List<RaycastHit2D>();
                ContactFilter2D filter = new ContactFilter2D();
                filter.SetLayerMask(LayerMask.GetMask("Enemy"));
                Physics2D.CircleCast(transform.position, towerStats._range, Vector2.zero, filter, hits, 0);
                Vector2 furthestVector = (furthestEnemy.transform.position - transform.position).normalized;
                hits.Sort((b, a) => a.collider.gameObject.GetComponent<EnemyScript>().GetDistance().CompareTo(b.collider.gameObject.GetComponent<EnemyScript>().GetDistance()));
                //print(hits.Count);
                foreach (RaycastHit2D hit in hits) {
                    if (Vector2.Dot(furthestVector, (hit.collider.transform.position - transform.position).normalized) > 0.5f) {
                        hit.collider.gameObject.GetComponent<EnemyScript>().TakeDamage(towerStats._attackDamage);
                        towerStats._pierce--;
                        if (towerStats._pierce < 0)
                            break;

                    }
                }
                break;
            case TowerType.Archer:
                GameObject projectile = Instantiate(arrow, transform.position, Quaternion.identity, projectileParent.transform);
                ArrowScript projectileScript = projectile.GetComponent<ArrowScript>();
                projectileScript.setValues(towerScript, towerStats, arrowSpeed);
                break;
            case TowerType.Grief:
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
