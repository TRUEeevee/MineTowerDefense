using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector3 startPosition;
    public int damage = 50;
    public int pierceNum;

    public Vector3 target;
    private TowerScript towerScript;
    private BowTowerScript bowTower;
    private Rigidbody2D rb;
    private Vector3 angle;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        towerScript = FindObjectOfType<TowerScript>();
        if (!towerScript.furthestEnemy) {
            Destroy(gameObject);
        } else {
            target = towerScript.furthestEnemy.transform.position;
            startPosition = towerScript.GetComponentInParent<Transform>().position;

            gameObject.transform.LookAt(towerScript.furthestEnemy.transform);
            transform.localRotation = Quaternion.Euler(0, 0, transform.rotation.z);

            Destroy(gameObject, 2f);
        }
        
    }

    void Start()
    {
        angle = target - transform.position;
        float rotationAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg - 0f;
        rb.rotation = rotationAngle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + angle.normalized * speed * Time.deltaTime);
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
