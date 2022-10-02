using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script will be managing the overall gameplay loop
 * For now, this will mean the main core tower defense things
 * such as health, currency and wave/round management
 *
 * Eventually will also handle the idle resource collection
 * as well as swapping over to the idle/tower defense views
 * 
 */

public enum GameState {
    BetweenRounds,
    Playing,
    Paused,
    Idle,
    FastForward
}

public class GameManager : MonoBehaviour
{
    private float fixedDeltaTime;

    [Header("Public variables to be used throughout the game")]
    [Tooltip("Amount of health player has, game ends when this reaches 0")]
    [Range(0, 1000)] public int health;

    [SerializeField]
    [Tooltip("Amount of currency used to build/upgrade towers. Will (maybe) be replaced by resources in the future")]
    private int startMoney;
    [SerializeField]
    private int curMoney;

    [SerializeField]
    private GameState prevState;

    [SerializeField]
    [Tooltip("Enum to describe current state of game")]
    public GameState currentState;
    



    [SerializeField]
    [Tooltip("Whether the player is currently placing a tower or not")]
    public bool placing = false;

    [Header("Round Handling")]
    public RoundManager rm;
    public int roundNum;

    public Transform spawn;
    public GameObject enemyParent;


    public int GetMoney()
    {
        return curMoney;
    }

    void Awake()
    {
        // grab references to neccessary components
        rm = GetComponentInChildren<RoundManager>();
        spawn = GameObject.Find("EnemySpawn").transform;
        enemyParent = GameObject.Find("EnemyParent");

        // set values of needed variables
        roundNum = 1;
        prevState = currentState = GameState.BetweenRounds;
        fixedDeltaTime = Time.fixedDeltaTime;

    }

    // Start is called before the first frame update
    void Start()
    {
        // money set amount different based on difficulty?
        curMoney = startMoney;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switch (currentState) {
            case GameState.FastForward:
            case GameState.Playing:
                if (!rm.spawning && enemyParent.transform.childCount == 0) {
                    currentState = GameState.BetweenRounds;
                    roundNum++;
                }
                break;
            case GameState.BetweenRounds:
                // change button sprite to play
                break;
            case GameState.Paused:
                // change button sprite to paused
                break;
            case GameState.Idle:
                break;
            
        }

        prevState = currentState;
    }

    public void startRound() {
        rm.processRound(roundNum);
    }

    public void fastForwardButtonPress() {
         if (currentState == GameState.Playing) {
            currentState = GameState.FastForward;
            Time.timeScale = 1.5f;
        } else {
            currentState = GameState.Playing;
            Time.timeScale = 1;
        }
    }
    public void playButtonPress() {
        if (currentState == GameState.Playing) {
            currentState = GameState.Paused;
            Time.timeScale = 0;
        } else {
            if (currentState == GameState.BetweenRounds)
                startRound();
            currentState = GameState.Playing;
            Time.timeScale = 1;
        }
            
    }
}
