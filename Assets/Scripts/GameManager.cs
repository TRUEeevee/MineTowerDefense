using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script will be managing the overall gameplay loop
 * For now, this will mean the main core tower defense things
 * such as health, currency and wave/round management
 *
 * Eventually will also handle the idle resource collection
 * as well as swapping over to the idle/tower defense views
 * 
 */
public class GameManager : MonoBehaviour
{
    [Header("Public variables to be used throughout the game")]
    [Tooltip("Amount of health player has, game ends when this reaches 0")]
    [Range(0, 1000)] public int health;

    [Tooltip("Amount of currency used to build/upgrade towers. Will (maybe) be replaced by resources in the future")]
    public int money;


    // Start is called before the first frame update
    void Start()
    {
        // money set amount different based on difficulty?
        money = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
