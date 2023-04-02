using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Projectile Speed")]
    public float _speed;

    // Position for projectile to be instantiated
    private Vector3 _startPosition;
    [Tooltip("Amount of damage to inflict on enemy on impact")]
    public int _damage;
    public int _pierceNum;

    public Vector3 _target;
    public NewTowerScript _towerScript; // CHANGE THIS WHEN RENAMING NewTowerScript to TowerScript or whatever name it becomes
    private ArcherTowerScript _bowTower;
    private Rigidbody2D _rb;
    private Vector3 _travelAngle;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (!_towerScript.furthestEnemy) {
            Destroy(gameObject);
        } else {
            _target = _towerScript.furthestEnemy.transform.position;
            _startPosition = _towerScript.GetComponentInParent<Transform>().position;

            // gameObject.transform.LookAt(towerScript.furthestEnemy.transform);
            transform.localRotation = Quaternion.Euler(0, 0, transform.rotation.z);

            Destroy(gameObject, 2f);
        }

        _travelAngle = _target - transform.position;
        float rotationAngle = Mathf.Atan2(_travelAngle.y, _travelAngle.x) * Mathf.Rad2Deg - 0f;
        _rb.rotation = rotationAngle;

        float arrowAngle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(arrowAngle, Vector3.forward);
        
    }

    public void setValues(NewTowerScript towerScript, TowerStats stats, float projectileSpeed) {
        _towerScript = towerScript;
        _damage = stats._attackDamage;
        _pierceNum = stats._pierce;
        _speed = projectileSpeed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.MovePosition(transform.position + _travelAngle.normalized * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy")) {
            _pierceNum--;
            if (_pierceNum < 0) {
                Destroy(gameObject);
            }
        }
    }
}
