using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Language : MonoBehaviour
{

    public Button EN, JP, ID;
    public GameObject box, textField, audioPanel, buttonPanel;
    public Button nextButton, cancelButton;
    public TextMeshProUGUI next, cancel;
    // public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        box.SetActive(false);
        buttonPanel.SetActive(false);
        EN.onClick.AddListener(() => Choose(1));
        ID.onClick.AddListener(() => Choose(2));
        JP.onClick.AddListener(() => Choose(3));
    }

    public void Choose(int lg)
    {
        if (lg == 1)
        {
            //PlayerPrefs.SetInt("Language", 1);
            Main.Instance.UserInfo.SetLanguage(1);
            textField.GetComponent<TextMeshProUGUI>().text = "You choose English as the language. Are you sure?";
            next.GetComponent<TextMeshProUGUI>().text = "Yes";
            cancel.GetComponent<TextMeshProUGUI>().text = "Cancel";
            buttonPanel.SetActive(true);
            Debug.Log("Language: English" + lg);
        } else if (lg == 2)
        {
            //PlayerPrefs.SetInt("Language", 2);
            Main.Instance.UserInfo.SetLanguage(2);
            textField.GetComponent<TextMeshProUGUI>().text = "Kamu memilih Bahasa Indonesia sebagai bahasa. Apakah kamu yakin?";
            next.GetComponent<TextMeshProUGUI>().text = "Ya";
            cancel.GetComponent<TextMeshProUGUI>().text = "Batal";
            buttonPanel.SetActive(true);
            Debug.Log("Language: Indonesia" + lg);
        } else if (lg == 3)
        {
            //PlayerPrefs.SetInt("Language", 3);
            Main.Instance.UserInfo.SetLanguage(3);
            textField.GetComponent<TextMeshProUGUI>().text = "言語は日本語にします。よろしいですか？";
            next.GetComponent<TextMeshProUGUI>().text = "はい";
            cancel.GetComponent<TextMeshProUGUI>().text = "キャンセル";
            buttonPanel.SetActive(true);
            Debug.Log("Language: Japanese" + lg);
        }

        box.SetActive(true);
        nextButton.onClick.AddListener(LoadRegister);
        cancelButton.onClick.AddListener(Start);
    }

    public void LoadRegister()
    {
        SceneManager.LoadScene("Register");
    }
}
