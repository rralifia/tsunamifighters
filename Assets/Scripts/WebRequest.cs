using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class WebRequest : MonoBehaviour
{
    void Start()
    {
    }

    // Login function; connecting GUI & PHP + mySQL
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/LoginUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Debug.Log(www.downloadHandler.text);
                // getting input from user, send the data to UserInfo (in-game temp database)
                Main.Instance.UserInfo.SetCredentials(username, password);
                Main.Instance.UserInfo.SetID(www.downloadHandler.text);

                // feedback for wrong credentials - still off
                Main.Instance.Login.wrongPass.SetActive(false);
                Main.Instance.Login.wrongUname.SetActive(false);

                //log in with wrong password
                if (www.downloadHandler.text.Contains("Wrong Credentials."))
                {
                    Debug.Log("Try again, password does not match.");
                    // occur wrong password warning (feedback)
                    Main.Instance.Login.wrongPass.SetActive(true);
                }
                // log in with wrong username
                else if (www.downloadHandler.text.Contains("Username does not exist."))
                {
                    Debug.Log("Try again, username does not exist.");
                    // occur wrong username warning (feedback)
                    Main.Instance.Login.wrongUname.SetActive(true);
                }
                // log in correctly, move to next scene
                else
                    SceneManager.LoadScene("Main Menu");
                }
            }
        }

    // Register function
    public IEnumerator RegisterUser(string name, string username, string password, int age, string gender, string loc)
    {
        WWWForm form = new WWWForm();
        form.AddField("registName", name);
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("registAge", age);
        form.AddField("registGender", gender);
        form.AddField("registLoc", loc);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                // if username is not unique
                if (www.downloadHandler.text.Contains("Username is already taken."))
                {
                    Debug.Log("Try another username.");
                    // occur taken username warning (feedback)
                    Main.Instance.Register.takenUsername.SetActive(true);
                } else if (www.downloadHandler.text.Contains("You must fill all input variables."))
                {
                    Debug.Log("Data incompleted.");
                    Main.Instance.Register.error_window.SetActive(true);
                } else
                {
                    // getting input from user, send the data to UserInfo (in-game temp database)
                    // www.downloadHandler.text = last recorded id in table users
                    Main.Instance.UserInfo.SetID(www.downloadHandler.text);
                    // move scene
                    SceneManager.LoadScene("Main Menu");
                }
            }
        }
    }

    // Exporting player's Novel scores to MySQL
    public IEnumerator AddNovelScores(int jijoScore, int kyojoScore, int kojoScore, int gameScore, string playID)
    {
        WWWForm form = new WWWForm();
        form.AddField("jijoScore", jijoScore);
        form.AddField("kyojoScore", kyojoScore);
        form.AddField("kojoScore",kojoScore);
        form.AddField("gameScore", gameScore);
        form.AddField("playID", playID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/ExportNovelScores.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.DisconnectedDataNovel();
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("Novel scores updated successfully."))
                {
                    Debug.Log("Novel scores sent.");
                } else
                {
                    NovelController.instance.DisconnectedDataNovel();
                }
            }
        }
    }

    // Importing player's Jijo score from MySQL
    public IEnumerator GetJijo(string playID)
    {

        WWWForm form = new WWWForm();
        form.AddField("playID", playID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetJijo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.Reimport(1);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("0 results."))
                {
                    Debug.Log("Transfer failed.");
                    Main.Instance.ScoresInfo.SetJijo("***");
                }
                else
                {
                    string jijoString = www.downloadHandler.text.ToString();
                    Debug.Log("string Jijo in GetJijo: " + jijoString);
                    Main.Instance.ScoresInfo.SetJijo(jijoString);
                }
            }
        }
    }

    // Exporting Jijo score to MySQL
    public IEnumerator AddJijo(string playID, int jijoScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("playID", playID);
        form.AddField("jijoScore", jijoScore);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/AddJijo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.DisconnectedDataNovel();
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string jijoString = www.downloadHandler.text.ToString();
                Debug.Log("string Jijo in AddJijo: " + jijoString);
                Main.Instance.ScoresInfo.SetJijo(jijoString);
            }
        }
    }

    // Importing player's kojo score from MySQL
    public IEnumerator GetKojo(string playID)
    {

        WWWForm form = new WWWForm();
        form.AddField("playID", playID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetKojo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.Reimport(2);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("0 results."))
                {
                    Debug.Log("Transfer failed.");
                    Main.Instance.ScoresInfo.SetKojo("***");
                } else
                {
                    string kojoString = www.downloadHandler.text.ToString();
                    Debug.Log("string Kojo in GetKojo: " + kojoString);
                    Main.Instance.ScoresInfo.SetKojo(kojoString);
                }
            }
        }
    }

    // Exporting Kojo score to mySQL
    public IEnumerator AddKojo(string playID, int kojoScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("playID", playID);
        form.AddField("kojoScore", kojoScore);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/AddKojo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.DisconnectedDataNovel();
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string kojoString = www.downloadHandler.text.ToString();
                Debug.Log("string Kojo in AddKojo: " + kojoString);
                Main.Instance.ScoresInfo.SetKojo(kojoString);
            }
        }
    }

    // Importing player's game score from MySQL
    public IEnumerator GetGame(string playID)
    {

        WWWForm form = new WWWForm();
        form.AddField("playID", playID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetGame.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.Reimport(4);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string gameString = www.downloadHandler.text.ToString();
                Debug.Log("string Game Score in GetGame: " + gameString);
                Main.Instance.ScoresInfo.SetGame(gameString);
            }
        }
    }

    // Exporting Game score to mySQL
    public IEnumerator AddGame(string playID, int gameScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("playID", playID);
        form.AddField("gameScore", gameScore);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/AddGame.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.DisconnectedDataNovel();
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("0 results."))
                {
                    Debug.Log("Transfer failed.");
                    Main.Instance.ScoresInfo.SetGame("***");
                }
                else
                {
                    string gameString = www.downloadHandler.text.ToString();
                    Debug.Log("string Game Score in AddGame: " + gameString);
                    Main.Instance.ScoresInfo.SetGame(gameString);
                }
            }
        }
    }

    // Get player's kyojo score from MySQL
    public IEnumerator GetKyojo(string playID)
    {

        WWWForm form = new WWWForm();
        form.AddField("playID", playID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetKyojo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.Reimport(3);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("0 results."))
                {
                    Debug.Log("Transfer failed.");
                    Main.Instance.ScoresInfo.SetKyojo("***");
                } else
                {
                    string kyojoString = www.downloadHandler.text.ToString();
                    Debug.Log("string Kyojo in GetKyojo: " + kyojoString);
                    Main.Instance.ScoresInfo.SetKyojo(kyojoString);
                }
            }
        }
    }

    // Exporting Kyojo score into mySQL
    public IEnumerator AddKyojo(string playID, int kyojoScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("playID", playID);
        form.AddField("kyojoScore", kyojoScore);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/AddKyojo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.DisconnectedDataNovel();
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string kyojoString = www.downloadHandler.text.ToString();
                Debug.Log("string Kyojo in AddKyojo: " + kyojoString);
                Main.Instance.ScoresInfo.SetKyojo(kyojoString);
            }
        }
    }

    public IEnumerator AddPretest (int preScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("preScore", preScore);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/AddPretest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Main.Instance.QuizOpening.disconnectedPanel.SetActive(true);
            }
            else
            {
                Debug.Log("AddPretest succeed.");
                //Debug.Log(www.downloadHandler.text);
                string playId = www.downloadHandler.text;
                Debug.Log("Play ID in WebRequest: " + playId);
                Main.Instance.ScoresInfo.SetPlayID(playId);
            }
        }
    }

    // Get player's game score from MySQL
    public IEnumerator GetPretest(string playID)
    {

        WWWForm form = new WWWForm();
        form.AddField("playID", playID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetPretest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string pretestString = www.downloadHandler.text.ToString();
                Debug.Log("string Pretest Score in WebRequest: " + pretestString);
                Main.Instance.ScoresInfo.SetPretest(pretestString);
            }
        }
    }

    // Exporting player's posttest score into MySQL
    public IEnumerator AddPosttest(int postScore, string playID, string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("postScore", postScore);
        form.AddField("playID", playID);
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/AddPosttest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Main.Instance.QuizOpening.disconnectedPanel.SetActive(true);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string posttestString = www.downloadHandler.text;
                Main.Instance.ScoresInfo.SetPosttest(posttestString);
            }
        }
    }

    // Exporting player's quiz answers
    public IEnumerator ExportAnswers(string answer1, string answer2, string answer3, string answer4, string answer5, string answer6, string answer7, string answer8, string answer9, string answer10, string answer11, string answer12, string answer13, string answer14, string answer15, string answer16, string answer17, string answer18, string answer19, string answer20, string playID, string type)
    {
        WWWForm form = new WWWForm();
        form.AddField("answer1", answer1);
        form.AddField("answer2", answer2);
        form.AddField("answer3", answer3);
        form.AddField("answer4", answer4);
        form.AddField("answer5", answer5);
        form.AddField("answer6", answer6);
        form.AddField("answer7", answer7);
        form.AddField("answer8", answer8);
        form.AddField("answer9", answer9);
        form.AddField("answer10", answer10);
        form.AddField("answer11", answer11);
        form.AddField("answer12", answer12);
        form.AddField("answer13", answer13);
        form.AddField("answer14", answer14);
        form.AddField("answer15", answer15);
        form.AddField("answer16", answer16);
        form.AddField("answer17", answer17);
        form.AddField("answer18", answer18);
        form.AddField("answer19", answer19);
        form.AddField("answer20", answer20);
        form.AddField("playID", playID);
        form.AddField("type", type);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/ExportAnswers.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Main.Instance.QuizOpening.disconnectedPanel.SetActive(true);
            }
            else
            {
                Debug.Log("Export Answers succeed.");
                // Debug.Log(www.downloadHandler.text);
                string quizId = www.downloadHandler.text;
                Debug.Log("Quiz ID in WebRequest: " + quizId);
                if (type == "pretest")
                {
                    Main.Instance.UserInfo.SetQuizID(quizId, "pretest");
                } else if (type == "posttest")
                {
                    Main.Instance.UserInfo.SetQuizID(quizId, "posttest");
                }
            }
        }
    }

    // Importing player's profile from MySQL
    public IEnumerator GetProfile(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        //Debug.Log("userID in GetProfile: " + userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetProfile.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonString = www.downloadHandler.text;
                Debug.Log("string from JSON in WebRequest: "+jsonString);

                callback(jsonString);
            }
        }   
    }

    // Get player's name from MySQL
    public IEnumerator GetName(string userID)
    {

        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetName.php", form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Main.Instance.UserInfo.ReimportData(6);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string nameString = www.downloadHandler.text;
                Debug.Log("string Name in WebRequest: " + nameString);
                Main.Instance.UserInfo.SetName(nameString);
            }
        }
    }

    // Get player's location from MySQL
    public IEnumerator GetLocation(string userID)
    {

        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetLoc.php", form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Main.Instance.UserInfo.ReimportData(5);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string locString = www.downloadHandler.text;
                Debug.Log("string Loc in WebRequest: " + locString);
                Main.Instance.UserInfo.SetLocation(locString);
            }
        }
    }

    // Export idA and idB to database, import multiplayId from database
    public IEnumerator AddMultiplay(string idA, string idB)
    {
        WWWForm form = new WWWForm();
        form.AddField("idA", idA);
        form.AddField("idB", idB);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/AddMultiplay.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                NovelController.instance.disconnectedDataPanel.SetActive(true);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("You must fill all input variables."))
                {
                    Debug.Log("Data incompleted.");
                    Main.Instance.Multiplay.SetMultiplay("No ID");
                } else
                {
                    string multiplayId = www.downloadHandler.text;
                    Debug.Log("string MultiplayId in Multiplay: " + multiplayId);
                    Main.Instance.Multiplay.SetMultiplay(multiplayId);
                }
            }
        }
    }

    public IEnumerator FindMultiplayA(string multiplayID)
    {
        WWWForm form = new WWWForm();
        form.AddField("multiplayID", multiplayID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/FindMultiplayA.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string playId = www.downloadHandler.text;
                Debug.Log("string playId in FindMultiplayA: " + playId);
                Main.Instance.Multiplay.SetIdA(playId);
            }
        }
    }

    public IEnumerator FindMultiplayB(string multiplayID)
    {
        WWWForm form = new WWWForm();
        form.AddField("multiplayID", multiplayID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/FindMultiplayB.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string playId = www.downloadHandler.text;
                Debug.Log("string playId in FindMultiplayB: " + playId);
                Main.Instance.Multiplay.SetIdB(playId);
            }
        }
    }

    // Importing chat from MySQL
    public IEnumerator ReceiveMessageIDN(string multiplayID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        // form.AddField("senderID", senderID);
        // form.AddField("receiverID", receiverID);
        form.AddField("multiplayID", multiplayID);
        //form.AddField("status", status);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetMsgIdn.php", form))
        {
            yield return www.SendWebRequest();
            // yield return new WaitForSeconds(5.0f);

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string orichatArray;
                string chatArray;
                // Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == " Not found.")
                {
                    Main.Instance.ChatInfo.SetMessage("Tidak ada chat.");
                } else
                {
                    if ((multiplayID != " 0") || (multiplayID != "0"))
                    {
                        orichatArray = www.downloadHandler.text;
                        if (orichatArray.Contains("&#39;"))
                        {
                            chatArray = orichatArray.Replace("&#39;", "'");
                        }
                        else
                        {
                            chatArray = orichatArray;
                        }
                        //Debug.Log("chatJson from JSON in WebRequest: " + orichatArray);
                        Debug.Log("chatJson from JSON in WebRequest: " + chatArray);

                    } else if ((multiplayID == " 0") || (multiplayID == "0"))
                    {
                        orichatArray = "[{\"sender_usern\":\" Administrator\",\"msg\":\"Eror\",\"msg_idn\":\"Pesan gagal diterima karena jaringan.\"}]";
                        chatArray = orichatArray;
                        Debug.Log("Multiplay ID = 0, chat: " + chatArray);
                    }
                    else
                    {
                        chatArray = "Message not found.";
                    }

                    // Main.Instance.ChatInfo.SetMessage(chatString);
                    callback(chatArray);
                }      
            }
        }
    }

    public IEnumerator ReceiveMessageENG(string multiplayID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        // form.AddField("senderID", senderID);
        // form.AddField("receiverID", receiverID);
        form.AddField("multiplayID", multiplayID);
        //form.AddField("status", status);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetMsgEng.php", form))
        {
            yield return www.SendWebRequest();
            // yield return new WaitForSeconds(5.0f);

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string orichatArray;
                string chatArray;
                // Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == " Not found.")
                {
                    Main.Instance.ChatInfo.SetMessage("Tidak ada chat.");
                }
                else
                {
                    if ((multiplayID != " 0") || (multiplayID != "0"))
                    {
                        orichatArray = www.downloadHandler.text;
                        if (orichatArray.Contains("&#39;"))
                        {
                            chatArray = orichatArray.Replace("&#39;", "'");
                            Debug.Log("chatJson from JSON in WebRequest: " + orichatArray);
                            Debug.Log("chatJson from JSON in WebRequest: " + chatArray);
                        }
                        else
                        {
                            chatArray = orichatArray;
                        }
                        //Debug.Log("chatJson from JSON in WebRequest: " + orichatArray);
                        Debug.Log("chatJson from JSON in WebRequest: " + chatArray);
                    }
                    else if ((multiplayID == "0") || (multiplayID == " 0"))
                    {
                        orichatArray = "[{\"sender_usern\":\" Administrator\",\"msg\":\"Eror\",\"msg_en\":\"Due to the network, message could not be received.\"}]";
                        chatArray = orichatArray;
                        Debug.Log("Multiplay ID = 0, chat: " + chatArray);
                    } else
                    {
                        chatArray = "Message not found.";
                    }

                    callback(chatArray);
                }
            }
        }
    }

    public IEnumerator ReceiveMessageJPN(string multiplayID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        // form.AddField("senderID", senderID);
        // form.AddField("receiverID", receiverID);
        form.AddField("multiplayID", multiplayID);
        //form.AddField("status", status);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/GetMsgJpn.php", form))
        {
            yield return www.SendWebRequest();
            // yield return new WaitForSeconds(5.0f);

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string orichatArray;
                string chatArray;
                // Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == " Not found.")
                {
                    Main.Instance.ChatInfo.SetMessage("Tidak ada chat.");
                }
                else
                {
                    if ((multiplayID != " 0") || (multiplayID != "0"))
                    {
                        orichatArray = www.downloadHandler.text;

                        if (orichatArray.Contains("&#39;"))
                        {
                            chatArray = orichatArray.Replace("&#39;", "'");
                        }
                        else
                        {
                            chatArray = orichatArray;
                        }
                        //Debug.Log("chatJson from JSON in WebRequest: " + orichatArray);
                        Debug.Log("chatJson from JSON in WebRequest: " + chatArray);
                    }
                    else if ((multiplayID == "0") || (multiplayID == " 0"))
                    {
                        orichatArray = "[{\"sender_usern\":\" Administrator\",\"msg\":\"Eror\",\"msg_jpn\":\"Due to the network, message could not be received.\"}]";
                        chatArray = orichatArray;
                        Debug.Log("Multiplay ID = 0, chat: " + chatArray);
                    }
                    else
                    {
                        chatArray = "Message not found.";
                    }
                    callback(chatArray);
                }
            }
        }
    }

    public IEnumerator AddMessage(string message, string senderID, string receiverID, string senderName, string multiplayID)
    //public IEnumerator AddMessage(string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("message", message);
        form.AddField("senderID", senderID);
        form.AddField("receiverID", receiverID);
        form.AddField("senderName", senderName);
        form.AddField("multiplayID", multiplayID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/TranslateMsg.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Debug.Log(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("You must fill all input variables."))
                {
                    Debug.Log("Data incompleted.");
                    Main.Instance.ChatInfo.SetMessage("Message Error");
                } else
                {
                    string chatidString = www.downloadHandler.text;
                    Debug.Log("chatidString from JSON in WebRequest: " + chatidString);
                    Main.Instance.ChatInfo.SetChatID(chatidString);
                }
            }
        }
    }

    // Export idA and idB to database, import multiplayId from database
    public IEnumerator SetGameplayID(string userID, string playID, string multiplayID, string pretestID, string posttestID)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("playID", playID);
        form.AddField("multiplayID", multiplayID);
        form.AddField("pretestID", pretestID);
        form.AddField("posttestID", posttestID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://tsunamifighters.iehts.com/SetGameplay.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Main.Instance.QuizOpening.disconnectedPanel.SetActive(true);
            }
            else
            {
                Debug.Log("Gameplay ID is set.");
                //Debug.Log(www.downloadHandler.text);
                string gameplayId = www.downloadHandler.text;
                Debug.Log("Gameplay ID in WebRequest: " + gameplayId);
                Main.Instance.UserInfo.SetGameplayID(gameplayId);
            }
        }
    }
}