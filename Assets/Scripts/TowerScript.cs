using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    //Tower Script is specifically for anything that would be considered a Normal Tower and how it would be handled

    //this should handle initialization of the tower, price of the tower, range of the tower and what mask the tower is able to target. Actual combat will be handled in its own script.
    [Header("Tower Related Variables")]
    [SerializeField]
    [Tooltip("Price for tower to be sold at")]
    private int price;
    [SerializeField]
    private string towerName;

    [Header("Targetting Variables")]
    [SerializeField]
    [Tooltip("View radius of tower")]
    private int range;
    [SerializeField]
    [Tooltip("Angle of field of view of tower")]
    [Range(1, 360)] private float angle = 360f;
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private LayerMask obstacleLayer;
    public bool CanSeeEnemy {get; private set; }    // any script can read this value, only this script can set the value
    [SerializeField]
    private Collider2D[] rangeCheck;
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FOV();
        }
    }
    private void FOV() {
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        if (rangeCheck.Length > 0) {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer)) {
                CanSeeEnemy = true;
            } else {
                CanSeeEnemy = false;
            }
        } else if (CanSeeEnemy) {
            CanSeeEnemy = false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);
        
        if (CanSeeEnemy) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, rangeCheck[0].transform.position);
        }
    }

    public int GetPrice()
    {
        return price;
    }
    public string GetName()
    {
        return towerName;
    }


    private void Awake()
    {

    }

    void FixedUpdate()
    {
        
    }



}
