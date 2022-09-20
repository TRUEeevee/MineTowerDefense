using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleTest : MonoBehaviour
{
    [SerializeField]
    LayerMask layer;
    [SerializeField]
    public Collider2D[] returnColliders;
    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.GetMask("Tower");
        print(layer + "" + layer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        returnColliders = Physics2D.OverlapCircleAll(transform.position, 6f, layer);
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 6f);

    }
}
