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
    private int damage;

    private int x;
    // Start is called before the first frame update
    void Start()
    {
        towerScript = FindObjectOfType<TowerScript>();  // does this just replace .this ?
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {

        WaitForSeconds wait = new WaitForSeconds(0.2f);
        
        while (true)
        {
            yield return wait;
            if(towerScript.CanSeeEnemy)
                Attack();
        }
    }

    private void Attack()
    {

        //instanciate projectile
        GameObject projectile = Instantiate(arrow, transform.position, Quaternion.identity);
        projectile.GetComponent<ArrowScript>().damage = GetDamage();
        //The bullet itself will have a script for its colission and calls a damage function on the enemy.
    }

    private int GetDamage()
    {
        return damage;
    }



}
