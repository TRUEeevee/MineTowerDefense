using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using System.IO;

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
}

public class GameManager : MonoBehaviour
{
    private float fixedDeltaTime;
    [Header("Audio Variables")]
    public AudioMixer musicMixer, sfxMixer;

    [Header("Public variables to be used throughout the game")]
    [Tooltip("Amount of health player has, game ends when this reaches 0")]
    [Range(0, 1000)] public int health;

    [SerializeField]
    [Tooltip("Amount of currency used to build/upgrade towers. Will (maybe) be replaced by resources in the future")]
    private int startMoney;
    [SerializeField]
    private int curMoney;


    [Header("Game State")]
    [SerializeField]
    private GameState prevState;

    [SerializeField]
    [Tooltip("Enum to describe current state of game")]
    public GameState currentState;

    [SerializeField]
    private bool FastForward = false;

    [Header("")]
    [SerializeField]
    public GameObject projectileParent;
    [SerializeField]
    public GameObject lastClickedTower = null;

    [SerializeField]
    public GameObject pauseMenu, UpgradeUI, leftButton, rightButton;
    [SerializeField]
    Slider musicSlider, sfxSlider;

    private float prevTimeScale;
    private Options options = new Options();
    private GameState tempState;
    



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

    public void AddMoney(int amount) {
        curMoney += amount;
    }

    public void SubMoney(int amount) {
        curMoney -= amount;
    }

    public bool CanAfford(int amount) {
        return curMoney >= amount;
    }

    void Awake()
    {
        // grab references to neccessary components
        rm = GetComponentInChildren<RoundManager>();
        spawn = GameObject.Find("EnemySpawn").transform;
        enemyParent = GameObject.Find("EnemyParent");
        projectileParent = GameObject.Find("ProjectileParent");
        UpgradeUI = GameObject.Find("UpgradeUI");
        leftButton = GameObject.Find("LeftPath");
        rightButton = GameObject.Find("RightPath");

        // set values of needed variables
        roundNum = 1;
        prevState = currentState = GameState.BetweenRounds;
        fixedDeltaTime = Time.fixedDeltaTime;

        string jsonString = File.ReadAllText("Assets/Options.txt");
        options.LoadData(jsonString);

    }

    // Start is called before the first frame update
    void Start()
    {
        // money set amount different based on difficulty?
        curMoney = startMoney;
        prevTimeScale = Time.timeScale;

        musicSlider.value = options.music;
        musicSlider.onValueChanged.AddListener(new UnityAction<float>(SetMusicLevel));

        sfxSlider.value = options.sfx;
        sfxSlider.onValueChanged.AddListener(new UnityAction<float>(SetSFXLevel));

        SetMusicLevel(options.music);
        SetSFXLevel(options.sfx);

        pauseMenu.SetActive(false);
        UpgradeUI.SetActive(false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        // state machine 
        switch (currentState) {
            case GameState.Playing:
                if (!rm.spawning && enemyParent.transform.childCount == 0) {
                    currentState = GameState.BetweenRounds;
                    roundNum++;
                    for (int i = 0; i < projectileParent.transform.childCount; i++) {
                        Destroy(projectileParent.transform.GetChild(i).gameObject);
                    }
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
    private void Update() {
        // if a tower was clicked, this will click off the tower
        // if (Input.GetMouseButtonDown(0) && lastClickedTower && !(currentState == GameState.Paused)) {
            // if (lastClickedTower.transform.GetChild(0).gameObject.activeSelf) {
            // unclickTower();
            
        // }
        if (!placing && Input.GetKeyDown(KeyCode.Escape)) {
            if (currentState == GameState.Paused)
                Resume();
            else 
                Pause();
        }
    }

    public void unclickTower() {
        UpgradeUI.SetActive(false);
        lastClickedTower.transform.GetChild(0).gameObject.SetActive(false);
        lastClickedTower = null;
    }


    public void startRound() {
        rm.processRound(roundNum);
        currentState = GameState.Playing;
    }

    public void playButtonPress() {
        if (currentState == GameState.BetweenRounds)
            startRound();
        else
            FastForward = !FastForward;

        Time.timeScale = FastForward ? 1.5f : 1;
    }

    public void Pause() {
        tempState = currentState;
        currentState = GameState.Paused;
        pauseMenu.SetActive(true);
        SetMusicLevel(options.music);
        SetSFXLevel(options.sfx);
        prevTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Resume() {
        musicMixer.GetFloat("Music", out options.music);
        options.music = Mathf.Pow(10, options.music / 20);
        sfxMixer.GetFloat("SFX", out options.sfx);
        options.sfx = Mathf.Pow(10, options.sfx / 20);
        System.IO.File.WriteAllText("Assets/Options.txt", options.SaveToString());

        Time.timeScale = prevTimeScale;
        currentState = tempState;
        pauseMenu.SetActive(false);
    }

    public void SetMusicLevel(float sliderValue){
        musicMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXLevel(float sliderValue) {
        sfxMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }

    public void leftUpgrade() {
        switch(lastClickedTower.tag) {
            case "BowTower":
                lastClickedTower.GetComponent<ArcherUpgradeModule>().upgradeLeft();
                break;
            case "MeleeTower":
                break;
            case "GrieferTower":
                break;

        }
    }
    public void RightUpgrade() {
        switch(lastClickedTower.tag) {
            case "BowTower":
                lastClickedTower.GetComponent<ArcherUpgradeModule>().upgradeRight();
                break;
            case "MeleeTower":
                break;
            case "GrieferTower":
                break;

        }
    }
}
