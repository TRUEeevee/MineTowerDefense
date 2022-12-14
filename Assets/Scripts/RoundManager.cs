using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class RoundManager : MonoBehaviour
{
    public class ClusterInfo {
        public int enemyType { get; set; }
        public int count { get; set; }
        public float spaceDelay { get; set; }
        public float processDelay { get; set; }

        // Debug purposes, able to print
        public override string ToString()
        {
            return "Enemy: " + enemyType + " Count: " + count + "\nSpace Delay: " + spaceDelay + " Process Delay: " + processDelay;
        }
    }

    [SerializeField]
    [Tooltip("Boolen to keep track of if a file is still being processed")]
    public bool spawning;

    [SerializeField] GameObject creeper, skeleton, zombie;

    private GameObject[] enemies;

    // object to make parent of all enemies
    private GameObject enemyParent;

    [SerializeField]
    [Tooltip("List containing all the clusters that will make up the round")]
    private List<ClusterInfo> waves;
    

    private List<string> allRoundFiles;

    // Start is called before the first frame update

    void Awake()
    {
        allRoundFiles = new List<string>();
        waves = new List<ClusterInfo>();
        enemies = new GameObject[4] {null, creeper, zombie, skeleton};
        enemyParent = GameObject.Find("EnemyParent");
        
    }
    void Start()
    {

    }

    public void processRound(int roundNum) {
        // fill list of waves with all info from file
        processFile(roundNum);
        spawning = true;

        // start processing clusters via coroutines
        StartCoroutine(StartSpawning());
        
    }

    private IEnumerator StartSpawning() {
        // process each cluster in the waves list and start spawning each of them after the appropriate process delay
        foreach (ClusterInfo cluster in waves) {
            for (int i = 0; i < cluster.count; i++) {
                while (GetComponent<GameManager>().currentState == GameState.Paused) {
                    yield return null;
                }
                GameObject enemy = Instantiate(enemies[cluster.enemyType], GetComponentInParent<GameManager>().spawn.position, Quaternion.identity);
                enemy.transform.parent = enemyParent.transform;
                yield return new WaitForSeconds(cluster.spaceDelay);
                
            }
            yield return new WaitForSeconds(cluster.processDelay);
        }
        waves.Clear();
        spawning = false;
    }


    public void processFile(int roundNum) {
        // load file from Hex/ directory and store into a string
        string fileData = File.ReadAllText(string.Format("Assets/Hex/Round{0}.txt", roundNum));

        // iterate through file content (string) and process 6 bytes into ClusterInfo
        for (int i = 0; i < fileData.Length; i += 6) {
            ClusterInfo temp = new ClusterInfo();
            temp.enemyType = int.Parse(fileData.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            temp.count = int.Parse(fileData.Substring(i+2, 2), System.Globalization.NumberStyles.HexNumber);
            temp.spaceDelay = (int.Parse(fileData.Substring(i+4, 1), System.Globalization.NumberStyles.HexNumber) + 1) * 0.1f;
            temp.processDelay = int.Parse(fileData.Substring(i+5, 1), System.Globalization.NumberStyles.HexNumber) * 0.125f;
            waves.Add(temp);
        }
        // foreach (ClusterInfo wave in waves) {
        //     print(wave);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
