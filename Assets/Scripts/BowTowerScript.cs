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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
