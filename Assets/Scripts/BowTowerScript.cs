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

    // Start is called before the first frame update
    void Start()
    {
        towerScript = FindObjectOfType<TowerScript>();  // does this just replace .this ?
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {

        WaitForSeconds wait = new WaitForSeconds(1 / attackSpeed);
        
        while (true)
        {
            // print("layer is" + gameObject.layer + " and get mask is " + LayerMask.GetMask("Tower"));
            if ((LayerMask.GetMask("Tower") & 1 << gameObject.layer) == 1 << gameObject.layer && towerScript.CanSeeEnemy && towerScript.furthestEnemy)
                Attack();

            yield return wait;
        }
    }

    private void Attack()
    {
        // if (towerScript.furthestEnemy) {
             //instanciate projectile
            GameObject projectile = Instantiate(arrow, transform.position, Quaternion.identity);
            projectile.GetComponent<ArrowScript>().damage = attackDamage;
            projectile.GetComponent<ArrowScript>().pierceNum = pierceNum;
            //The bullet itself will have a script for its colission and calls a damage function on the enemy.
        // }
    }




}
