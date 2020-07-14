using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndWindow : MonoBehaviour
{
	private int score;
	private int bestScore;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score", 0);
    	GameObject.Find("YourScore").GetComponent<Text>().text = score.ToString();

    	bestScore = PlayerPrefs.GetInt("Best score", 0);
    	GameObject.Find("BestScoreText").GetComponent<Text>().text = "Best score: " + bestScore.ToString();
    }
    public void PlayGame() {
        StartCoroutine(ToGameScene());
    }

    public void GoHome() {
        StartCoroutine(ToStartScene());
    }
    IEnumerator ToStartScene()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("StartScene");
    }
    IEnumerator ToGameScene()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GameScene");
    }
}
