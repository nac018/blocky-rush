using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RetrieveScore : MonoBehaviour
{
    public GameObject scoreBoard;

    private Text scoreText;
    private Text nameText;

    private string scoreString;
    private string nameString;

    public void GetTopTen()
    {
        scoreBoard.SetActive(true);
        scoreText = GameObject.Find("ScoreBoardWindow/Scores").GetComponent<Text>();
        nameText = GameObject.Find("ScoreBoardWindow/Usernames").GetComponent<Text>();
        scoreString = "";
        nameString = "";
        StartCoroutine(GetScores());
    }
    IEnumerator GetScores()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://blocky-rush-server.bitnamiapp.com/GetScores.php"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                string[] scores = webRequest.downloadHandler.text.Split(':');
                for(int i = 0; i < scores.Length - 1; i++){
                    scoreString = scoreString + scores[i].Split(',')[1] + "\n";
                    nameString = nameString + scores[i].Split(',')[0] + "\n";
                }
                SetText();
                //Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }
    void SetText()
    {
        scoreText.text = scoreString;
        nameText.text = nameString;
    }
}
