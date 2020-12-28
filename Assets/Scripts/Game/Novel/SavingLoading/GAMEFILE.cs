using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GAMEFILE
{
    /// <summary>
    /// The currently open game file.
    /// </summary>
    public static GAMEFILE activeFile = new GAMEFILE();

    public string chapterName;
    public int chapterProgress = 0;

    public string playerName = "";

    public string cachedLastSpeaker = "";

    public string currentTextSystemSpeakerNameText = "";
    public string currentTextSystemDisplayText = "";

    public List<CHARACTERDATA> charactersInScene = new List<CHARACTERDATA>();

    //public List<AudioClip> ambiance = new List<AudioClip>();

    public Texture background = null;

    /*public AudioClip music = null;

    public string modificationDate = "";
    public Texture2D previewImage = null;

    public string[] tempVals = new string[9];*/

    public GAMEFILE()
    {
        if (UserInfo.language == 1)
        {
            this.chapterName = "chap0a_eng";
        }
        else if (UserInfo.language == 2)
        {
            this.chapterName = "chap0a_ind";
        }
        else if (UserInfo.language == 3)
        {
            this.chapterName = "chap0a_jpn";
        }

        this.chapterProgress = 0;
        this.cachedLastSpeaker = "";

        this.playerName = "No Name";

        this.background = null;
        //this.cinematic = null;
        //this.foreground = null;

        //this.music = null;

        charactersInScene = new List<CHARACTERDATA>();
        //ambiance = new List<AudioClip>();
        //tempVals = new string[9];
    }

    [System.Serializable]
    public class CHARACTERDATA
    {
        public string characterName = "";
        public string displayName = "";
        public bool enabled = true;
        public string facialExpression = "";
        //public bool facingLeft = true;
        public Vector2 position = Vector2.zero;

        public CHARACTERDATA(Character character)
        {
            this.characterName = character.characterName;
            this.displayName = character.displayName;
            this.enabled = character.enabled;
            this.facialExpression = character.renderers.expressionRenderer.sprite.name;
            //this.facingLeft = character.isFacingLeft;
            this.position = character._targetPosition;
        }
    }
}
