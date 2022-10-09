using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Events;
using UnityEngine.Audio;

public class OptionSceneManager : MonoBehaviour
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
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    [SerializeField]
    private GameObject sfxSliderPrefab, musicSliderPrefab;
    Options options = new Options();

    void Awake() {
        canvas = FindObjectOfType<Canvas>();
        string jsonString = File.ReadAllText(string.Format("Assets/Options.txt"));
        options.LoadData(jsonString);
    }
    // Start is called before the first frame update
    void Start()
    {
        // GameObject exitButtonObj = DefaultControls.CreateButton(new DefaultControls.Resources());
        // exitButtonObj.transform.SetParent(canvas.transform, false);
        // Button exitButton = exitButtonObj.GetComponent<Button>();
        // exitButton.onClick.AddListener():

        // Instantiate slider UI element for music volume NOTE: CHANGE SLIDER PREFAB
        GameObject musicSliderObj = Instantiate(musicSliderPrefab, transform.position, Quaternion.identity, canvas.transform);
        musicSliderObj.transform.position += new Vector3(640, 140, 0);
        Slider musicSlider =  musicSliderObj.GetComponent<Slider>();
        musicSlider.value = options.music;
        musicSlider.onValueChanged.AddListener(new UnityAction<float>(SetMusicLevel));

        // Insantiate slider UI element for sfx volume
        GameObject sfxSliderObj = Instantiate(sfxSliderPrefab, transform.position, Quaternion.identity, canvas.transform);
        sfxSliderObj.transform.position += new Vector3(800, 140, 0);
        Slider sfxSlider =  sfxSliderObj.GetComponent<Slider>();
        sfxSlider.value = options.sfx;
        sfxSlider.onValueChanged.AddListener(new UnityAction<float>(SetSFXLevel));

        // update volume mixer in case of changed options
        SetMusicLevel(options.music);
        SetSFXLevel(options.sfx);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            musicMixer.GetFloat("Music", out options.music);
            options.music = Mathf.Pow(10, options.music / 20);
            sfxMixer.GetFloat("SFX", out options.sfx);
            options.sfx = Mathf.Pow(10, options.sfx / 20);
            System.IO.File.WriteAllText("Assets/Options.txt", options.SaveToString());
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
        }
    }
    public void SetMusicLevel(float sliderValue){
        musicMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXLevel(float sliderValue) {
        sfxMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }
}
