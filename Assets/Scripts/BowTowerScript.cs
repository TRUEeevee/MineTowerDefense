using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTowerScript : MonoBehaviour
{
    [SerializeField]
    private TowerScript towerScript;

    [SerializeField]
    private GameObject arrow;

    [SerializeField]
    [Tooltip("Damage of primary attack/projectile")]
    private int attackDamage;

    [SerializeField]
    [Tooltip("How many attacks per second tower can perform")]
    private float attackSpeed;

    [SerializeField]
    [Tooltip("How many enemies the projectile goes through")]
    private int pierceNum;

    [SerializeField]
    [Tooltip("How fast the arrow/projectile moves")]
    private int projectileSpeed;

    private GameObject projectileParent;

    private void Awake() {
        projectileParent = GameObject.Find("ProjectileParent");
    }
    void Start()
    {
        towerScript = GetComponent<TowerScript>();
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {

        WaitForSeconds wait = new WaitForSeconds(1 / attackSpeed);
        
        while (true)
        {
            if ((LayerMask.GetMask("Tower") & 1 << gameObject.layer) == 1 << gameObject.layer && towerScript.CanSeeEnemy && towerScript.furthestEnemy)
                Attack();

            yield return wait;
        }
    }

    private void Attack()
    {
        GameObject projectile = Instantiate(arrow, transform.position, Quaternion.identity, projectileParent.transform);
        ArrowScript projectileScript = projectile.GetComponent<ArrowScript>();
        projectileScript.towerScript = towerScript;
        projectileScript.damage = attackDamage;
        projectileScript.pierceNum = pierceNum;
        projectileScript.speed = projectileSpeed;

        // projectile.GetComponent<ArrowScript>().towerScript = towerScript;
        // projectile.GetComponent<ArrowScript>().damage = attackDamage;
        // projectile.GetComponent<ArrowScript>().pierceNum = pierceNum;
        // projectile.GetComponent<ArrowScript>().speed = projectileSpeed;
    }

    void Update() {
        // print(towerScript.GetPrice());
    }



}
