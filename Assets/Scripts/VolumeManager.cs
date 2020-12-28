using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string musicPref = "musicPref";
    private static readonly string soundPref = "soundPref";
    private int firstPlayInt;
    public Slider musicSlider, soundSlider;
    private float musicFloat, soundFloat;
    public AudioSource[] musicAudio;
    public AudioSource[] SFXAudio;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            musicFloat = .25f;
            soundFloat = .40f;
            musicSlider.value = musicFloat;
            soundSlider.value = soundFloat;

            PlayerPrefs.SetFloat(musicPref, musicFloat);
            PlayerPrefs.SetFloat(soundPref, soundFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        } else
        {
            musicFloat = PlayerPrefs.GetFloat(musicPref);
            musicSlider.value = musicFloat;
            soundFloat = PlayerPrefs.GetFloat(soundPref);
            soundSlider.value = soundFloat;
        }
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(musicPref, musicSlider.value);
        PlayerPrefs.SetFloat(soundPref, soundSlider.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
        if(!inFocus)
        {
            SaveVolumeSettings();
        }
    }

    public void UpdateSound()
    {
        for (int i = 0; i < musicAudio.Length; i++)
        {
            musicAudio[i].volume = musicSlider.value;
        }

        for (int i=0; i < SFXAudio.Length; i++)
        {
            SFXAudio[i].volume = soundSlider.value;
        }
    }
}
