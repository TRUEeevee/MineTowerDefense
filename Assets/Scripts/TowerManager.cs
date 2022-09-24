using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    //Trigger the tower placement method on button press
    private GameManager gm;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>(); //caching access to the game manager to reference things such as current money, difficulty (which should affect prices)

    }

    public void TowerButtonPress(GameObject towerType)  //called when the towers icon buttons are pressed. Should receive from the button pressed the prefab for tower instanciation.
    {
        return;
    }

    
}
