using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BGScroll bgScroll;
    public GameObject inGameInstruct;
    public GameObject tips;
    public bool hasCloudCover;

    private bool updateOn;
    private int isFirst;
    private int health;
    private int score;
    private int level;
    private float time;
    private float speed;
    private GameObject[] healthImage;
    private Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Input.simulateMouseWithTouches = true;
        // Check whether the user is new to game.
        isFirst = PlayerPrefs.GetInt("First time", 0);
        if (isFirst == 0){
            PlayerPrefs.SetInt("First time", 1); 
            Time.timeScale = 0;
            tips.SetActive(true);
            GameObject.Find("Pause").SetActive(false);
            gameObject.GetComponent<AudioSource>().Stop();
        }
        hasCloudCover = false;
        updateOn = true;
        health = 3;
        score = 0;
        level = 0;
        time = 0;
        speed = 2.2f;
        healthImage = GameObject.FindGameObjectsWithTag("Health");
        scoreText = GameObject.Find("Canvas/Score").GetComponent<Text>();
    }
    void Update()
    {
        if (updateOn == true)
        {
            time += Time.deltaTime;
            if (time <= 10){ // Tutorial Level
                level = 0;
            }
            else if (time <= 25){ // Add meteorite and speed up
                level = 1;
                speed = 2.7f;
            }
            else if (time <= 45){ // Add swticher and speed up
                level = 2;
                speed = 3.1f;
            }
            else if (time <= 65){ // Add flashing blocks and moving blocks & speed up
                level = 3;
                speed = 3.5f;
            }
            else if (time <= 85){ // Add cloud cover and speed up
                level = 4;
                speed = 3.9f;
            }
            else if (time <= 110){ // Speed up
                level = 5;
                speed = 4.2f;
            }
            else{   // Speed up
                level = 6;
                speed = 4.5f;
                updateOn = false;
                Debug.Log("StopUpdate");
            }
        }
    }
    public void CloseTips()
    {
        gameObject.GetComponent<AudioSource>().Play();
        tips.SetActive(false);
        inGameInstruct.SetActive(true);
    }
    public void changeBG()
    {
    	bgScroll.BGChange();
    }

    public int GetLevel()
    {
        return level;
    }
    public void AddScore()
    {
        score ++;
        UpdateScore();
    }
    public void LoseHealth()
    {
        health --;
        UpdateHealth();
        if (health <= 0)
        {
            EndGame();
        }
    }
    public void ChangeSpeed()
    {
        BlockMove.speed = speed;
        ColorMove.speed = speed;
        ShapeMove.speed = speed;
    }
    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
    private void UpdateHealth()
    {
        if (health == 2)
        {
            healthImage[0].SetActive(true);
            healthImage[1].SetActive(true);
            healthImage[2].SetActive(false);
        }
        else if (health == 1)
        {
            healthImage[0].SetActive(true);
            healthImage[1].SetActive(false);
            healthImage[2].SetActive(false);
        }
        else if (health == 0)
        {
            healthImage[0].SetActive(false);
            healthImage[1].SetActive(false);
            healthImage[2].SetActive(false);
        }
        else if (health == 3)
        {
            healthImage[0].SetActive(true);
            healthImage[1].SetActive(true);
            healthImage[2].SetActive(true);
        }
    }
    public void EndGame() {
        PlayerPrefs.SetInt("Score", score);
        if (score > PlayerPrefs.GetInt("Best score", 0))
        {
            PlayerPrefs.SetInt("Best score", score);
        }
        StartCoroutine(ToEndScene());
    }
    IEnumerator ToEndScene()
    {
        Time.timeScale = 0;
        GetComponent<AudioSource>().Stop();
        yield return new WaitForSecondsRealtime(0.9f);
        SceneManager.LoadScene("EndScene");
        Time.timeScale = 1;
        // Input.simulateMouseWithTouches = true;
    }
}

