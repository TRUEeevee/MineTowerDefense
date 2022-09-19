using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyToSpawn;
    [SerializeField]
    private Transform spawnLocation;

    //this script is COMPLETELY debug, you can use it for anything including tests to make sure your code is performing how you want it!
   public void SpawnTheEnemy()
    {
        Instantiate(enemyToSpawn, spawnLocation);
    }
}
