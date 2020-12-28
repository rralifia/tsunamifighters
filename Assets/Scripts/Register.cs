using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Register : MonoBehaviour
{
    public TMP_InputField nameField, usernameField, passField, ageField;
    public Button save_button, transfer_button;
    public Button female, male;
    public Button jpn, idn, others, close;
    public GameObject takenUsername, error_window;
    public TextMeshProUGUI guideName, guideUsername, guidePass;
    string gender, location;
    //Color32 lightPurple = new Color32(191, 178, 182, 186);

    // Start is called before the first frame update
    void Start()
    {
        Begin();
        if (UserInfo.language == 1)
        {
            guideName.GetComponent<TextMeshProUGUI>().text = "Name must be at least 8 letters.";
            guideUsername.GetComponent<TextMeshProUGUI>().text = "Username must consist of at least 6 characters.";
            guidePass.GetComponent<TextMeshProUGUI>().text = "Password must consist of at least 8 characters.";
        }
        else if (UserInfo.language == 2)
        {
            guideName.GetComponent<TextMeshProUGUI>().text = "Nama harus terdiri dari minimal 8 huruf.";
            guideUsername.GetComponent<TextMeshProUGUI>().text = "Username harus terdiri dari minimal 6 karakter.";
            guidePass.GetComponent<TextMeshProUGUI>().text = "Kata sandi harus terdiri dari minimal 8 karakter.";
        }
        else if (UserInfo.language == 3)
        {
            guideName.GetComponent<TextMeshProUGUI>().text = "名前は 8 文字以上にする必要があります。";
            guideUsername.GetComponent<TextMeshProUGUI>().text = "パスワードは 8 文字以上の文字、数字または記号で構成する必要があります。";
            guidePass.GetComponent<TextMeshProUGUI>().text = "ユーザーネームは 6 文字以上の文字、数字または記号で構成する必要があります。";
        }
    }

    void Begin()
    {
        //Main.Instance.UserInfo.SetScene(1);
        takenUsername.SetActive(false);
        female.onClick.AddListener(ChooseFemale);
        male.onClick.AddListener(ChooseMale);

        jpn.onClick.AddListener(ChooseJPN);
        idn.onClick.AddListener(ChooseIDN);
        others.onClick.AddListener(ChooseOthers);

        error_window.SetActive(false);

        save_button.onClick.AddListener(CheckField);
        gender = string.Empty;
        location = string.Empty;

        // for user who has been registered
        // if click this button, scene will be changed into Login scene
        transfer_button.onClick.AddListener(ClickToLogin);
    }

    void CheckField()
    {
        if ((string.IsNullOrEmpty(nameField.text)) || (string.IsNullOrEmpty(usernameField.text)) || (string.IsNullOrEmpty(passField.text)) || (string.IsNullOrEmpty(ageField.text)) || (string.IsNullOrEmpty(gender)) || (string.IsNullOrEmpty(location)))
        {
            error_window.SetActive(true);
            close.onClick.AddListener(Begin);
        }
        else
        {
            StartCoroutine(Main.Instance.Web.RegisterUser(nameField.text, usernameField.text, passField.text, int.Parse(ageField.text), gender, location));
        }
    }

    public void VerifyInputs()
    {
        save_button.interactable = ((nameField.text.Length >= 8) && (usernameField.text.Length >= 6) && (passField.text.Length >= 8));

        if (nameField.text.Length >= 8)
        {
            guideName.text = "";
        } else if (nameField.text.Length < 8)
        {
            if (UserInfo.language == 1)
            {
                guideName.GetComponent<TextMeshProUGUI>().text = "Name must consist of a minimum 8 letters.";
            } else if (UserInfo.language == 2)
            {
                guideName.GetComponent<TextMeshProUGUI>().text = "Nama harus terdiri dari minimal 8 huruf.";
            } else if (UserInfo.language == 3)
            {
                guideName.GetComponent<TextMeshProUGUI>().text = "";
            }
           
        }

        if (usernameField.text.Length >= 6)
        {
            guideUsername.text = "";
        } else if (usernameField.text.Length < 6)
        {
            if (UserInfo.language == 1)
            {
                guideUsername.GetComponent<TextMeshProUGUI>().text = "Username must consist of at minimum 6 letters and/ or characters.";
            } else if (UserInfo.language == 2)
            {
                guideUsername.GetComponent<TextMeshProUGUI>().text = "Username harus terdiri dari minimal 6 huruf dan/ atau karakter.";
            } else if (UserInfo.language == 3)
            {
                guideUsername.GetComponent<TextMeshProUGUI>().text = "";
            }
        }

        if (passField.text.Length >= 6)
        {
            guidePass.text = "";
        } else if (passField.text.Length < 8)
        {
            if (UserInfo.language == 1)
            {
                guidePass.GetComponent<TextMeshProUGUI>().text = "Password must consist of a minimum of 8 letters and/ or characters.";
            } else if (UserInfo.language == 2)
            {
                guidePass.GetComponent<TextMeshProUGUI>().text = "Kata sandi harus terdiri dari minimal 8 huruf dan/ atau karakter.";
            } else if (UserInfo.language == 3)
            {
                guidePass.GetComponent<TextMeshProUGUI>().text = "";
            }

        }
    }

    void ChooseFemale()
    {
        gender = "Female";
        male.image.color = Color.white;
        female.image.color = new Color32(191, 178, 182, 186);
        //Debug.Log("Female pressed");
    }

    void ChooseMale()
    {
        gender = "Male";
        male.image.color = new Color32(191, 178, 182, 186);
        female.image.color = Color.white;
        //Debug.Log("Male pressed");
    }

    void ChooseJPN()
    {
        location = "Japan";
        jpn.image.color = new Color32(191, 178, 182, 186);
        idn.image.color = Color.white;
        others.image.color = Color.white;
        //Debug.Log("Female pressed");
    }

    void ChooseOthers()
    {
        location = "Others";
        others.image.color = new Color32(191, 178, 182, 186);
        idn.image.color = Color.white;
        jpn.image.color = Color.white;
    }

    void ChooseIDN()
    {
        location = "Indonesia";
        idn.image.color = new Color32(191, 178, 182, 186);
        others.image.color = Color.white;
        jpn.image.color = Color.white;
        //Debug.Log("Male pressed");
    }
    void ClickToLogin()
    {
        SceneManager.LoadScene("Login");
    }
}