using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType{
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
    }

    // Coroutine to search for enemies, timeout of .2 seconds to allow for code to run
    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    // Function coroutine uses to check for enemy within its range
    private void FOV() {
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, towerStats.range, enemyLayer);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
