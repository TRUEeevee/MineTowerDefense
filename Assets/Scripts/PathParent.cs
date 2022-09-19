using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathParent : MonoBehaviour
{
    [SerializeField]
    private Transform[] PathwayBeacons;

    public Transform[] GetPathwayBeacons()
    {
        return PathwayBeacons;
    }
}
