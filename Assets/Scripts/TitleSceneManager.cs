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
    void Awake() {
        canvas = FindObjectOfType<Canvas>();
        if (!System.IO.File.Exists("Assets/Options.txt")) {
            Options opt = new Options(1, 1);
            System.IO.File.WriteAllText("Assets/Options.txt", opt.SaveToString());
        }
    }
    void Start()
    {
        
    }

    public void PlayButton() {
        SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
    }

    public void OptionsButton() {
        SceneManager.LoadScene("OptionsScene", LoadSceneMode.Single);
    }

    public void ExitButton() {
        // 1st line is to quit application, 2nd line is to quit playing in editor
        // Uncomment/comment lines when game is ready to be shipped
        // Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
