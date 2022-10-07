using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Events;

public class TitleSceneManager : MonoBehaviour
{
    public class Options {
        public float music;
        public float sfx;

        public Options() {}
        public Options(int music_, int sfx_) {
            music = music_;
            sfx = sfx_;
        }

        public string SaveToString() {
            return JsonUtility.ToJson(this, true);
        }
        public void LoadData(string jsonString) {
            JsonUtility.FromJsonOverwrite(jsonString, this);
        }

        public override string ToString() {
            return "Music: " + music + "\nSFX: " + sfx; 
        }

    }
    Canvas canvas;
    public string jsonString;
    Options options = new Options();
    void Awake() {
        canvas = FindObjectOfType<Canvas>();
        if (!System.IO.File.Exists("Assets/Options.txt")) {
            Options opt = new Options(1, 1);
            System.IO.File.WriteAllText("Assets/Options.txt", opt.SaveToString());
        }
    }
    void Start()
    {
        jsonString = File.ReadAllText(string.Format("Assets/Options.txt"));
        options.LoadData(jsonString);

        GameObject newButton = DefaultControls.CreateButton(new DefaultControls.Resources());
        newButton.transform.SetParent(canvas.transform, false);
        newButton.GetComponent<Button>().onClick.AddListener(LoadOptions);
        
    }

    public void LoadOptions() {
        // SceneManager.LoadScene("OptionsScene", LoadSceneMode.Single);
        print(options);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
