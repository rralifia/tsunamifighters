using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreA, scoreB, jijoA, jijoB, kyojoA, kyojoB, kojoA, kojoB;

    // Start is called before the first frame update
    void Start()
    {
        //Set all score to 0 in start
        PlayerPrefs.SetInt("ScoreA", 0);
        PlayerPrefs.SetInt("ScoreB", 0);
        PlayerPrefs.SetInt("JijoA", 0);
        PlayerPrefs.SetInt("JijoB", 0);
        PlayerPrefs.SetInt("KyojoA", 0);
        PlayerPrefs.SetInt("KyojoB", 0);
        PlayerPrefs.SetInt("KojoA", 0);
        PlayerPrefs.SetInt("KojoB", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Shows the score to the UI (score bar)
        scoreA.text = PlayerPrefs.GetInt("ScoreA").ToString();
        scoreB.text = PlayerPrefs.GetInt("ScoreB").ToString();
        jijoA.text = PlayerPrefs.GetInt("JijoA").ToString();
        jijoB.text = PlayerPrefs.GetInt("JijoB").ToString();
        kyojoA.text = PlayerPrefs.GetInt("KyojoA").ToString();
        kyojoB.text = PlayerPrefs.GetInt("KyojoB").ToString();
        kojoA.text = PlayerPrefs.GetInt("KojoA").ToString();
        kojoB.text = PlayerPrefs.GetInt("KojoB").ToString();
    }

}

