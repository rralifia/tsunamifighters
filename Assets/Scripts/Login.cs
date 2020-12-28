using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField inputUsername;
    public TMP_InputField inputPass;
    public Button login_button;
    public Button create_button;
    public GameObject wrongPass;
    public GameObject wrongUname;


    // Start is called before the first frame update
    void Start()
    {
        login_button.onClick.AddListener(() =>
            // to start a function, use StartCoroutine();
            StartCoroutine(Main.Instance.Web.Login(inputUsername.text, inputPass.text)));

        // for user who hasn't been registered
        // if click this button, scene will be changed into Register scene
        create_button.onClick.AddListener(ClickToRegister);
        //Main.Instance.UserInfo.SetScene(2);
    }

    // to Login scene
    void ClickToRegister()
    {
        SceneManager.LoadScene("Register");
    }
}
