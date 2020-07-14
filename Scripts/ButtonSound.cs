using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource buttonSound;
    // Start is called before the first frame update
    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
    }

    public void PlaySoundOnClick()
    {
    	buttonSound.Play();
    }
}
