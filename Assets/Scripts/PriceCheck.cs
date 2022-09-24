using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceCheck : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The price required to place the tower")]
    private int towerPrice;

    private GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().interactable = gm.GetMoney() >= towerPrice;
    }
}
