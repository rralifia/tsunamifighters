using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    public AudioClip sound;
    public GameObject audioPanel;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => PlaySound());
        // gameObject.GetComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
    }
    public void PlaySound()
    {
        audioPanel.SetActive(true);
        source.Play();
        source.PlayOneShot(sound, 5.0f);
    }
}
