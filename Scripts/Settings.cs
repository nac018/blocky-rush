using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    public GameObject soundsToggle;
    public GameObject shakeToggle;
    private Toggle noSounds;
    private Toggle noShake;
    // Start is called before the first frame update
    void Start()
    {
    	noSounds = soundsToggle.GetComponent<Toggle>();
    	noShake = shakeToggle.GetComponent<Toggle>();
        if (PlayerPrefs.GetInt("No Sounds", 0) == 1) // If mute.
        {
        	noSounds.isOn = true;
        }
        else{
        	noSounds.isOn = false;
        }
        if (PlayerPrefs.GetInt("No Shake", 0) == 1) // If no shake.
        {
        	noShake.isOn = true;
        }else{
        	noShake.isOn = false;
        }
    }
    public void SoundChanged(bool ifOn)
    {
    	if (ifOn) // User choose to mute sounds
    	{
    		PlayerPrefs.SetInt("No Sounds", 1);
    	}
    	else{
    		PlayerPrefs.SetInt("No Sounds", 0);
    	}
    }
    public void ShakeChanged(bool ifOn)
    {
    	if (ifOn) // User choose no shake
    	{
    		PlayerPrefs.SetInt("No Shake", 1);
    	}
    	else{
    		PlayerPrefs.SetInt("No Shake", 0);
    	}
    }


}
