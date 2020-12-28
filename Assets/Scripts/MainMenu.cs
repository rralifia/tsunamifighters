using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject main_page, profile_page, instruct_page, settings_page;
    public GameObject back_footer, desc_box;

    public Button start, instruct, settings, quit, back_button, profile, learn;
    public Button open_box, specialback_button;

    public TextMeshProUGUI playername, location;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();
        Debug.Log("user_id in Main Menu from Register: " + UserInfo.user_id);
        StartCoroutine(Main.Instance.Web.GetName(UserInfo.user_id));
        StartCoroutine(Main.Instance.Web.GetLocation(UserInfo.user_id));
        //StartCoroutine(ShowProfile());
        ClickToMain();
        main_page.SetActive(true);
        PlayerPrefs.SetInt("SceneNumber", 3);
        int scene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Scene Number in Main Menu: " + scene);
    }

    void Update()
    {
        //yield return new WaitForSeconds(2.0f);
        PlayerPrefs.SetString("PlayerName", UserInfo.playerName);
        playername.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerName");
        PlayerPrefs.SetString("PlayerLoc", UserInfo.playerLoc);
        location.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerLoc");
        //ClickToMain();
    }

    public void ClickToMain()
    {
        main_page.SetActive(true);
        // profile_header.SetActive(true);
        back_footer.SetActive(false);
        profile_page.SetActive(false);
        instruct_page.SetActive(false);
        settings_page.SetActive(false);
        desc_box.SetActive(false);

        //ShowProfile();
        //Debug.Log("ShowProfile");

        start.onClick.AddListener(ClickToStart);
        profile.onClick.AddListener(LoadProfile);
        instruct.onClick.AddListener(LoadInstruction);
        learn.onClick.AddListener(ClickToLearn);
        settings.onClick.AddListener(LoadSettings);
        quit.onClick.AddListener(Application.Quit);
    }

    void ClickToStart()
    {
        SceneManager.LoadScene("Pre Test");
        PlayerPrefs.SetInt("SceneNumber", 4);
        int scene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Scene Number in ClickToStart: " + scene);

        Debug.Log("playerName after Start: " + UserInfo.playerName);
        Debug.Log("playerLoc after Start: " + UserInfo.playerLoc);
    }

    void LoadProfile()
    {

        // data_objects.SetActive(false);
        main_page.SetActive(false);
        // profile_header.SetActive(false);
        back_footer.SetActive(true);
        profile_page.SetActive(true);
        instruct_page.SetActive(false);
        settings_page.SetActive(false);
        Debug.Log("user_id in LoadProfile: " + UserInfo.user_id);
        back_button.onClick.AddListener(ClickToMain);
    }

    void LoadInstruction()
    {
        main_page.SetActive(false);
        // profile_header.SetActive(false);
        back_footer.SetActive(true);
        profile_page.SetActive(false);
        instruct_page.SetActive(true);
        settings_page.SetActive(false);

        back_button.onClick.AddListener(ClickToMain);
    }

    void LoadSettings()
    {
        main_page.SetActive(false);
        // profile_header.SetActive(false);
        back_footer.SetActive(true);
        profile_page.SetActive(false);
        instruct_page.SetActive(false);
        settings_page.SetActive(true);
        desc_box.SetActive(false);

        //default mode: sound on, music on, languange in bahasa, description off

        open_box.onClick.AddListener(LoadDescription);
        back_button.onClick.AddListener(ClickToMain);
    }

    void LoadDescription()
    {
        main_page.SetActive(false);
        // profile_header.SetActive(false);
        back_footer.SetActive(false);
        profile_page.SetActive(false);
        instruct_page.SetActive(false);
        settings_page.SetActive(true);
        desc_box.SetActive(true);

        specialback_button.onClick.AddListener(LoadSettings);
    }

    void ClickToLearn()
    {
        SceneManager.LoadScene("Learning");
    }
}
