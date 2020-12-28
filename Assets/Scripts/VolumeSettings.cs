using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    private static readonly string musicPref = "musicPref";
    private static readonly string soundPref = "soundPref";
    public static float musicFloat, soundFloat;
    public AudioSource[] musicAudio;
    public AudioSource[] SFXAudio;

    void Awake()
    {
        ContinueSettings();
    }

    // Update is called once per frame
    void ContinueSettings()
    {
        musicFloat = PlayerPrefs.GetFloat(musicPref);
        soundFloat = PlayerPrefs.GetFloat(soundPref);

        for (int i = 0; i < musicAudio.Length; i++)
        {
            musicAudio[i].volume = musicFloat;
        }

        for (int i = 0; i < SFXAudio.Length; i++)
        {
            SFXAudio[i].volume = soundFloat;
        }
    }
}
