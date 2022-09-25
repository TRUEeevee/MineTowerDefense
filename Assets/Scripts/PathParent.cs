using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathParent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Array that holds beacons in order of traversal for the map")]
    private Transform[] PathwayBeacons;
    [SerializeField]
    [Tooltip("Array that holds all vectors that path consists of")]




    public Transform[] GetPathwayBeacons()
    {
        return PathwayBeacons;
    }

    // NOTE: collider radius set to .65 for good effect for checking if tower was on path
    public bool onPath(Transform tower) {
        LayerMask placing = LayerMask.GetMask("Placing");
        // iterate through array, using i and i+1 index for linecasting, which is why i < length - 1 instead of just length
        for (int i = 0; i < PathwayBeacons.Length - 1; i++) {
            if (Physics2D.Linecast(PathwayBeacons[i].transform.position, PathwayBeacons[i+1].transform.position, placing)) {
                return true;
            }
        }
        return false;
    }
}
