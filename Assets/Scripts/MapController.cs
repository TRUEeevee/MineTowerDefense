using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Array that holds beacons in order of traversal for the map")]
    private Transform[] PathwayBeacons;




    public Transform[] GetPathwayBeacons()
    {
        return PathwayBeacons;
    }

    // NOTE: collider radius set to .65 for good effect for checking if tower was on path
    public bool onPath() {
        LayerMask placing = LayerMask.GetMask("Placing");
        // iterate through array, using i and i+1 index for linecasting, which is why i < length - 1 instead of just length
        for (int i = 0; i < PathwayBeacons.Length - 1; i++) {
            if (Physics2D.Linecast(PathwayBeacons[i].transform.position, PathwayBeacons[i+1].transform.position, placing)) {
                return true;
            }
        }
        return false;
    }

    public bool onObstacle(Transform tower) {
        LayerMask obstacle = LayerMask.GetMask("Obstacle", "Tower");
        Vector3 extents = tower.GetComponent<BoxCollider2D>().bounds.extents;
        Vector3 center = tower.GetComponent<BoxCollider2D>().bounds.center;

        if (Physics2D.OverlapArea(center + extents, center - extents, obstacle)) {
            return true;
        }
        return false;
    }

    public bool CanPlaceTower(Transform tower) {
        return !(onPath() || onObstacle(tower));
    }
}
