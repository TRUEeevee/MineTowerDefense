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
    private string enemyname;


    [SerializeField]
    private Transform[] pathBeacons;
    private int cur = 0;

    public int GetHealth()
    {
        return health;
    }

    public string GetName()
    {
        return enemyname;
    }

    private void Awake()
    {
        FindPath();
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
        if (transform.position != pathBeacons[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, pathBeacons[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else cur = (cur + 1) % pathBeacons.Length;

        Vector2 dir = pathBeacons[cur].position - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Kill();
    }
    private void Kill()
    {
        Debug.Log("An Enemy has gotten past the defense");
        Destroy(gameObject);
    }
}
