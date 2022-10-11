using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options
{
    public float music;
    public float sfx;

    public Options(): this(1, 1) {}
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
