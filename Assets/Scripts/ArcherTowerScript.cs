using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArcherTowerScript : MonoBehaviour
{
    [SerializeField]
    private TowerScript towerScript;

    [SerializeField]
    private GameObject arrow;
    [Header("Tower Stats")]

    [SerializeField]
    TowerStats _stats;

    [SerializeField]
    [Tooltip("How fast the arrow/projectile moves")]
    private float projectileSpeed;

    private GameObject projectileParent;

    [SerializeField]
    private Animator anim;

    private void Awake()
    {
        projectileParent = GameObject.Find("ProjectileParent");
        GetComponent<TowerScript>().stats = new TowerStats(TowerType.Archer);


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
            if ((LayerMask.GetMask("Tower") & 1 << gameObject.layer) == 1 << gameObject.layer && towerScript.CanSeeEnemy && towerScript.furthestEnemy)
            {
                Attack();
                anim.speed = _stats.attackSpeed * 3;
                GetComponent<Animator>().Play("ArcherAttack");
            }

            yield return wait;
        }
    }

    private void Attack()
    {
        GameObject projectile = Instantiate(arrow, transform.position, Quaternion.identity, projectileParent.transform);
        ArrowScript projectileScript = projectile.GetComponent<ArrowScript>();
        projectileScript.setValues(towerScript, _stats, projectileSpeed);

    }
}

