using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    //Tower Script is specifically for anything that would be considered a Normal Tower and how it would be handled

    //this should handle initialization of the tower, price of the tower, range of the tower and what mask the tower is able to target. Actual combat will be handled in its own script.
    
    [SerializeField]
    private int price;
    [SerializeField]
    private int range;
    [SerializeField]
    private string towerName;

    public int Getprice()
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



}
