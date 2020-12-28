using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move1 : MonoBehaviour
{
    // move to next + 1 gameobject
    public void moveobject(bool Move1)
    {
        gameObject.SetActive(false);
        transform.parent.GetChild(gameObject.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
    }

    public void nextquest()
    {
        int quest = PlayerPrefs.GetInt("QuestNumber") + 1;
        PlayerPrefs.SetInt("QuestNumber", quest);
    }
}
