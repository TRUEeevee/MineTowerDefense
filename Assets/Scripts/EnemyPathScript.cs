using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class EnemyPathScript : MonoBehaviour
{
    //Handles the very basic moving along the path, what speed, health, and enemy name.
    [Header("Enemy Related Variables")]
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private float speed;
    [SerializeField]
    private string enemyName;

    [Header("Pathing Variables")]
    [Tooltip("Possibly for debugging only")]
    private const float trackLength = 60f;

    [SerializeField]
    private float distanceTraveled = 0;
    private float prevX, prevY, curX, curY;

    [SerializeField]
    private Transform[] pathBeacons;
    private int curBeacon = 0;

    public int GetHealth()
    {
        return currentHealth;
    }

    public string GetName()
    {
        return enemyName;
    }

    public float GetDistance()
    {
        return distanceTraveled;
    }

    private void Awake()
    {
        pathBeacons = FindObjectOfType<MapController>().GetPathwayBeacons();
        curX = transform.position.x;
        prevX = curX;
        curY = transform.position.y;
        prevY = curY;

        currentHealth = maxHealth;
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

        if (currentHealth <= 0)
        {
            Kill();
            //add money
            //proc on death effects

        }

        if (transform.position == pathBeacons[pathBeacons.Length - 1].position)
        {
            Kill();
            //lose a life
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage(collision.gameObject.GetComponent<ArrowScript>().damage);
    }


    public void TakeDamage(int damage)
    {
        //calculated IF the target is invulnerable

        //calculates armor values?

        //deals damage to the objects HP
        currentHealth -= damage;

        //sets animation trigger for take damage

    }
    private void Kill()
    {
        Destroy(gameObject);
    }
}
