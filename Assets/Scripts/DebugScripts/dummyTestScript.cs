using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyTestScript : MonoBehaviour
{
    public MapController path;
    public Transform[] beacons;
    // Start is called before the first frame update
    void Start()
    {
        path = FindObjectOfType<MapController>();
        beacons = path.GetPathwayBeacons();
    }

    // Update is called once per frame
    void Update()
    {
        if (path.CanPlaceTower(transform))
            GetComponent<SpriteRenderer>().color = Color.white;
        else 
            GetComponent<SpriteRenderer>().color = Color.red;
    }
}
