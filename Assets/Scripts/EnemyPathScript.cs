using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathScript : MonoBehaviour
{
    //Handles the very basic moving along the path, what speed, health, and enemy name.
    [SerializeField]
    private int health;
    [SerializeField]
    private float speed;
    [SerializeField]
    private string enemyName;
    
    [SerializeField]
    private const float trackLength = 60f;
    private float distanceTraveled = 0;
    private float prevX, prevY, curX, curY;

    [SerializeField]
    private Transform[] pathBeacons;
    private int curBeacon = 0;

    public int GetHealth()
    {
        return health;
    }

    public string GetName()
    {
        return enemyName;
    }

    private void Awake()
    {
        FindPath();
        curX = transform.position.x;
        prevX = curX;
        curY = transform.position.y;
        prevY = curY;
    }

    private void FindPath()
    {
        int x = 0;
        foreach (Transform item in FindObjectOfType<PathParent>().GetPathwayBeacons())
        {
            pathBeacons[x] = item;
            x++;
        }
    }

    void FixedUpdate()
    {
        // calculate distance traveled along path for targeting first
        prevX = curX;
        prevY = curY;
        curX = transform.position.x;
        curY = transform.position.y;
        distanceTraveled += Mathf.Abs(curX - prevX);
        distanceTraveled += Mathf.Abs(curY - prevY);

        if (transform.position != pathBeacons[curBeacon].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, pathBeacons[curBeacon].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else 
        { 
            curBeacon = (curBeacon + 1) % pathBeacons.Length;
            
        }

        Vector2 dir = pathBeacons[curBeacon].position - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Kill();
    }
    private void Kill()
    {
        print(distanceTraveled);
        Debug.Log("An Enemy has gotten past the defense");
        Destroy(gameObject);
    }
}
