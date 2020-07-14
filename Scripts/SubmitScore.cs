using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SubmitScore : MonoBehaviour
{
	public InputField nameField;
	public Button okButton;
	private GameObject errorMsg;
	private string username;
	private int score;

	public GameObject submitField;
	public GameObject successField;

	public AudioSource uploadSound;

	// Start is called before the first frame update
	void Start()
	{
    	errorMsg = GameObject.Find("ErrorMsg");
    	errorMsg.SetActive(false);
    	nameField.characterLimit = 15;
		okButton.interactable = true;
	}
	public void GetName()
	{
		username = nameField.text;
		if (username.Length >= 1)
		{
			okButton.interactable = false;
			score = PlayerPrefs.GetInt("Score", 0);
        	StartCoroutine(SetScore(username, score));
        	uploadSound.Play();
        	StartCoroutine(AfterUpload());
		}
		else{
			errorMsg.SetActive(true);
		}			
	}
	IEnumerator AfterUpload()
    {
        yield return new WaitForSeconds(0.1f);
        successField.SetActive(true);
        submitField.SetActive(false);
    }
	IEnumerator SetScore(string username, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("myUsername", username);
        form.AddField("myScore", score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://blocky-rush-server.bitnamiapp.com/SetScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
