using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMute : MonoBehaviour
{
    public AudioMixer audioMixer;

    private Slider master;
	private Slider music;
	private Slider soundEffect;
	private Toggle noSounds;
	private int minVol = -50;
    private int originVol = -5;
    // Start is called before the first frame update
	void Start()
	{
		master = GameObject.Find("Master Volume/Slider-Sound").GetComponent<Slider>();
		music = GameObject.Find("Music/Slider-Sound").GetComponent<Slider>();
		soundEffect = GameObject.Find("SoundEffect/Slider-Sound").GetComponent<Slider>();
		noSounds = GameObject.Find("Sound Toggle").GetComponent<Toggle>();
		if (PlayerPrefs.GetInt("No Sounds", 0) == 1) // If mute.
        {
            master.value = minVol;
            master.interactable = false;
            music.interactable = false;
            soundEffect.interactable = false;
            noSounds.isOn = true;
        }
        else{
        	master.value = originVol;
            master.interactable = true;
            music.interactable = true;
            soundEffect.interactable = true;
            noSounds.isOn = false;
        }
	}
    public void SoundChanged(bool ifOn)
    {
    	if (ifOn) // User choose to mute sounds
    	{
    		PlayerPrefs.SetInt("No Sounds", 1);
    		master.value = minVol;
    		master.interactable = false;
            music.interactable = false;
            soundEffect.interactable = false;
            audioMixer.SetFloat("MasterVolume", minVol);
    	}
    	else{
    		PlayerPrefs.SetInt("No Sounds", 0);
    		master.value = originVol;
    		master.interactable = true;
            music.interactable = true;
            soundEffect.interactable = true;
            audioMixer.SetFloat("MasterVolume", originVol);
    	}
    }
}
