using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowScores : MonoBehaviour
{
    public GameObject pretest_box, posttest_box, gamescore_box;
    public GameObject jijo_box, kyojo_box, kojo_box, score_label, score_value;
    // Start is called before the first frame update

    void Start()
    {
        // Showing scores to UI
        pretest_box.GetComponent<TextMeshProUGUI>().text = ScoresInfo.pretest_score;
        posttest_box.GetComponent<TextMeshProUGUI>().text = Posttest.posttest_score;
        gamescore_box.GetComponent<TextMeshProUGUI>().text = ScoresInfo.game_score;

        jijo_box.GetComponent<TextMeshProUGUI>().text = ScoresInfo.jijo_score;
        kyojo_box.GetComponent<TextMeshProUGUI>().text = ScoresInfo.kyojo_score;
        kojo_box.GetComponent<TextMeshProUGUI>().text = ScoresInfo.kojo_score;

        // Debugging
        Debug.Log("Pre-test score in ShowScores: " + ScoresInfo.pretest_score);
        Debug.Log("Post-test score in ShowScores: " + Posttest.posttest_score);
    }

    public void NextScene()
    {
        transform.parent.GetChild(gameObject.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
        score_label.SetActive(false);
        score_value.SetActive(false);
        gameObject.SetActive(false);
    }
}
