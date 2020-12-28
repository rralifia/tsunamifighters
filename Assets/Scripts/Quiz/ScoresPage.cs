using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoresPage : MonoBehaviour
{
    public Button mainMenuButton, learnMoreButton, conclusionButton, closeButton;
    public GameObject conclusionPanel, mainMenu, learnMore;
    public TextMeshProUGUI knowledge, behavior;
    int pretestScore, posttestScore, kyojoScore, jijoScore, kojoScore, gameScore;

    public void LoadScores()
    {
        Debug.Log("Start exporting answers...");
        StartCoroutine(Main.Instance.Web.ExportAnswers(Opening.quizAnswers[1], Opening.quizAnswers[2], Opening.quizAnswers[3], Opening.quizAnswers[4], Opening.quizAnswers[5], Opening.quizAnswers[6], Opening.quizAnswers[7], Opening.quizAnswers[8], Opening.quizAnswers[9], Opening.quizAnswers[10], Opening.quizAnswers[11], Opening.quizAnswers[12], Opening.quizAnswers[13], Opening.quizAnswers[14], Opening.quizAnswers[15], Opening.quizAnswers[16], Opening.quizAnswers[17], Opening.quizAnswers[18], Opening.quizAnswers[19], Opening.quizAnswers[20], ScoresInfo.play_id, "posttest"));
        conclusionPanel.SetActive(false);
        //mainMenu.SetActive(false);
        //learnMore.SetActive(false);

        mainMenuButton.onClick.AddListener(LoadMainMenu);
        learnMoreButton.onClick.AddListener(LoadLearning);
        conclusionButton.onClick.AddListener(ShowConclusion);
    }

    public void ShowConclusion()
    {
        conclusionPanel.SetActive(true);
        mainMenu.SetActive(true);
        learnMore.SetActive(true);
        closeButton.onClick.AddListener(LoadScores);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        learnMoreButton.onClick.AddListener(LoadLearning);

        pretestScore = int.Parse(ScoresInfo.pretest_score);
        posttestScore = int.Parse(Posttest.posttest_score);
        jijoScore = int.Parse(ScoresInfo.jijo_score);
        kojoScore = int.Parse(ScoresInfo.kojo_score);
        kyojoScore = int.Parse(ScoresInfo.kyojo_score);
        gameScore = int.Parse(ScoresInfo.game_score);

        Debug.Log("pretestScore: " + pretestScore);
        Debug.Log("posttestScore: " + posttestScore);
        Debug.Log("jijoScore: " + jijoScore);
        Debug.Log("kojoScore: " + kojoScore);
        Debug.Log("kyojoScore: " +kyojoScore);

        if (UserInfo.language == 2)
        {
            // Statement 1
            if (posttestScore < pretestScore)
            {
                knowledge.text = "Sayang sekali, pengetahuanmu menurun setelah bermain game. Jangan menyerah untuk tingkatkan pengetahuanmu!";
                Debug.Log("Menurun");
            }
            else if (posttestScore > pretestScore)
            {
                knowledge.text = "Senang sekali, pengetahuanmu meningkat setelah bermain game! Pertahankan dan terapkan pengetahuanmu, ya.";
                Debug.Log("Meningkat");
            }
            else if (posttestScore == pretestScore)
            {
                knowledge.text = "Hmmm, pengetahuanmu tidak berubah setelah bermain game. Jangan menyerah untuk tingkatkan pengetahuanmu!";
                Debug.Log("Sama");
            }

            // Statement 2
            if ((jijoScore == 0) && (kojoScore == 0) && (kyojoScore == 0) && (gameScore == 0))
            {
                behavior.text = "Perilaku pengambilan keputusanmu tidak dapat terdefinisi, sebab kamu tidak bermain visual novel.";
                Debug.Log("Tidak terdefinisi.");
            }
            else if ((jijoScore > kyojoScore) && (jijoScore > kojoScore))
            {
                behavior.text = "Kamu cenderung bergantung pada diri sendiri (jijo) ketika mengambil keputusan aksi dalam menghadapi bencana.";
                Debug.Log("Dominan: Jijo.");
            }
            else if ((kojoScore > jijoScore) && (kojoScore > kyojoScore))
            {
                behavior.text = "Kamu cenderung bergantung pada pemerintah (kojo) ketika mengambil keputusan aksi dalam menghadapi bencana.";
                Debug.Log("Dominan: Kojo.");
            }
            else if ((kyojoScore > jijoScore) && (kyojoScore > kojoScore))
            {
                behavior.text = "Kamu cenderung bergantung pada komunitas (kyojo) ketika mengambil keputusan aksi dalam menghadapi bencana.";
                Debug.Log("Dominan: Kyojo.");
            }
            else if ((jijoScore == kojoScore) && (jijoScore > kyojoScore) && (kojoScore > kyojoScore))
            {
                behavior.text = "Ketergantunganmu dalam mengambil keputusan aksi sama rata untuk diri sendiri (jijo) dan pemerintah (kojo).";
                Debug.Log("Dominan: Jijo & Kojo.");
            }
            else if ((jijoScore == kyojoScore) && (jijoScore > kojoScore) && (kyojoScore > kojoScore))
            {
                behavior.text = "Ketergantunganmu dalam mengambil keputusan aksi sama rata untuk diri sendiri (jijo) dan komunitas (kyojo).";
                Debug.Log("Dominan: Jijo & Kojo.");
            }
            else if ((kyojoScore == kojoScore) && (kyojoScore > jijoScore) && (kojoScore > jijoScore))
            {
                behavior.text = "Ketergantunganmu dalam mengambil keputusan aksi sama rata untuk komunitas (kyojo) dan pemerintah (kojo).";
                Debug.Log("Dominan: Kojo & Kyojo.");
            }
            else if ((jijoScore == kyojoScore) && (kyojoScore == kojoScore) && (jijoScore == kojoScore))
            {
                behavior.text = "Ketergantunganmu dalam mengambil keputusan aksi sama rata untuk diri sendiri, komunitas, dan pemerintah.";
                Debug.Log("Tidak ada yang dominan.");
            }

        }
        else if (UserInfo.language == 1)
        {
            // Statement 1
            if (posttestScore < pretestScore)
            {
                knowledge.text = "Too bad, your knowledge decreases after playing the game. Don't give up on improving your skills and knowledge!";
                Debug.Log("Decreased");
            }
            else if (posttestScore > pretestScore)
            {
                knowledge.text = "Congratulations, your knowledge improves after playing the game! Keep learning and don't forget to implement your knowledge.";
                Debug.Log("Improved");
            }
            else if (posttestScore == pretestScore)
            {
                knowledge.text = "Well, your knowledge hasn't changed even after playing the game. Don't give up on improving your skills and knowledge!";
                Debug.Log("Same");
            }

            // Statement 2
            if ((jijoScore == 0) && (kojoScore == 0) && (kyojoScore == 0) && (gameScore == 0))
            {
                behavior.text = "Your decision-making behavior cannot be defined, because you don't play the visual novel.";
                Debug.Log("Undefined.");
            }
            else if ((jijoScore > kyojoScore) && (jijoScore > kojoScore))
            {
                behavior.text = "When making a decision in a disaster, you tend to manage yourself. Beware of individualism!";
                Debug.Log("Dominance: Jijo.");
            }
            else if ((kojoScore > jijoScore) && (kojoScore > kyojoScore))
            {
                behavior.text = "When making decisions in the event of a disaster, you tend to support the community.";
                Debug.Log("Dominance: Kojo.");
            }
            else if ((kyojoScore > jijoScore) && (kyojoScore > kojoScore))
            {
                behavior.text = "When making decisions in the event of a disaster, you tend to use public support.";
                Debug.Log("Dominance: Kyojo.");
            }
            else if ((jijoScore == kojoScore) && (jijoScore > kyojoScore) && (kojoScore > kyojoScore))
            {
                behavior.text = "When making decisions in the event of a disaster, you tend to be both self-solving and community-supporting.";
                Debug.Log("Dominance: Jijo & Kojo.");
            }
            else if ((jijoScore == kyojoScore) && (jijoScore > kojoScore) && (kyojoScore > kojoScore))
            {
                behavior.text = "When making decisions in the event of a disaster, you tend both to resolve yourself and to use public support.";
                Debug.Log("Dominance: Jijo & Kyojo.");
            }
            else if ((kyojoScore == kojoScore) && (kyojoScore > jijoScore) && (kojoScore > jijoScore))
            {
                behavior.text = "When making decisions in the event of a disaster, you tend to be both community-supported and public-backed.";
                Debug.Log("Dominance: Kojo & Kyojo.");
            }
            else if ((jijoScore == kyojoScore) && (kyojoScore == kojoScore) && (jijoScore == kojoScore))
            {
                behavior.text = "When making a decision in the event of a disaster, you are not biased towards self-help, mutual assistance, or public assistance.";
                Debug.Log("No dominance.");
            }
        }
        else if (UserInfo.language == 3)
        {
            // Statement 1
            if (posttestScore < pretestScore)
            {
                knowledge.text = "残念ながら、ゲームをプレイしたあとに、あなたの知識が減ってしまっています。スキルと知識の向上をあきらめないでください！";
                Debug.Log("Decreased");
            }
            else if (posttestScore > pretestScore)
            {
                knowledge.text = "おめでとうございます。ゲームをプレイしたあとに、あなたの知識は向上しています。学習を続け、知識を実践することを忘れないでください。";
                Debug.Log("Improved");
            }
            else if (posttestScore == pretestScore)
            {
                knowledge.text = "うーん、あなたの知識はゲームをプレイした後でも変わっていません。スキルと知識の向上をあきらめないでください。";
                Debug.Log("Same");
            }

            // Statement 2
            if ((jijoScore == 0) && (kojoScore == 0) && (kyojoScore == 0) && (gameScore == 0))
            {
                behavior.text = "Your decision-making behavior cannot be defined, because you don't play the visual novel.";
                Debug.Log("Undefined.");
            }
            else if ((jijoScore > kyojoScore) && (jijoScore > kojoScore))
            {
                behavior.text = "災害時に判断を下すとき、あなたは自分自身でなんとかする傾向があります。";
                Debug.Log("Dominance: Jijo.");
            }
            else if ((kojoScore > jijoScore) && (kojoScore > kyojoScore))
            {
                behavior.text = "災害時に判断を下すとき、あなたはコミュニティで支えあう傾向があります。";
                Debug.Log("Dominance: Kojo.");
            }
            else if ((kyojoScore > jijoScore) && (kyojoScore > kojoScore))
            {
                behavior.text = "災害時に判断を下すとき、あなたは公的な支援を活用する傾向があります。";
                Debug.Log("Dominance: Kyojo.");
            }
            else if ((jijoScore == kojoScore) && (jijoScore > kyojoScore) && (kojoScore > kyojoScore))
            {
                behavior.text = "災害時に判断を下すとき、あなたは自分自身で解決する傾向と、コミュニティで支えあう傾向の両方があります。";
                Debug.Log("Dominance: Jijo & Kojo.");
            }
            else if ((jijoScore == kyojoScore) && (jijoScore > kojoScore) && (kyojoScore > kojoScore))
            {
                behavior.text = "災害時に判断を下すとき、あなたは自分自身で解決する傾向と、公的な支援を活用する傾向の両方があります。";
                Debug.Log("Dominance: Jijo & Kyojo.");
            }
            else if ((kyojoScore == kojoScore) && (kyojoScore > jijoScore) && (kojoScore > jijoScore))
            {
                behavior.text = "災害時に判断を下すとき、あなたはコミュニティで支えあう傾向と、公的な支援を活用する傾向の両方があります。";
                Debug.Log("Dominance: Kojo & Kyojo.");
            }
            else if ((jijoScore == kyojoScore) && (kyojoScore == kojoScore) && (jijoScore == kojoScore))
            {
                behavior.text = "災害時に判断を下すとき、あなたは自助・共助・公助のいずれにも偏っていません。";
                Debug.Log("No dominance.");
            }
        }
        else
        {
            Debug.Log("Language is not found.");
        }

        Debug.Log("Setting Gameplay ID...");
        Debug.Log("User ID: " + UserInfo.user_id);
        Debug.Log("Play ID: " + ScoresInfo.play_id);
        Debug.Log("Multiplay ID: " + Multiplay.multiplay_id);
        Debug.Log("Pretest ID: " + UserInfo.pretest_id);
        Debug.Log("Posttest ID: " + UserInfo.posttest_id);

        Debug.Log("Setting Gameplay ID...");
        StartCoroutine(Main.Instance.Web.SetGameplayID(UserInfo.user_id, ScoresInfo.play_id, Multiplay.multiplay_id, UserInfo.pretest_id, UserInfo.posttest_id));
    }

    public void LoadLearning()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("Connecting to disconnecting...");
        }
        SceneManager.LoadScene("Learning");
    }
    public void LoadMainMenu()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("Connecting to disconnecting...");
        }
        SceneManager.LoadScene("Main Menu");
    }
}
