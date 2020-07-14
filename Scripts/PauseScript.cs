using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
	public GameObject pauseMenu;

	public void OnPause()
	{
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
	}
	public void OnResume()
	{
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
	}
	public void OnHome()
    {
        //Load home scene
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }
    public void OnRestart()
    {
        //Restart. Load game scene
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }
}
