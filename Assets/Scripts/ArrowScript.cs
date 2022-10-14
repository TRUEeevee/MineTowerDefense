using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Projectile Speed")]
    public float speed;

    // Position for projectile to be instantiated
    private Vector3 startPosition;
    [Tooltip("Amount of damage to inflict on enemy on impact")]
    public int damage;
    public int pierceNum;

    public Vector3 target;
    public TowerScript towerScript;
    private ArcherTowerScript bowTower;
    private Rigidbody2D rb;
    private Vector3 travelAngle;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!towerScript.furthestEnemy) {
            Destroy(gameObject);
        } else {
            target = towerScript.furthestEnemy.transform.position;
            startPosition = towerScript.GetComponentInParent<Transform>().position;

            // gameObject.transform.LookAt(towerScript.furthestEnemy.transform);
            transform.localRotation = Quaternion.Euler(0, 0, transform.rotation.z);

            Destroy(gameObject, 2f);
        }

        travelAngle = target - transform.position;
        float rotationAngle = Mathf.Atan2(travelAngle.y, travelAngle.x) * Mathf.Rad2Deg - 0f;
        rb.rotation = rotationAngle;

        float arrowAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(arrowAngle, Vector3.forward);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + travelAngle.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy")) {
            pierceNum--;
            if (pierceNum < 0) {
                Destroy(gameObject);
            }
        }
    }
}
