using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyTestScript : MonoBehaviour
{
    public PathParent path;
    public Transform[] beacons;
    // Start is called before the first frame update
    void Start()
    {
        path = FindObjectOfType<PathParent>();
        beacons = path.GetPathwayBeacons();
    }

    // Update is called once per frame
    void Update()
    {
        if (path.onPath(transform))
            GetComponent<SpriteRenderer>().color = Color.red;
        else 
            GetComponent<SpriteRenderer>().color = Color.white;
    }
}
