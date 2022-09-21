using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTowerScript : MonoBehaviour
{
    [SerializeField]
    private TowerScript towerScript;
    // Start is called before the first frame update
    void Start()
    {
        towerScript = GetComponentInParent<TowerScript>();
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            Attack();
        }
    }

    private void Attack()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
