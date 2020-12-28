using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move2 : MonoBehaviour
{
    // move to next + 2 gameobject

    public void moveobject(bool Move2)
    {
        gameObject.SetActive(false);
        transform.parent.GetChild(gameObject.transform.GetSiblingIndex() + 2).gameObject.SetActive(true);
    }

    public void nextquest()
    {
        int quest = PlayerPrefs.GetInt("QuestNumber") + 1;
        PlayerPrefs.SetInt("QuestNumber", quest);
    }
}
